using Domain.Entities;

public class ProductImage
{
    public int Id { get; set; }

    public Guid ProductId { get; set; }

    public string ImageUrl { get; set; }

    public string ColorName { get; set; } = string.Empty;

    public string ColorCode { get; set; } = string.Empty;

    public bool IsPrimary { get; set; }

    public Product Product { get; set; }
}