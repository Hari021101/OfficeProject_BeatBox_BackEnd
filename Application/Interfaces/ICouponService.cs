using Application.DTOs;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface ICouponService
{
    // Customer-facing
    Task<PromoValidateResponseDto> ValidatePromoCodeAsync(PromoValidateRequestDto dto);
    Task<CouponResultDto> ApplyCouponAsync(ApplyCouponDto dto);
    Task<IEnumerable<Coupon>> GetActiveCouponsAsync();

    // Admin CRUD
    Task<IEnumerable<CouponDto>> GetAllCouponsAsync();
    Task<CouponDto> GetByIdAsync(int id);
    Task<CouponDto> CreateCouponAsync(CouponCreateDto dto);
    Task<CouponDto> UpdateCouponAsync(int id, CouponCreateDto dto);
    Task DeleteCouponAsync(int id);
    Task<CouponDto> ToggleActiveAsync(int id);

    // Stats
    Task<CouponStatsDto> GetStatsAsync();
}