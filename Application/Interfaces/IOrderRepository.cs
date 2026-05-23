using Domain.Entities;

namespace Application.Interfaces;

public interface IOrderRepository
{
    Task<Order> GetOrderByIdAsync(int orderId);
    Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
    Task AddOrderAsync(Order order);
    Task UpdateOrderStatusAsync(int orderId, string status);
    Task<bool> SaveChangesAsync();
}