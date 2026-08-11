namespace Application.DTOs;

public class PromoValidateRequestDto
{
    public string Code { get; set; } = string.Empty;
    public decimal CartTotal { get; set; }
}

public class PromoValidateResponseDto
{
    public bool IsValid { get; set; }
    public string Code { get; set; } = string.Empty;
    public string DiscountType { get; set; } = "Percentage";
    public decimal DiscountPercentage { get; set; }
    public decimal DiscountAmount { get; set; }
    public bool IsFreeShipping { get; set; }
    public decimal FinalAmount { get; set; }
    public string Message { get; set; } = string.Empty;
}
