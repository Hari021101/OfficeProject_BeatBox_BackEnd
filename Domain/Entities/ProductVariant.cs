using Domain.Entities;

public class ProductVariant
{
    public int Id { get; set; }

    public Guid ProductId { get; set; }

    public string ColorName { get; set; }

    public string ColorCode { get; set; }

    public string ImageUrl { get; set; }

    public Product Product { get; set; }
}