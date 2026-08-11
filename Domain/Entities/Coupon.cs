namespace Domain.Entities;

public class Coupon
{
    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;

    /// <summary>Optional human-readable description.</summary>
    public string? Description { get; set; }

    /// <summary>Percentage | Fixed | Shipping</summary>
    public string DiscountType { get; set; } = "Percentage";

    public decimal DiscountAmount { get; set; }

    public decimal? DiscountPercentage { get; set; }

    public decimal MinimumOrderAmount { get; set; }

    /// <summary>Cap on discount amount (relevant for percentage discounts).</summary>
    public decimal? MaximumDiscount { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime ExpiryDate { get; set; }

    public bool IsActive { get; set; }

    public int UsageLimit { get; set; }

    public int UsedCount { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedDate { get; set; }
}