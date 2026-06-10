using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
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

    public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository, IMapper mapper, IInventoryService inventoryService, INotificationService notifier, IAdminDashboardService dashboardService)
    {
        _orderRepository = orderRepository;
        _cartRepository = cartRepository;
        _mapper = mapper;
        _inventoryService = inventoryService;
        _notifier = notifier;
        _dashboardService = dashboardService;
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

    public async Task<byte[]> GenerateInvoicePdfAsync(int orderId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);

        if (order == null)
            throw new Exception("Order not found");

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
                            $"Invoice No: BB-{DateTime.Now.Year}-{order.OrderId:D6}");

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
                                .Text("BILL TO")
                                .Bold()
                                .FontSize(14);

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
                        .Width(250)
                        .Border(1)
                        .Padding(10)
                        .Column(total =>
                        {
                            total.Item().Row(row =>
                            {
                                row.RelativeItem().Text("Subtotal");
                                row.ConstantItem(80)
                                    .AlignRight()
                                    .Text($"₹{subtotal:N2}");
                            });

                            total.Item().Row(row =>
                            {
                                row.RelativeItem().Text("GST (18%)");
                                row.ConstantItem(80)
                                    .AlignRight()
                                    .Text($"₹{gst:N2}");
                            });

                            total.Item().Row(row =>
                            {
                                row.RelativeItem().Text("Shipping");
                                row.ConstantItem(80)
                                    .AlignRight()
                                    .Text(shipping == 0
                                        ? "FREE"
                                        : $"₹{shipping:N2}");
                            });

                            total.Item()
                                .PaddingTop(5)
                                .LineHorizontal(1);

                            total.Item()
                                .PaddingTop(5);

                            total.Item().Row(row =>
                            {
                                row.RelativeItem()
                                    .Text("Grand Total")
                                    .Bold();

                                row.ConstantItem(80)
                                    .AlignRight()
                                    .Text($"₹{grandTotal:N2}")
                                    .Bold();
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
                                .Text("PAYMENT INFORMATION")
                                .Bold()
                                .FontSize(14);

                            payment.Item()
                                .Text("Payment Status: Paid");

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
}