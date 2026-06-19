namespace Domain.Entities;

public class ProductVariantImage
{
    public Guid Id { get; set; }

    public Guid VariantId { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public bool IsPrimary { get; set; }

    public int DisplayOrder { get; set; }

    public ProductVariant Variant { get; set; } = null!;
}
