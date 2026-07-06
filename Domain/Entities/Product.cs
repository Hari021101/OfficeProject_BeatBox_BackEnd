namespace Domain.Entities;

public class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Guid CategoryId { get; set; }

    public Category? Category { get; set; }

    public string Brand { get; set; } = string.Empty;

    public double? Rating { get; set; }

    public string BatteryLife { get; set; } = string.Empty;

    public string Connectivity { get; set; } = string.Empty;

    public bool IsFeatured { get; set; }

    public int SoldCount { get; set; }

    public int DeliveryDays { get; set; }

    public ICollection<ProductReview> Reviews { get; set; } = new List<ProductReview>();

    public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();

    public ICollection<ProductFaq> Faqs { get; set; } = new List<ProductFaq>();
    public ICollection<ProductVariant> Variants { get; set; }
    = new List<ProductVariant>();
    


}