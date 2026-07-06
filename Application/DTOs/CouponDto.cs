namespace Application.DTOs;

public class CouponDto
{
    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string? Description { get; set; }

    /// <summary>Percentage | Fixed | Shipping</summary>
    public string DiscountType { get; set; } = "Percentage";

    public decimal DiscountAmount { get; set; }

    public decimal? DiscountPercentage { get; set; }

    public decimal MinimumOrderAmount { get; set; }

    public decimal? MaximumDiscount { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime ExpiryDate { get; set; }

    public bool IsActive { get; set; }

    public int UsageLimit { get; set; }

    public int UsedCount { get; set; }

    /// <summary>Derived status: Active | Expired | Scheduled</summary>
    public string Status { get; set; } = string.Empty;
}

public class CouponCreateDto
{
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string DiscountType { get; set; } = "Percentage";
    public decimal DiscountAmount { get; set; }
    public decimal? DiscountPercentage { get; set; }
    public decimal MinimumOrderAmount { get; set; }
    public decimal? MaximumDiscount { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsActive { get; set; }
    public int UsageLimit { get; set; }
}

public class CouponStatsDto
{
    public int ActiveCount { get; set; }
    public int ExpiredCount { get; set; }
    public int ScheduledCount { get; set; }
    public int TotalRedemptions { get; set; }
    public decimal TotalDiscountGiven { get; set; }
}