namespace Application.DTOs;

public class ApplyCouponDto
{
    public string CouponCode { get; set; } = string.Empty;

    public decimal OrderAmount { get; set; }
}