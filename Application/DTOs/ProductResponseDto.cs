namespace Application.DTOs;

public class ProductResponseDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public decimal? DiscountPrice { get; set; }

    public int StockQuantity { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public string Brand { get; set; } = string.Empty;

    public double? Rating { get; set; }

    public string BatteryLife { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;

    public string Connectivity { get; set; } = string.Empty;

    public bool IsFeatured { get; set; }
    public int SoldCount { get; set; }

    public int DeliveryDays { get; set; }

    public double AverageRating { get; set; }

    public int ReviewCount { get; set; }

    public List<ProductReviewDto> Reviews { get; set; } = new();

    public List<ProductImageDto> Images { get; set; } = new();

    public List<ProductFaqDto> Faqs { get; set; } = new();
}