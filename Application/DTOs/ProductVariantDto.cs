namespace Application.DTOs;

public class ProductVariantDto
{
    public Guid Id { get; set; }

    public string Color { get; set; } = string.Empty;

    public string ColorCode { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public decimal? DiscountPrice { get; set; }

    public int StockQuantity { get; set; }

    public string Sku { get; set; } = string.Empty;

    public string? Capacity { get; set; }

    public bool IsActive { get; set; }

    public List<ProductVariantImageDto> Images { get; set; } = new();
}