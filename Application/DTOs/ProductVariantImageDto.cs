namespace Application.DTOs;

public class ProductVariantImageDto
{
    public Guid Id { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public bool IsPrimary { get; set; }

    public int DisplayOrder { get; set; }
}
