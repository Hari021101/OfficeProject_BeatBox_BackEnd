namespace Application.DTOs;

public class CouponResultDto
{
    public bool IsValid { get; set; }

    public string Message { get; set; } = string.Empty;

    public decimal Discount { get; set; }

    public decimal FinalAmount { get; set; }
}