namespace Domain.Entities;

public class ProductVariant
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public string Color { get; set; } = string.Empty;

    public string ColorCode { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public decimal? DiscountPrice { get; set; }

    public int StockQuantity { get; set; }

    // Each variant can have multiple images (front, side, lifestyle, etc.)
    public ICollection<ProductVariantImage> Images { get; set; } = new List<ProductVariantImage>();

    public Product Product { get; set; } = null!;
}