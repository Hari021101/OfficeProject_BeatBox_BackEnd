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

    public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    public async Task<OrderDto> CreateOrderAsync(string userId, OrderCreateDto orderCreateDto)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart == null || !cart.CartItems.Any()) throw new Exception("Cart is empty");

        var order = new Order
        {
            UserId = userId,
            ShippingAddress =
    $"{orderCreateDto.ShippingAddress.FullName}, " +
    $"{orderCreateDto.ShippingAddress.AddressLine1}, " +
    $"{orderCreateDto.ShippingAddress.AddressLine2}, " +
    $"{orderCreateDto.ShippingAddress.City}, " +
    $"{orderCreateDto.ShippingAddress.State}, " +
    $"{orderCreateDto.ShippingAddress.PostalCode}, " +
    $"{orderCreateDto.ShippingAddress.Country}, " +
    $"Phone: {orderCreateDto.ShippingAddress.Phone}",
            CreatedDate = DateTime.UtcNow,
            Status = "Pending",
            TotalAmount = cart.CartItems.Sum(ci => ci.Quantity * ci.UnitPrice),
            OrderItems = cart.CartItems.Select(ci => new OrderItem
            {
                ProductId = ci.ProductId,
                Quantity = ci.Quantity,
                UnitPrice = ci.UnitPrice
            }).ToList()
        };

        await _orderRepository.AddOrderAsync(order);
        await _orderRepository.SaveChangesAsync();

        await _cartRepository.ClearCartAsync(cart.CartId);
        await _cartRepository.SaveChangesAsync();

        return _mapper.Map<OrderDto>(order);
    }

    public async Task<IEnumerable<OrderDto>> GetUserOrdersAsync(string userId)
    {
        var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
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
    }

    public async Task CancelOrderAsync(string userId, int orderId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);
        if (order == null || order.UserId != userId) throw new Exception("Order not found");

        if (order.Status != "Pending" && order.Status != "Processing")
            throw new Exception("Order cannot be cancelled");

        order.Status = "Cancelled";
        await _orderRepository.SaveChangesAsync();
    }
}