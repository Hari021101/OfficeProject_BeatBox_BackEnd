using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public class CouponService : ICouponService
{
    private readonly ICouponRepository _couponRepository;
    private readonly AppDbContext _context;

    public CouponService(ICouponRepository couponRepository, AppDbContext context)
    {
        _couponRepository = couponRepository;
        _context = context;
    }

    // ─── Helpers ──────────────────────────────────────────────────────────────

    private static string DeriveStatus(Coupon c)
    {
        var now = DateTime.UtcNow;
        if (!c.IsActive) return "Expired";
        if (c.ExpiryDate <= now) return "Expired";
        if (c.UsageLimit > 0 && c.UsedCount >= c.UsageLimit) return "Expired";
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
        CreatedDate = c.CreatedDate,
        UpdatedDate = c.UpdatedDate,
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
            (c.UsageLimit == 0 || c.UsedCount < c.UsageLimit) &&
            (!c.StartDate.HasValue || c.StartDate.Value <= now));
    }

    public async Task<PromoValidateResponseDto> ValidatePromoCodeAsync(PromoValidateRequestDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Code))
        {
            return new PromoValidateResponseDto
            {
                IsValid = false,
                Message = "Promo code is required."
            };
        }

        var normalizedCode = dto.Code.Trim().ToUpperInvariant();
        var coupon = await _couponRepository.GetByCodeAsync(normalizedCode);

        if (coupon == null)
        {
            return new PromoValidateResponseDto
            {
                IsValid = false,
                Code = normalizedCode,
                Message = "Invalid promo code."
            };
        }

        if (!coupon.IsActive)
        {
            return new PromoValidateResponseDto
            {
                IsValid = false,
                Code = coupon.Code,
                Message = "Promo code is not active."
            };
        }

        var now = DateTime.UtcNow;
        if (coupon.StartDate.HasValue && coupon.StartDate.Value > now)
        {
            return new PromoValidateResponseDto
            {
                IsValid = false,
                Code = coupon.Code,
                Message = "Promo code is not active yet."
            };
        }

        if (coupon.ExpiryDate <= now)
        {
            return new PromoValidateResponseDto
            {
                IsValid = false,
                Code = coupon.Code,
                Message = "Promo code has expired."
            };
        }

        if (coupon.UsageLimit > 0 && coupon.UsedCount >= coupon.UsageLimit)
        {
            return new PromoValidateResponseDto
            {
                IsValid = false,
                Code = coupon.Code,
                Message = "Promo code usage limit has been reached."
            };
        }

        if (dto.CartTotal < coupon.MinimumOrderAmount)
        {
            return new PromoValidateResponseDto
            {
                IsValid = false,
                Code = coupon.Code,
                Message = $"Minimum order amount is ₹{coupon.MinimumOrderAmount:N0}."
            };
        }

        decimal discountAmount = 0;
        bool isFreeShipping = false;
        string message = "";

        if (string.Equals(coupon.DiscountType, "Shipping", StringComparison.OrdinalIgnoreCase))
        {
            isFreeShipping = true;
            discountAmount = 0;
            message = "Free shipping applied!";
        }
        else if (string.Equals(coupon.DiscountType, "Percentage", StringComparison.OrdinalIgnoreCase) || coupon.DiscountPercentage.HasValue)
        {
            var pct = coupon.DiscountPercentage ?? 0;
            discountAmount = Math.Round(dto.CartTotal * pct / 100m, 2);
            if (coupon.MaximumDiscount.HasValue && discountAmount > coupon.MaximumDiscount.Value)
            {
                discountAmount = coupon.MaximumDiscount.Value;
            }
            message = $"{pct}% discount applied!";
        }
        else
        {
            discountAmount = Math.Min(dto.CartTotal, coupon.DiscountAmount);
            message = $"₹{discountAmount:N0} discount applied!";
        }

        decimal finalAmount = Math.Max(0, dto.CartTotal - discountAmount);

        // DO NOT increment UsedCount here. Read-only validation.
        return new PromoValidateResponseDto
        {
            IsValid = true,
            Code = coupon.Code,
            DiscountType = coupon.DiscountType,
            DiscountPercentage = coupon.DiscountPercentage ?? 0,
            DiscountAmount = discountAmount,
            IsFreeShipping = isFreeShipping,
            FinalAmount = finalAmount,
            Message = message
        };
    }

    public async Task<CouponResultDto> ApplyCouponAsync(ApplyCouponDto dto)
    {
        var validation = await ValidatePromoCodeAsync(new PromoValidateRequestDto
        {
            Code = dto.CouponCode,
            CartTotal = dto.OrderAmount
        });

        if (!validation.IsValid)
        {
            return new CouponResultDto
            {
                IsValid = false,
                Message = validation.Message
            };
        }

        return new CouponResultDto
        {
            IsValid = true,
            Discount = validation.DiscountAmount,
            FinalAmount = validation.FinalAmount,
            IsFreeShipping = validation.IsFreeShipping,
            Message = validation.Message
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
        if (string.IsNullOrWhiteSpace(dto.Code))
            throw new Exception("Coupon code is required");

        var normalizedCode = dto.Code.Trim().ToUpperInvariant();

        if (await _couponRepository.CodeExistsAsync(normalizedCode))
            throw new Exception($"Coupon code '{normalizedCode}' already exists");

        if (string.Equals(dto.DiscountType, "Percentage", StringComparison.OrdinalIgnoreCase))
        {
            if (dto.DiscountPercentage is null or <= 0 or > 100)
                throw new Exception("Percentage discount must be between 1 and 100%");
        }
        else if (string.Equals(dto.DiscountType, "Fixed", StringComparison.OrdinalIgnoreCase))
        {
            if (dto.DiscountAmount <= 0)
                throw new Exception("Fixed discount amount must be greater than 0");
        }

        if (dto.MinimumOrderAmount < 0)
            throw new Exception("Minimum order amount cannot be negative");

        if (dto.MaximumDiscount.HasValue && dto.MaximumDiscount.Value < 0)
            throw new Exception("Maximum discount cannot be negative");

        if (dto.UsageLimit < 0)
            throw new Exception("Usage limit cannot be negative");

        if (dto.StartDate.HasValue && dto.StartDate.Value >= dto.ExpiryDate)
            throw new Exception("Expiry date must be after start date");

        var coupon = new Coupon
        {
            Code = normalizedCode,
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
            UsedCount = 0,
            CreatedDate = DateTime.UtcNow
        };

        await _couponRepository.AddAsync(coupon);
        return ToDto(coupon);
    }

    public async Task<CouponDto> UpdateCouponAsync(int id, CouponCreateDto dto)
    {
        var coupon = await _couponRepository.GetByIdAsync(id)
            ?? throw new Exception($"Coupon {id} not found");

        if (string.IsNullOrWhiteSpace(dto.Code))
            throw new Exception("Coupon code is required");

        var normalizedCode = dto.Code.Trim().ToUpperInvariant();

        if (await _couponRepository.CodeExistsAsync(normalizedCode, id))
            throw new Exception($"Coupon code '{normalizedCode}' is already used by another coupon");

        if (string.Equals(dto.DiscountType, "Percentage", StringComparison.OrdinalIgnoreCase))
        {
            if (dto.DiscountPercentage is null or <= 0 or > 100)
                throw new Exception("Percentage discount must be between 1 and 100%");
        }
        else if (string.Equals(dto.DiscountType, "Fixed", StringComparison.OrdinalIgnoreCase))
        {
            if (dto.DiscountAmount <= 0)
                throw new Exception("Fixed discount amount must be greater than 0");
        }

        if (dto.MinimumOrderAmount < 0)
            throw new Exception("Minimum order amount cannot be negative");

        if (dto.MaximumDiscount.HasValue && dto.MaximumDiscount.Value < 0)
            throw new Exception("Maximum discount cannot be negative");

        if (dto.UsageLimit < 0)
            throw new Exception("Usage limit cannot be negative");

        if (dto.StartDate.HasValue && dto.StartDate.Value >= dto.ExpiryDate)
            throw new Exception("Expiry date must be after start date");

        coupon.Code = normalizedCode;
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
        coupon.UpdatedDate = DateTime.UtcNow;

        await _couponRepository.UpdateAsync(coupon);
        return ToDto(coupon);
    }

    public async Task DeleteCouponAsync(int id)
    {
        var coupon = await _couponRepository.GetByIdAsync(id);
        if (coupon == null) return;

        // Soft delete/disable if the coupon was ever used in completed/placed orders
        bool hasBeenUsed = await _context.Orders.AnyAsync(o => o.PromoCode == coupon.Code);
        if (hasBeenUsed || coupon.UsedCount > 0)
        {
            coupon.IsActive = false;
            coupon.UpdatedDate = DateTime.UtcNow;
            await _couponRepository.UpdateAsync(coupon);
        }
        else
        {
            await _couponRepository.DeleteAsync(id);
        }
    }

    public async Task<CouponDto> ToggleActiveAsync(int id)
    {
        var coupon = await _couponRepository.GetByIdAsync(id)
            ?? throw new Exception($"Coupon {id} not found");

        coupon.IsActive = !coupon.IsActive;
        coupon.UpdatedDate = DateTime.UtcNow;
        await _couponRepository.UpdateAsync(coupon);
        return ToDto(coupon);
    }

    // ─── Stats ────────────────────────────────────────────────────────────────

    public async Task<CouponStatsDto> GetStatsAsync()
    {
        var all = (await _couponRepository.GetAllAsync()).ToList();
        var now = DateTime.UtcNow;

        var totalDiscountGivenFromOrders = await _context.Orders
            .Where(o => o.Status != "Cancelled" && o.DiscountAmount > 0)
            .SumAsync(o => (decimal?)o.DiscountAmount) ?? 0m;

        return new CouponStatsDto
        {
            ActiveCount = all.Count(c => c.IsActive && c.ExpiryDate > now && (c.UsageLimit == 0 || c.UsedCount < c.UsageLimit) && (!c.StartDate.HasValue || c.StartDate.Value <= now)),
            ExpiredCount = all.Count(c => !c.IsActive || c.ExpiryDate <= now || (c.UsageLimit > 0 && c.UsedCount >= c.UsageLimit)),
            ScheduledCount = all.Count(c => c.IsActive && c.StartDate.HasValue && c.StartDate.Value > now && c.ExpiryDate > now),
            TotalRedemptions = all.Sum(c => c.UsedCount),
            TotalDiscountGiven = totalDiscountGivenFromOrders
        };
    }
}