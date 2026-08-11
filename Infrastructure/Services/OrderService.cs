using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Infrastructure.Services;

public class OrderService : IOrderService
{
	private readonly IOrderRepository _orderRepository;
	private readonly ICartRepository _cartRepository;
	private readonly IMapper _mapper;
	private readonly IInventoryService _inventoryService;
	private readonly INotificationService _notifier;
	private readonly IAdminDashboardService _dashboardService;
	private readonly IEmailService _emailService;
	private readonly UserManager<AppUser> _userManager;
	private readonly IPaymentRepository _paymentRepository;
	private readonly IBusinessEventPublisher _eventPublisher;
	private readonly AppDbContext _context;
	private readonly ITransactionActionQueue _actionQueue;

	public OrderService(
		IOrderRepository orderRepository, 
		ICartRepository cartRepository, 
		IMapper mapper, 
		IInventoryService inventoryService, 
		INotificationService notifier, 
		IAdminDashboardService dashboardService, 
		IEmailService emailService, 
		UserManager<AppUser> userManager, 
		IPaymentRepository paymentRepository,
		IBusinessEventPublisher eventPublisher,
		AppDbContext context,
		ITransactionActionQueue actionQueue)
	{
		_orderRepository = orderRepository;
		_cartRepository = cartRepository;
		_mapper = mapper;
		_inventoryService = inventoryService;
		_notifier = notifier;
		_dashboardService = dashboardService;
		_emailService = emailService;
		_userManager = userManager;
		_paymentRepository = paymentRepository;
		_eventPublisher = eventPublisher;
		_context = context;
		_actionQueue = actionQueue;
	}

