namespace Domain.Entities;

public class ProductImage
{
    public int Id { get; set; }

    public Guid ProductId { get; set; }

    public string ImageUrl { get; set; }

    public bool IsPrimary { get; set; }

    public Product Product { get; set; }
}