namespace Application.DTOs;

public class CouponDto
{
    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public decimal DiscountAmount { get; set; }

    public decimal? DiscountPercentage { get; set; }

    public decimal MinimumOrderAmount { get; set; }

    public DateTime ExpiryDate { get; set; }

    public bool IsActive { get; set; }

    public int UsageLimit { get; set; }

    public int UsedCount { get; set; }
}