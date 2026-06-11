using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
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

    public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository, IMapper mapper, IInventoryService inventoryService, INotificationService notifier, IAdminDashboardService dashboardService, IEmailService emailService, UserManager<AppUser> userManager, IPaymentRepository paymentRepository)
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
    }

    public async Task<OrderDto> CreateOrderAsync(string userId, OrderCreateDto orderCreateDto)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);

        if (cart == null || !cart.CartItems.Any()) throw new Exception("Cart is empty");

        // Build a clean shipping address string, skipping empty parts
        var addr = orderCreateDto.ShippingAddress;
        var parts = new[]
        {
            addr?.FullName, addr?.AddressLine1, addr?.AddressLine2,
            addr?.City, addr?.State, addr?.PostalCode, addr?.Country,
            string.IsNullOrWhiteSpace(addr?.Phone) ? null : $"Ph: {addr.Phone}"
        };
        var shippingAddress = string.Join(", ", parts.Where(p => !string.IsNullOrWhiteSpace(p)));

        var subtotal = cart.CartItems.Sum(ci => ci.Quantity * ci.UnitPrice);

        var gst = subtotal * 0.18m;

        var shipping = subtotal >= 999 ? 0 : 79;

        var discount = orderCreateDto.DiscountAmount;

        var grandTotal =
            subtotal +
            gst +
            shipping -
            discount;

        var order = new Order
        {
            UserId = userId,
            ShippingAddress = shippingAddress,
            CreatedDate = DateTime.UtcNow,
            Status = "Pending",
            PromoCode = orderCreateDto.PromoCode,
            DiscountAmount = orderCreateDto.DiscountAmount,
            TotalAmount = grandTotal,

            OrderItems = cart.CartItems.Select(ci => new OrderItem
            {
                ProductId = ci.ProductId,
                Quantity = ci.Quantity,
                UnitPrice = ci.UnitPrice
            }).ToList()
        };

        await _orderRepository.AddOrderAsync(order);
        await _orderRepository.SaveChangesAsync();

        var user = await _userManager.FindByIdAsync(userId);

        if (user?.Email != null)
        {
            await _emailService.SendEmailAsync(
     user.Email,
     $"Order Confirmed - #{order.OrderId}",
     BuildOrderConfirmationEmail(user, order)
 );
        }
        ;

        // Reserve stock for each item
        foreach (var item in order.OrderItems)
        {
            await _inventoryService.ReserveStockAsync(new Application.DTOs.ReserveStockDto { ProductId = item.ProductId, Quantity = item.Quantity, UserId = userId });
        }

        // Notify admins about new order
        await _notifier.NotifyNewOrderAsync(order.OrderId);

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

        await _cartRepository.ClearCartAsync(cart.CartId);
        await _cartRepository.SaveChangesAsync();

        return _mapper.Map<OrderDto>(order);
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
    }

    public async Task UpdateOrderStatusAsync(int orderId, OrderStatusUpdateDto orderStatusUpdateDto)
    {
        await _orderRepository.UpdateOrderStatusAsync(orderId, orderStatusUpdateDto.Status);
        await _orderRepository.SaveChangesAsync();

        var order =
    await _orderRepository.GetOrderByIdAsync(orderId);

        var user =
            await _userManager.FindByIdAsync(order.UserId);

        if (user?.Email != null)
        {
            string customerName = user.UserName; // Assuming UserName is the customer's name
            await _emailService.SendEmailAsync(
     user.Email,
     $"Order #{order.OrderId} Update",
     BuildStatusEmail(customerName, order.OrderId, order.Status)
 );
        }

        await _notifier.NotifyOrderStatusAsync(
     orderId,
     orderStatusUpdateDto.Status);

    }

    public async Task CancelOrderAsync(string userId, int orderId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);
        if (order == null || order.UserId != userId) throw new Exception("Order not found");

        if (order.Status != "Pending" && order.Status != "Processing")
            throw new Exception("Order cannot be cancelled");

        order.Status = "Cancelled";
        await _orderRepository.SaveChangesAsync();

        var user = await _userManager.FindByIdAsync(order.UserId);

        if (user?.Email != null)
        {
            string customerName = user.UserName; // Assuming UserName is the customer's name
            await _emailService.SendEmailAsync(
                user.Email,
                $"Order #{order.OrderId} Cancelled",
                BuildStatusEmail(customerName, order.OrderId, order.Status)
            );
        }

        await _notifier.NotifyOrderStatusAsync(order.OrderId, "Cancelled");

        // Restore reserved stock for cancelled order
        foreach (var item in order.OrderItems)
        {
            await _inventoryService.ReleaseStockAsync(new ReserveStockDto { ProductId = item.ProductId, Quantity = item.Quantity, UserId = userId });
        }

        // Broadcast updated dashboard summary (best-effort)
        try
        {
            var summary = await _dashboardService.GetSummaryAsync();
            await _notifier.NotifyDashboardUpdatedAsync(summary);
        }
        catch
        {
        }
    }

    private string BuildStatusEmail(
        string customerName,
        int orderId,
        string status)
    {
        string statusColor = status switch
        {
            "Pending" => "#f59e0b",
            "Processing" => "#3b82f6",
            "Shipped" => "#8b5cf6",
            "Delivered" => "#22c55e",
            "Cancelled" => "#ef4444",
            _ => "#64748b"
        };

        string statusMessage = status switch
        {
            "Pending" =>
                "Your order has been received and is waiting to be processed.",

            "Processing" =>
                "Our team is preparing your products for shipment.",

            "Shipped" =>
                "Great news! Your order has been shipped and is on its way.",

            "Delivered" =>
                "Your order has been delivered successfully. We hope you enjoy your purchase.",

            "Cancelled" =>
                "Your order has been cancelled. If this was unexpected, please contact support.",

            _ =>
                "Your order status has been updated."
        };

        string emoji = status switch
        {
            "Pending" => "🟡",
            "Processing" => "🔵",
            "Shipped" => "🚚",
            "Delivered" => "✅",
            "Cancelled" => "❌",
            _ => "ℹ️"
        };

        return $@"
<html>

<body style='margin:0;padding:0;background:#f4f6f9;font-family:Segoe UI,Arial,sans-serif;'>

<div style='max-width:700px;margin:30px auto;background:white;border-radius:12px;overflow:hidden;border:1px solid #e5e7eb;'>

    <!-- Header -->
    <div style='background:linear-gradient(135deg,#00f3ff,#6c2bff);padding:30px;text-align:center;color:white;'>

        <h1 style='margin:0;font-size:32px;'>
            🎧 BEATBOX
        </h1>

        <p style='margin-top:8px;font-size:15px;opacity:0.9;'>
            Premium Audio Experience
        </p>

    </div>

    <!-- Content -->
    <div style='padding:35px;'>

        <h2 style='margin-top:0;color:#111827;'>
            Order Update {emoji}
        </h2>

        <p style='font-size:15px;color:#374151;'>
            Hello <strong>{customerName}</strong>,
        </p>

        <p style='font-size:15px;color:#374151;line-height:1.6;'>
            {statusMessage}
        </p>

        <!-- Status Card -->
        <div style='background:#f8fafc;padding:25px;border-radius:10px;border-left:5px solid {statusColor};margin:25px 0;'>

            <h3 style='margin-top:0;color:#111827;'>
                Order Information
            </h3>

            <p>
                <strong>Order ID:</strong>
                #{orderId}
            </p>

            <p>
                <strong>Current Status:</strong>
            </p>

            <div style='margin-top:15px;'>

                <span style='
                    background:{statusColor};
                    color:white;
                    padding:12px 24px;
                    border-radius:25px;
                    font-weight:bold;
                    font-size:15px;'>
                    {status}
                </span>

            </div>

        </div>

        <!-- Timeline -->
        <div style='background:#f9fafb;padding:20px;border-radius:8px;'>

            <h3 style='margin-top:0;color:#111827;'>
                Order Journey
            </h3>

            <p>📦 Order Placed</p>
            <p>⚙️ Processing</p>
            <p>🚚 Shipped</p>
            <p>🏠 Delivered</p>

        </div>

        <p style='margin-top:30px;color:#374151;'>

            Thank you for choosing BeatBox.

        </p>

        <p style='color:#6b7280;font-size:14px;'>

            Need help? Simply reply to this email and our support team will assist you.

        </p>

    </div>

    <!-- Footer -->
    <div style='background:#111827;color:white;padding:20px;text-align:center;'>

        <h3 style='margin:0;'>
            BeatBox Audio
        </h3>

        <p style='margin-top:10px;color:#d1d5db;font-size:13px;'>
            Premium Headphones • Speakers • Audio Accessories
        </p>

        <p style='font-size:12px;color:#9ca3af;margin-top:15px;'>
            This is an automated email from BeatBox.
        </p>

    </div>

</div>

</body>

</html>";
    }

    public async Task<byte[]> GenerateInvoicePdfAsync(int orderId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);

        if (order == null)
            throw new Exception("Order not found");

        var paymentInfo =
    await _paymentRepository.GetPaymentByOrderIdAsync(orderId);

        decimal subtotal = order.OrderItems.Sum(x =>
            x.UnitPrice * x.Quantity);

        decimal gst = subtotal * 0.18m;

        decimal shipping = subtotal >= 999 ? 0 : 79;

        decimal grandTotal = subtotal + gst + shipping;

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);

                page.Margin(30);

                page.DefaultTextStyle(x =>
                    x.FontSize(11));

                // HEADER
                page.Header().Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text("BEATBOX")
                            .FontSize(28)
                            .Bold()
                            .FontColor("#00F3FF");

                        col.Item().Text("Premium Audio Experience")
                            .FontSize(10);

                        col.Item().Text("support@beatbox.com");
                        col.Item().Text("www.beatbox.com");
                    });

                    row.RelativeItem().AlignRight().Column(col =>
                    {
                        col.Item().Text("INVOICE")
                            .FontSize(24)
                            .Bold();

                        col.Item().Text(
                            $"Invoice No: BB-{order.CreatedDate.Year}-{order.OrderId:D6}");

                        col.Item().Text(
                            $"Date: {order.CreatedDate:dd MMM yyyy}");

                        col.Item().Text(
                            $"Status: {order.Status}");
                    });
                });

                // CONTENT
                page.Content().PaddingVertical(20).Column(col =>
                {
                    // BILL TO
                    col.Item()
                        .Border(1)
                        .Padding(10)
                        .Column(address =>
                        {
                            address.Item()
    .Text("CUSTOMER DETAILS")
    .Bold()
    .FontSize(14);

                            address.Item()
                                .PaddingTop(5);

                            address.Item()
                                .Text(order.ShippingAddress);
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
                            header.Cell()
                                .Background("#00F3FF")
                                .Padding(5)
                                .Text("Product")
                                .Bold();

                            header.Cell()
                                .Background("#00F3FF")
                                .Padding(5)
                                .Text("Qty")
                                .Bold();

                            header.Cell()
                                .Background("#00F3FF")
                                .Padding(5)
                                .Text("Price")
                                .Bold();

                            header.Cell()
                                .Background("#00F3FF")
                                .Padding(5)
                                .Text("Total")
                                .Bold();
                        });

                        foreach (var item in order.OrderItems)
                        {
                            table.Cell()
                                .BorderBottom(1)
                                .Padding(5)
                                .Text(item.Product?.Name ?? "Product");

                            table.Cell()
                                .BorderBottom(1)
                                .Padding(5)
                                .Text(item.Quantity.ToString());

                            table.Cell()
                                .BorderBottom(1)
                                .Padding(5)
                                .Text($"₹{item.UnitPrice:N2}");

                            table.Cell()
                                .BorderBottom(1)
                                .Padding(5)
                                .Text($"₹{(item.Quantity * item.UnitPrice):N2}");
                        }
                    });

                    col.Item().PaddingVertical(20);

                    // TOTALS BOX
                    col.Item()
          .AlignRight()
          .Width(300)
          .Border(1)
          .Padding(10)
          .Column(total =>
          {
              total.Item().Row(row =>
              {
                  row.RelativeItem().Text("Subtotal");

                  row.ConstantItem(100)
                      .AlignRight()
                      .Text($"₹{subtotal:N2}");
              });

              total.Item().Row(row =>
              {
                  row.RelativeItem().Text("GST (18%)");

                  row.ConstantItem(100)
                      .AlignRight()
                      .Text($"₹{gst:N2}");
              });

              total.Item().Row(row =>
              {
                  row.RelativeItem().Text("Shipping");

                  row.ConstantItem(100)
                      .AlignRight()
                      .Text(shipping == 0
                          ? "FREE"
                          : $"₹{shipping:N2}");
              });

              if (order.DiscountAmount > 0)
              {
                  total.Item().Row(row =>
                  {
                      row.RelativeItem()
                          .Text($"Discount ({order.PromoCode})");

                      row.ConstantItem(100)
                          .AlignRight()
                          .Text($"-₹{order.DiscountAmount:N2}");
                  });
              }

              total.Item()
                  .PaddingVertical(5)
                  .LineHorizontal(1);

              total.Item().Row(row =>
              {
                  row.RelativeItem()
                      .Text("Grand Total")
                      .Bold()
                      .FontSize(14);

                  row.ConstantItem(100)
                      .AlignRight()
                      .Text($"₹{order.TotalAmount:N2}")
                      .Bold()
                      .FontSize(14);
              });
          });

                    col.Item().PaddingVertical(20);

                    // PAYMENT INFO
                    col.Item()
                        .Border(1)
                        .Padding(10)
                        .Column(payment =>
                        {
                            payment.Item()
     .Text($"Payment Method: {paymentInfo?.Method ?? "N/A"}");

                            payment.Item()
                                .Text($"Transaction ID: {paymentInfo?.TransactionId ?? "N/A"}");

                            payment.Item()
                                .Text($"Payment Status: {paymentInfo?.Status ?? "Pending"}");

                            payment.Item()
                                .Text($"Order Status: {order.Status}");
                        });
                });

                // FOOTER
                page.Footer()
                    .AlignCenter()
                    .Column(col =>
                    {
                        col.Item().LineHorizontal(1);

                        col.Item().PaddingTop(10);

                        col.Item()
                            .Text("Thank you for shopping with BeatBox!")
                            .Bold();

                        col.Item()
                            .Text("This is a computer-generated invoice.");

                        col.Item()
                            .Text("support@beatbox.com | www.beatbox.com");
                    });
            });
        });

        return document.GeneratePdf();
    }



    private string BuildOrderConfirmationEmail(
      AppUser user,
      Order order)
    {
        return $@"
<html>
<body style='margin:0;padding:0;background:#f4f6f9;font-family:Segoe UI,Arial,sans-serif;'>

<div style='max-width:700px;margin:30px auto;background:white;border-radius:12px;overflow:hidden;border:1px solid #e5e7eb;'>

    <!-- Header -->
    <div style='background:linear-gradient(135deg,#00f3ff,#6c2bff);padding:30px;text-align:center;color:white;'>

        <h1 style='margin:0;font-size:32px;'>
            🎧 BEATBOX
        </h1>

        <p style='margin-top:8px;font-size:15px;opacity:0.9;'>
            Premium Audio Experience
        </p>

    </div>

    <!-- Content -->
    <div style='padding:35px;'>

        <h2 style='margin-top:0;color:#111827;'>
            Order Confirmed 🎉
        </h2>

        <p style='font-size:15px;color:#374151;'>
            Hi <strong>{user.FullName}</strong>,
        </p>

        <p style='font-size:15px;color:#374151;line-height:1.6;'>
            Thank you for shopping with BeatBox.
            We've successfully received your order and it's now being prepared.
        </p>

        <!-- Order Box -->
        <div style='background:#f8fafc;border-left:5px solid #00f3ff;padding:20px;margin:25px 0;border-radius:8px;'>

            <h3 style='margin-top:0;color:#111827;'>
                Order Details
            </h3>

            <p>
                <strong>Order ID:</strong>
                #{order.OrderId}
            </p>

            <p>
                <strong>Order Date:</strong>
                {order.CreatedDate:dd MMM yyyy hh:mm tt}
            </p>

            <p>
                <strong>Total Amount:</strong>
                ₹{order.TotalAmount:N2}
            </p>

            <p>
                <strong>Status:</strong>

                <span style='
                    background:#fff7ed;
                    color:#f59e0b;
                    padding:8px 16px;
                    border-radius:20px;
                    font-weight:bold;
                    margin-left:10px;'>
                    Pending
                </span>
            </p>

        </div>

        <!-- Shipping Address -->
        <div style='background:#f9fafb;padding:20px;border-radius:8px;margin-bottom:25px;'>

            <h3 style='margin-top:0;color:#111827;'>
                Delivery Address
            </h3>

            <p style='line-height:1.7;color:#374151;'>
                {order.ShippingAddress}
            </p>

        </div>

        <!-- What's Next -->
        <div style='background:#eefcff;padding:20px;border-radius:8px;'>

            <h3 style='margin-top:0;color:#111827;'>
                What happens next?
            </h3>

            <p>✅ Order received</p>
            <p>📦 Preparing your products</p>
            <p>🚚 Shipping update will be emailed soon</p>
            <p>🎉 Delivery within 3-5 business days</p>

        </div>

        <p style='margin-top:30px;color:#374151;'>
            Thank you for choosing BeatBox.
        </p>

    </div>

    <!-- Footer -->
    <div style='background:#111827;color:white;padding:20px;text-align:center;'>

        <h3 style='margin:0;'>BeatBox Audio</h3>

        <p style='margin-top:10px;color:#d1d5db;font-size:13px;'>
            Premium Headphones • Speakers • Audio Accessories
        </p>

        <p style='font-size:12px;color:#9ca3af;margin-top:15px;'>
            This is an automated email from BeatBox.
        </p>

    </div>

</div>

</body>
</html>";
    }




}