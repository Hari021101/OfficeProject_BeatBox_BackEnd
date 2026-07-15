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

    public bool IsPersonalised { get; set; }
    public string? EngravingName { get; set; }
    public string? EngravingDate { get; set; }
    public string? EngravingMessage { get; set; }
    public decimal EngravingPrice { get; set; }

    public decimal TotalPrice => Quantity * (UnitPrice + (IsPersonalised ? EngravingPrice : 0));
}