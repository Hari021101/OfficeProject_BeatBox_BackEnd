namespace Application.DTOs;

public class PromoValidateRequestDto
{
    public string Code { get; set; } = string.Empty;
}

public class PromoValidateResponseDto
{
    public bool IsValid { get; set; }
    public decimal DiscountPercentage { get; set; }
    public bool IsFreeShipping { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
