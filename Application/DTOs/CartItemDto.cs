public class CartItemDto
{
    public int CartItemId { get; set; }

    public Guid ProductId { get; set; }

    public Guid VariantId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public string ProductImage { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice => Quantity * UnitPrice;
}