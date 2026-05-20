using Domain.Entities;

namespace Application.Interfaces;

public interface IPaymentRepository
{
    Task<Payment> GetPaymentByOrderIdAsync(int orderId);
    Task AddPaymentAsync(Payment payment);
    Task<bool> SaveChangesAsync();
}