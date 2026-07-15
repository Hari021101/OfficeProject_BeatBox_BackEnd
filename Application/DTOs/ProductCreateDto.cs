namespace Application.DTOs;

public class ProductCreateDto
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public List<ProductVariantCreateDto> Variants { get; set; }
        = new();

    public Guid CategoryId { get; set; }

    public string Brand { get; set; } = string.Empty;

    public string BatteryLife { get; set; } = string.Empty;

    public string Connectivity { get; set; } = string.Empty;

    public bool IsFeatured { get; set; }
    public bool IsEngravingAvailable { get; set; } = false;
    public decimal EngravingPrice { get; set; } = 99.00m;

    public List<ProductFaqDto> Faqs { get; set; } = new();
}