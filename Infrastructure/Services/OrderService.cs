using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

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

        var order = new Order
        {
            UserId          = userId,
            ShippingAddress = shippingAddress,
            CreatedDate     = DateTime.UtcNow,
            Status          = "Pending",
            TotalAmount     = cart.CartItems.Sum(ci => ci.Quantity * ci.UnitPrice),
            OrderItems      = cart.CartItems.Select(ci => new OrderItem
            {
                ProductId = ci.ProductId,
                Quantity  = ci.Quantity,
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
}