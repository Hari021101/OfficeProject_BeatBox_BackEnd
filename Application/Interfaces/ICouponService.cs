using Application.DTOs;

namespace Application.Interfaces;

public interface ICouponService
{
    Task<CouponResultDto> ApplyCouponAsync(
        ApplyCouponDto dto);
}