namespace Application.DTOs;

public class ProductUpdateDto
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public decimal? DiscountPrice { get; set; }

    public int? StockQuantity { get; set; }

    public string? ImageUrl { get; set; }

    public Guid? CategoryId { get; set; }

    public string? Brand { get; set; }

    public string? BatteryLife { get; set; }

    public string? Color { get; set; }

    public string? Connectivity { get; set; }

    public bool? IsFeatured { get; set; }
    public bool? IsEngravingAvailable { get; set; }
    public decimal? EngravingPrice { get; set; }

    public List<ProductFaqDto>? Faqs { get; set; }
}