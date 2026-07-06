using Domain.Entities;

namespace Application.Interfaces;

public interface ICouponRepository
{
    Task<Coupon?> GetByCodeAsync(string code);
    Task<Coupon?> GetByIdAsync(int id);
    Task<IEnumerable<Coupon>> GetAllAsync();
    Task AddAsync(Coupon coupon);
    Task UpdateAsync(Coupon coupon);
    Task DeleteAsync(int id);
    Task<bool> CodeExistsAsync(string code, int? excludeId = null);
}