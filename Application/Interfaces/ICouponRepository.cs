using Domain.Entities;

namespace Application.Interfaces;

public interface ICouponRepository
{
    Task<Coupon?> GetByCodeAsync(string code);

    Task<IEnumerable<Coupon>> GetAllAsync();

    Task AddAsync(Coupon coupon);

    Task UpdateAsync(Coupon coupon);
}