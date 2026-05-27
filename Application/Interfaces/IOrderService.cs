using Application.DTOs;

namespace Application.Interfaces;

public interface IOrderService
{
    Task<OrderDto> CreateOrderAsync(string userId, OrderCreateDto orderCreateDto);
    Task<IEnumerable<OrderDto>> GetUserOrdersAsync(string userId);
    Task<OrderDto> GetOrderByIdAsync(string userId, int orderId);
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
    Task UpdateOrderStatusAsync(int orderId, OrderStatusUpdateDto orderStatusUpdateDto);
    Task CancelOrderAsync(string userId, int orderId);
}