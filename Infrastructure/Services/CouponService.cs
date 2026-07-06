using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public class CouponService : ICouponService
{
    private readonly ICouponRepository _couponRepository;

    public CouponService(ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository;
    }

    // ─── Helpers ──────────────────────────────────────────────────────────────

    private static string DeriveStatus(Coupon c)
    {
        var now = DateTime.UtcNow;
        if (c.ExpiryDate < now) return "Expired";
        if (c.StartDate.HasValue && c.StartDate.Value > now) return "Scheduled";
        return "Active";
    }

    private static CouponDto ToDto(Coupon c) => new CouponDto
    {
        Id = c.Id,
        Code = c.Code,
        Description = c.Description,
        DiscountType = c.DiscountType,
        DiscountAmount = c.DiscountAmount,
        DiscountPercentage = c.DiscountPercentage,
        MinimumOrderAmount = c.MinimumOrderAmount,
        MaximumDiscount = c.MaximumDiscount,
        StartDate = c.StartDate,
        ExpiryDate = c.ExpiryDate,
        IsActive = c.IsActive,
        UsageLimit = c.UsageLimit,
        UsedCount = c.UsedCount,
        Status = DeriveStatus(c)
    };

    // ─── Customer-facing ──────────────────────────────────────────────────────

    public async Task<IEnumerable<Coupon>> GetActiveCouponsAsync()
    {
        var coupons = await _couponRepository.GetAllAsync();
        var now = DateTime.UtcNow;
        return coupons.Where(c =>
            c.IsActive &&
            c.ExpiryDate > now &&
            (!c.StartDate.HasValue || c.StartDate.Value <= now));
    }

    public async Task<CouponResultDto> ApplyCouponAsync(ApplyCouponDto dto)
    {
        var coupon = await _couponRepository.GetByCodeAsync(dto.CouponCode);

        if (coupon == null)
            return new CouponResultDto { IsValid = false, Message = "Invalid coupon code" };

        if (!coupon.IsActive)
            return new CouponResultDto { IsValid = false, Message = "Coupon is disabled" };

        var now = DateTime.UtcNow;
        if (coupon.ExpiryDate < now)
            return new CouponResultDto { IsValid = false, Message = "Coupon has expired" };

        if (coupon.StartDate.HasValue && coupon.StartDate.Value > now)
            return new CouponResultDto { IsValid = false, Message = "Coupon is not yet active" };

        if (coupon.UsageLimit > 0 && coupon.UsedCount >= coupon.UsageLimit)
            return new CouponResultDto { IsValid = false, Message = "Coupon usage limit reached" };

        if (dto.OrderAmount < coupon.MinimumOrderAmount)
            return new CouponResultDto { IsValid = false, Message = $"Minimum order ₹{coupon.MinimumOrderAmount}" };

        decimal discount = 0;

        if (coupon.DiscountType == "Shipping")
        {
            return new CouponResultDto
            {
                IsValid = true,
                Discount = 0,
                IsFreeShipping = true,
                FinalAmount = dto.OrderAmount,
                Message = "Free shipping applied!"
            };
        }
        else if (coupon.DiscountType == "Percentage" || coupon.DiscountPercentage.HasValue)
        {
            var pct = coupon.DiscountPercentage ?? 0;
            discount = dto.OrderAmount * pct / 100;
            if (coupon.MaximumDiscount.HasValue && discount > coupon.MaximumDiscount.Value)
                discount = coupon.MaximumDiscount.Value;
        }
        else
        {
            discount = coupon.DiscountAmount;
        }

        // Increment usage count
        coupon.UsedCount++;
        await _couponRepository.UpdateAsync(coupon);

        return new CouponResultDto
        {
            IsValid = true,
            Discount = discount,
            FinalAmount = dto.OrderAmount - discount,
            Message = "Coupon applied successfully"
        };
    }

    // ─── Admin CRUD ───────────────────────────────────────────────────────────

    public async Task<IEnumerable<CouponDto>> GetAllCouponsAsync()
    {
        var coupons = await _couponRepository.GetAllAsync();
        return coupons.Select(ToDto);
    }

    public async Task<CouponDto> GetByIdAsync(int id)
    {
        var coupon = await _couponRepository.GetByIdAsync(id)
            ?? throw new Exception($"Coupon {id} not found");
        return ToDto(coupon);
    }

    public async Task<CouponDto> CreateCouponAsync(CouponCreateDto dto)
    {
        // Uniqueness check
        if (await _couponRepository.CodeExistsAsync(dto.Code.ToUpper()))
            throw new Exception($"Coupon code '{dto.Code}' already exists");

        // Validation
        if (dto.DiscountType == "Percentage" && dto.DiscountPercentage is > 100)
            throw new Exception("Percentage discount cannot exceed 100%");

        if (dto.StartDate.HasValue && dto.StartDate.Value >= dto.ExpiryDate)
            throw new Exception("Start date must be before expiry date");

        if (dto.UsageLimit < 0)
            throw new Exception("Usage limit cannot be negative");

        var coupon = new Coupon
        {
            Code = dto.Code.ToUpper().Trim(),
            Description = dto.Description,
            DiscountType = dto.DiscountType,
            DiscountAmount = dto.DiscountAmount,
            DiscountPercentage = dto.DiscountPercentage,
            MinimumOrderAmount = dto.MinimumOrderAmount,
            MaximumDiscount = dto.MaximumDiscount,
            StartDate = dto.StartDate,
            ExpiryDate = dto.ExpiryDate,
            IsActive = dto.IsActive,
            UsageLimit = dto.UsageLimit,
            UsedCount = 0
        };

        await _couponRepository.AddAsync(coupon);
        return ToDto(coupon);
    }

    public async Task<CouponDto> UpdateCouponAsync(int id, CouponCreateDto dto)
    {
        var coupon = await _couponRepository.GetByIdAsync(id)
            ?? throw new Exception($"Coupon {id} not found");

        // Uniqueness check (exclude self)
        if (await _couponRepository.CodeExistsAsync(dto.Code.ToUpper(), id))
            throw new Exception($"Coupon code '{dto.Code}' already used by another coupon");

        if (dto.DiscountType == "Percentage" && dto.DiscountPercentage is > 100)
            throw new Exception("Percentage discount cannot exceed 100%");

        if (dto.StartDate.HasValue && dto.StartDate.Value >= dto.ExpiryDate)
            throw new Exception("Start date must be before expiry date");

        coupon.Code = dto.Code.ToUpper().Trim();
        coupon.Description = dto.Description;
        coupon.DiscountType = dto.DiscountType;
        coupon.DiscountAmount = dto.DiscountAmount;
        coupon.DiscountPercentage = dto.DiscountPercentage;
        coupon.MinimumOrderAmount = dto.MinimumOrderAmount;
        coupon.MaximumDiscount = dto.MaximumDiscount;
        coupon.StartDate = dto.StartDate;
        coupon.ExpiryDate = dto.ExpiryDate;
        coupon.IsActive = dto.IsActive;
        coupon.UsageLimit = dto.UsageLimit;

        await _couponRepository.UpdateAsync(coupon);
        return ToDto(coupon);
    }

    public async Task DeleteCouponAsync(int id)
    {
        await _couponRepository.DeleteAsync(id);
    }

    public async Task<CouponDto> ToggleActiveAsync(int id)
    {
        var coupon = await _couponRepository.GetByIdAsync(id)
            ?? throw new Exception($"Coupon {id} not found");
        coupon.IsActive = !coupon.IsActive;
        await _couponRepository.UpdateAsync(coupon);
        return ToDto(coupon);
    }

    // ─── Stats ────────────────────────────────────────────────────────────────

    public async Task<CouponStatsDto> GetStatsAsync()
    {
        var all = (await _couponRepository.GetAllAsync()).ToList();
        var now = DateTime.UtcNow;

        return new CouponStatsDto
        {
            ActiveCount = all.Count(c => c.IsActive && c.ExpiryDate > now && (!c.StartDate.HasValue || c.StartDate.Value <= now)),
            ExpiredCount = all.Count(c => c.ExpiryDate < now),
            ScheduledCount = all.Count(c => c.StartDate.HasValue && c.StartDate.Value > now),
            TotalRedemptions = all.Sum(c => c.UsedCount),
            TotalDiscountGiven = all.Sum(c => c.UsedCount * (c.DiscountPercentage.HasValue ? 0 : c.DiscountAmount))
        };
    }
}