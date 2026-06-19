namespace Application.DTOs;

public class ProductVariantCreateDto
{
    public string Color { get; set; } = string.Empty;

    public string ColorCode { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public decimal? DiscountPrice { get; set; }

    public int StockQuantity { get; set; }

    public string ImageUrl { get; set; } = string.Empty;
}