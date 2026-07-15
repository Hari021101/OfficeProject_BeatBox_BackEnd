using Domain.Entities;

public class CartItem
{
    public int CartItemId { get; set; }

    public int CartId { get; set; }

    public Guid ProductId { get; set; }

    public Guid VariantId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public Cart Cart { get; set; } = null!;

    public Product Product { get; set; } = null!;

    public ProductVariant Variant { get; set; } = null!;
    public string Color { get; set; }
    public string ColorCode { get; set; }
    public Guid? ProductVariantId { get; set; }
    public string ProductImageUrl { get; set; }

    public bool IsPersonalised { get; set; } = false;
    public string? EngravingName { get; set; }
    public string? EngravingDate { get; set; }
    public string? EngravingMessage { get; set; }
    public decimal EngravingPrice { get; set; } = 0.00m;
}