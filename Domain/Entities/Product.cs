namespace Domain.Entities;

public class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public decimal? DiscountPrice { get; set; }

    public int StockQuantity { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public Guid CategoryId { get; set; }

    public Category? Category { get; set; }

    public string Brand { get; set; } = string.Empty;

    public double? Rating { get; set; }

    public string BatteryLife { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;

    public string Connectivity { get; set; } = string.Empty;

    public bool IsFeatured { get; set; }
}