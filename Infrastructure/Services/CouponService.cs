using Application.DTOs;
using Application.Interfaces;

namespace Infrastructure.Services;

public class CouponService : ICouponService
{
    private readonly ICouponRepository _couponRepository;

    public CouponService(
        ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository;
    }

    public async Task<CouponResultDto> ApplyCouponAsync(
        ApplyCouponDto dto)
    {
        var coupon =
            await _couponRepository.GetByCodeAsync(
                dto.CouponCode);

        if (coupon == null)
        {
            return new CouponResultDto
            {
                IsValid = false,
                Message = "Invalid Coupon"
            };
        }

        if (!coupon.IsActive)
        {
            return new CouponResultDto
            {
                IsValid = false,
                Message = "Coupon Disabled"
            };
        }

        if (coupon.ExpiryDate < DateTime.UtcNow)
        {
            return new CouponResultDto
            {
                IsValid = false,
                Message = "Coupon Expired"
            };
        }

        if (dto.OrderAmount < coupon.MinimumOrderAmount)
        {
            return new CouponResultDto
            {
                IsValid = false,
                Message =
                    $"Minimum order ₹{coupon.MinimumOrderAmount}"
            };
        }

        decimal discount = 0;

        if (coupon.DiscountPercentage.HasValue)
        {
            discount =
                dto.OrderAmount *
                coupon.DiscountPercentage.Value / 100;
        }
        else
        {
            discount = coupon.DiscountAmount;
        }

        return new CouponResultDto
        {
            IsValid = true,
            Discount = discount,
            FinalAmount = dto.OrderAmount - discount,
            Message = "Coupon Applied Successfully"
        };
    }
}