	public async Task<OrderDto> CreateOrderAsync(string userId, OrderCreateDto orderCreateDto)
	{
		using var tx = await _context.Database.BeginTransactionAsync();
		try
		{
			var cart = await _cartRepository.GetCartByUserIdAsync(userId);
			if (cart == null || !cart.CartItems.Any())
			{
				throw new Exception("Cart is empty. Please add items to your cart before checking out.");
			}

			var orderItems = cart.CartItems.Select(ci => new OrderItem
			{
				ProductId = ci.ProductId,
				Quantity = ci.Quantity,
				UnitPrice = ci.UnitPrice,
				Color = ci.Color,
				ColorCode = ci.ColorCode,
				ProductVariantId = ci.VariantId,
				ProductImageUrl = ci.ProductImageUrl,
				IsPersonalised = ci.IsPersonalised,
				EngravingName = ci.EngravingName,
				EngravingDate = ci.EngravingDate,
				EngravingMessage = ci.EngravingMessage,
				EngravingPrice = ci.EngravingPrice
			}).ToList();

			decimal subtotal = cart.CartItems.Sum(ci => ci.Quantity * (ci.UnitPrice + (ci.IsPersonalised ? ci.EngravingPrice : 0)));

			var addr = orderCreateDto.ShippingAddress;
			var parts = new[]
			{
				addr?.FullName, addr?.AddressLine1, addr?.AddressLine2,
				addr?.City, addr?.State, addr?.PostalCode, addr?.Country,
				string.IsNullOrWhiteSpace(addr?.Phone) ? null : $"Ph: {addr.Phone}"
			};
			var shippingAddress = string.Join(", ", parts.Where(p => !string.IsNullOrWhiteSpace(p)));

			var gst = subtotal * 0.18m;
			var shipping = subtotal >= 999 ? 0 : 79;
			decimal calculatedDiscount = 0m;
			string? appliedPromoCode = null;

			if (!string.IsNullOrWhiteSpace(orderCreateDto.PromoCode))
			{
				var normalizedCode = orderCreateDto.PromoCode.Trim().ToUpperInvariant();
				var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Code == normalizedCode);
				var now = DateTime.UtcNow;

				if (coupon == null || !coupon.IsActive || coupon.ExpiryDate <= now ||
					(coupon.StartDate.HasValue && coupon.StartDate.Value > now) ||
					(coupon.UsageLimit > 0 && coupon.UsedCount >= coupon.UsageLimit) ||
					subtotal < coupon.MinimumOrderAmount)
				{
					throw new Exception($"Invalid, expired, or inapplicable promo code '{orderCreateDto.PromoCode}'.");
				}

				if (string.Equals(coupon.DiscountType, "Shipping", StringComparison.OrdinalIgnoreCase))
				{
					shipping = 0;
					calculatedDiscount = 0m;
				}
				else if (string.Equals(coupon.DiscountType, "Percentage", StringComparison.OrdinalIgnoreCase) || coupon.DiscountPercentage.HasValue)
				{
					var pct = coupon.DiscountPercentage ?? 0;
					calculatedDiscount = Math.Round(subtotal * pct / 100m, 2);
					if (coupon.MaximumDiscount.HasValue && calculatedDiscount > coupon.MaximumDiscount.Value)
					{
						calculatedDiscount = coupon.MaximumDiscount.Value;
					}
				}
				else
				{
					calculatedDiscount = Math.Min(subtotal, coupon.DiscountAmount);
				}

				// Increment UsedCount within the single existing transaction
				coupon.UsedCount++;
				_context.Coupons.Update(coupon);
				appliedPromoCode = coupon.Code;
			}

			var grandTotal = Math.Max(0, subtotal + gst + shipping - calculatedDiscount);

			var order = new Order
			{
				UserId = userId,
				ShippingAddress = shippingAddress,
				CreatedDate = DateTime.UtcNow,
				Status = "Pending",
				PromoCode = appliedPromoCode,
				DiscountAmount = calculatedDiscount,
				TotalAmount = grandTotal,
				OrderItems = orderItems
			};

			await _orderRepository.AddOrderAsync(order);
			await _orderRepository.SaveChangesAsync();

			// Reserve stock for each item
			foreach (var item in order.OrderItems)
			{
				await _inventoryService.ReserveStockAsync(new Application.DTOs.ReserveStockDto { ProductId = item.ProductId, Quantity = item.Quantity, UserId = userId });
			}

			// Notify admins about new order
			await _notifier.NotifyNewOrderAsync(order.OrderId);

			var user = await _userManager.FindByIdAsync(userId);
			if (user != null && !string.IsNullOrEmpty(user.Email))
			{
				await _eventPublisher.PublishAsync(new Application.Common.Events.BusinessEvent
				{
					ActionType = "CREATED",
					EntityType = "Order",
					EntityId = order.OrderId.ToString(),
					Title = $"New Order #{order.OrderId}",
					Description = $"Order #{order.OrderId} placed by {user.FullName} for total ₹{order.TotalAmount:N2}",
					Icon = "ShoppingBag",
					ColorClass = "text-success",
					BgClass = "bg-success",

					UserId = userId,
					NotificationTitle = $"Order Placed Successfully",
					NotificationMessage = $"Your order #{order.OrderId} of ₹{order.TotalAmount:N2} has been placed.",
					NotificationType = "Order",
					OrderId = order.OrderId,
					NavigationUrl = $"/orders/{order.OrderId}",

					SendEmailTo = user.Email,
					EmailSubject = $"Order Confirmed - #{order.OrderId}",
					EmailBody = EmailTemplates.GetOrderConfirmationEmail(user.FullName, order.OrderId, order.CreatedDate.ToString("dd MMM yyyy hh:mm tt"), order.TotalAmount.ToString("N2"), order.ShippingAddress)
				});
			}

			// Broadcast updated dashboard summary (best-effort)
			try
			{
				var summary = await _dashboardService.GetSummaryAsync();
				await _notifier.NotifyDashboardUpdatedAsync(summary);
			}
			catch
			{
				// Best-effort: don't block order creation
			}

			if (cart != null)
			{
				await _cartRepository.ClearCartAsync(cart.CartId);
				await _cartRepository.SaveChangesAsync();
			}

			await tx.CommitAsync();
			await _actionQueue.RunAllAsync();

			return _mapper.Map<OrderDto>(order);
		}
		catch
		{
			await tx.RollbackAsync();
			throw;
		}
	}

	public async Task<IEnumerable<OrderDto>> GetUserOrdersAsync(string userId)
	{
		var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
		return _mapper.Map<IEnumerable<OrderDto>>(orders);
	}

	public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
	{
		var orders = await _orderRepository.GetAllOrdersAsync();
		return _mapper.Map<IEnumerable<OrderDto>>(orders);
	}

	public async Task<OrderDto> GetOrderByIdAsync(string userId, int orderId)
	{
		var order = await _orderRepository.GetOrderByIdAsync(orderId);
		if (order == null || order.UserId != userId) throw new Exception("Order not found");

		return _mapper.Map<OrderDto>(order);
	}	public async Task UpdateOrderStatusAsync(int orderId, OrderStatusUpdateDto orderStatusUpdateDto)
	{
		using var tx = await _context.Database.BeginTransactionAsync();
		try
		{
			var order = await _orderRepository.GetOrderByIdAsync(orderId);
			if (order != null)
			{
				await _orderRepository.UpdateOrderStatusAsync(orderId, orderStatusUpdateDto.Status);
				await _orderRepository.SaveChangesAsync();
				await _notifier.NotifyOrderStatusAsync(orderId, orderStatusUpdateDto.Status);

				var user = await _userManager.FindByIdAsync(order.UserId);
				if (user != null && !string.IsNullOrEmpty(user.Email))
				{
					string customerName = user.FullName ?? user.UserName ?? "Customer";
					await _eventPublisher.PublishAsync(new Application.Common.Events.BusinessEvent
					{
						ActionType = "UPDATED",
						EntityType = "Order",
						EntityId = order.OrderId.ToString(),
						Title = $"Order #{order.OrderId} {orderStatusUpdateDto.Status}",
						Description = $"Order #{order.OrderId} status changed to '{orderStatusUpdateDto.Status}' by Administrator",
						Icon = "Truck",
						ColorClass = "text-info",
						BgClass = "bg-info",

						UserId = order.UserId,
						NotificationTitle = $"Order Status Updated",
						NotificationMessage = $"Your order #{order.OrderId} status is now '{orderStatusUpdateDto.Status}'.",
						NotificationType = "Order",
						OrderId = order.OrderId,
						NavigationUrl = $"/orders/{order.OrderId}",

						SendEmailTo = user.Email,
						EmailSubject = $"Order #{order.OrderId} Update",
						EmailBody = EmailTemplates.GetOrderStatusEmail(customerName, order.OrderId, orderStatusUpdateDto.Status)
					});
				}
			}
			await tx.CommitAsync();
			await _actionQueue.RunAllAsync();
		}
		catch
		{
			await tx.RollbackAsync();
			throw;
		}
	}

	public async Task UpdateBulkOrderStatusAsync(BulkOrderStatusUpdateDto dto)
	{
		using var tx = await _context.Database.BeginTransactionAsync();
		try
		{
			foreach (var orderId in dto.OrderIds)
			{
				var order = await _orderRepository.GetOrderByIdAsync(orderId);
				if (order != null)
				{
					await _orderRepository.UpdateOrderStatusAsync(orderId, dto.Status);
					await _notifier.NotifyOrderStatusAsync(orderId, dto.Status);

					var user = await _userManager.FindByIdAsync(order.UserId);
					if (user != null && !string.IsNullOrEmpty(user.Email))
					{
						string customerName = user.FullName ?? user.UserName ?? "Customer";
						await _eventPublisher.PublishAsync(new Application.Common.Events.BusinessEvent
						{
							ActionType = "UPDATED",
							EntityType = "Order",
							EntityId = order.OrderId.ToString(),
							Title = $"Order #{order.OrderId} {dto.Status}",
							Description = $"Order #{order.OrderId} status changed to '{dto.Status}' (Bulk Action) by Administrator",
							Icon = "Truck",
							ColorClass = "text-info",
							BgClass = "bg-info",

							UserId = order.UserId,
							NotificationTitle = $"Order Status Updated",
							NotificationMessage = $"Your order #{order.OrderId} status is now '{dto.Status}'.",
							NotificationType = "Order",
							OrderId = order.OrderId,
							NavigationUrl = $"/orders/{order.OrderId}",

							SendEmailTo = user.Email,
							EmailSubject = $"Order #{order.OrderId} Update",
							EmailBody = EmailTemplates.GetOrderStatusEmail(customerName, order.OrderId, dto.Status)
						});
					}
				}
			}
			await _orderRepository.SaveChangesAsync();
			await tx.CommitAsync();
			await _actionQueue.RunAllAsync();
		}
		catch
		{
			await tx.RollbackAsync();
			throw;
		}
	}

	public async Task DeleteBulkOrdersAsync(List<int> orderIds)
	{
		foreach (var orderId in orderIds)
		{
			var order = await _orderRepository.GetOrderByIdAsync(orderId);
			if (order != null)
			{
				order.Status = "Cancelled"; 
			}
		}
		await _orderRepository.SaveChangesAsync();
	}

	public async Task CancelOrderAsync(string userId, int orderId)
	{
		using var tx = await _context.Database.BeginTransactionAsync();
		try
		{
			var order = await _orderRepository.GetOrderByIdAsync(orderId);
			if (order == null || order.UserId != userId) throw new Exception("Order not found");

			if (order.Status != "Pending" && order.Status != "Processing")
				throw new Exception("Order cannot be cancelled");

			order.Status = "Cancelled";
			await _orderRepository.SaveChangesAsync();
			await _notifier.NotifyOrderStatusAsync(order.OrderId, "Cancelled");

			var user = await _userManager.FindByIdAsync(userId);
			if (user != null && !string.IsNullOrEmpty(user.Email))
			{
				string customerName = user.FullName ?? user.UserName ?? "Customer";
				await _eventPublisher.PublishAsync(new Application.Common.Events.BusinessEvent
				{
					ActionType = "CANCELLED",
					EntityType = "Order",
					EntityId = order.OrderId.ToString(),
					Title = $"Order #{order.OrderId} Cancelled",
					Description = $"Order #{order.OrderId} cancelled by User",
					Icon = "XCircle",
					ColorClass = "text-danger",
					BgClass = "bg-danger",

					UserId = userId,
					NotificationTitle = $"Order Cancelled",
					NotificationMessage = $"Your order #{order.OrderId} has been successfully cancelled.",
					NotificationType = "Order",
					OrderId = order.OrderId,
					NavigationUrl = $"/orders/{order.OrderId}",

					SendEmailTo = user.Email,
					EmailSubject = $"Order #{order.OrderId} Cancelled",
					EmailBody = EmailTemplates.GetOrderStatusEmail(customerName, order.OrderId, order.Status)
				});
			}

			// Restore reserved stock for cancelled order
			foreach (var item in order.OrderItems)
			{
				await _inventoryService.ReleaseStockAsync(new ReserveStockDto { ProductId = item.ProductId, Quantity = item.Quantity, UserId = userId });
			}

			await tx.CommitAsync();
			await _actionQueue.RunAllAsync();
		}
		catch
		{
			await tx.RollbackAsync();
			throw;
		}
	}

	public async Task<byte[]> GenerateInvoicePdfAsync(int orderId)
	{
		var order = await _orderRepository.GetOrderByIdAsync(orderId);
		if (order == null) throw new Exception("Order not found");

		var paymentInfo = await _paymentRepository.GetPaymentByOrderIdAsync(orderId);

		decimal subtotal = order.OrderItems.Sum(x => x.UnitPrice * x.Quantity);
		decimal gst = subtotal * 0.18m;
		decimal shipping = subtotal >= 999 ? 0 : 79;
		decimal grandTotal = subtotal + gst + shipping - order.DiscountAmount;

		var document = Document.Create(container =>
		{
			container.Page(page =>
			{
				page.Size(PageSizes.A4);
				page.Margin(30);
				page.DefaultTextStyle(x => x.FontSize(11));

				// HEADER
				page.Header().Row(row =>
				{
					row.RelativeItem().Column(col =>
					{
						col.Item().Text("BEATBOX").FontSize(28).Bold().FontColor("#00F3FF");
						col.Item().Text("Premium Audio Experience").FontSize(10);
						col.Item().Text("support@beatbox.com");
						col.Item().Text("www.beatbox.com");
					});
					row.RelativeItem().AlignRight().Column(col =>
					{
						col.Item().Text("INVOICE").FontSize(24).Bold();
						col.Item().Text($"Invoice No: BB-{order.CreatedDate.Year}-{order.OrderId:D6}");
						col.Item().Text($"Date: {order.CreatedDate:dd MMM yyyy}");
						col.Item().Text($"Status: {order.Status}");
					});
				});

				// CONTENT
				page.Content().PaddingVertical(20).Column(col =>
				{
					// BILL TO
					col.Item().Border(1).Padding(10).Column(address =>
					{
						address.Item().Text("CUSTOMER DETAILS").Bold().FontSize(14);
						address.Item().PaddingTop(5);
						address.Item().Text(order.ShippingAddress);
					});

					col.Item().PaddingVertical(15);

					// PRODUCTS TABLE
					col.Item().Table(table =>
					{
						table.ColumnsDefinition(columns =>
						{
							columns.RelativeColumn(4);
							columns.RelativeColumn(1);
							columns.RelativeColumn(2);
							columns.RelativeColumn(2);
						});

						table.Header(header =>
						{
							header.Cell().Background("#00F3FF").Padding(5).Text("Product").Bold();
							header.Cell().Background("#00F3FF").Padding(5).Text("Qty").Bold();
							header.Cell().Background("#00F3FF").Padding(5).Text("Price").Bold();
							header.Cell().Background("#00F3FF").Padding(5).Text("Total").Bold();
						});

						foreach (var item in order.OrderItems)
						{
							table.Cell().BorderBottom(1).Padding(5).Text(item.Product?.Name ?? "Product");
							table.Cell().BorderBottom(1).Padding(5).Text(item.Quantity.ToString());
							table.Cell().BorderBottom(1).Padding(5).Text($"₹{item.UnitPrice:N2}");
							table.Cell().BorderBottom(1).Padding(5).Text($"₹{(item.Quantity * item.UnitPrice):N2}");
						}
					});

					col.Item().PaddingVertical(20);

					// TOTALS BOX
					col.Item().AlignRight().Width(300).Border(1).Padding(10).Column(total =>
					{
						total.Item().Row(row =>
						{
							row.RelativeItem().Text("Subtotal");
							row.ConstantItem(100).AlignRight().Text($"₹{subtotal:N2}");
						});

						total.Item().Row(row =>
						{
							row.RelativeItem().Text("GST (18%)");
							row.ConstantItem(100).AlignRight().Text($"₹{gst:N2}");
						});

						total.Item().Row(row =>
						{
							row.RelativeItem().Text("Shipping");
							row.ConstantItem(100).AlignRight().Text(shipping == 0 ? "FREE" : $"₹{shipping:N2}");
						});

						if (order.DiscountAmount > 0)
						{
							total.Item().Row(row =>
							{
								row.RelativeItem().Text($"Discount ({order.PromoCode})");
								row.ConstantItem(100).AlignRight().Text($"-₹{order.DiscountAmount:N2}");
							});
						}

						total.Item().PaddingVertical(5).LineHorizontal(1);

						total.Item().Row(row =>
						{
							row.RelativeItem().Text("Grand Total").Bold().FontSize(14);
							row.ConstantItem(100).AlignRight().Text($"₹{order.TotalAmount:N2}").Bold().FontSize(14);
						});
					});

					col.Item().PaddingVertical(20);

					// PAYMENT INFO
					col.Item().Border(1).Padding(10).Column(payment =>
					{
						payment.Item().Text($"Payment Method: {paymentInfo?.Method ?? "N/A"}");
						payment.Item().Text($"Transaction ID: {paymentInfo?.TransactionId ?? "N/A"}");
						payment.Item().Text($"Payment Status: {paymentInfo?.Status ?? "Pending"}");
						payment.Item().Text($"Order Status: {order.Status}");
					});
				});

				// FOOTER
				page.Footer().AlignCenter().Column(col =>
				{
					col.Item().LineHorizontal(1);
					col.Item().PaddingTop(10);
					col.Item().Text("Thank you for shopping with BeatBox!").Bold();
					col.Item().Text("This is a computer-generated invoice.");
					col.Item().Text("support@beatbox.com | www.beatbox.com");
				});
			});
		});

		return document.GeneratePdf();
	}
}