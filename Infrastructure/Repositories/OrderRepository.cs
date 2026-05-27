using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order> GetOrderByIdAsync(int orderId)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)   // ← needed for ProductName mapping
            .FirstOrDefaultAsync(o => o.OrderId == orderId);
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
    {
        return await _context.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)   // ← needed for ProductName mapping
            .OrderByDescending(o => o.CreatedDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .OrderByDescending(o => o.CreatedDate)
            .ToListAsync();
    }

    public async Task AddOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
    }

    public async Task UpdateOrderStatusAsync(int orderId, string status)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order != null)
        {
            order.Status = status;
            _context.Orders.Update(order);
        }
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}