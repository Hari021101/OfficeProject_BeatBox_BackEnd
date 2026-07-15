public class OrderItemDto
{
    public int OrderItemId { get; set; }
    public Guid ProductId { get; set; }
    public string? ProductName { get; set; }

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public string? Color { get; set; }
    public string? ColorCode { get; set; }
    public Guid? ProductVariantId { get; set; }
    public string? ProductImageUrl { get; set; }

    public bool IsPersonalised { get; set; }
    public string? EngravingName { get; set; }
    public string? EngravingDate { get; set; }
    public string? EngravingMessage { get; set; }
    public decimal EngravingPrice { get; set; }

    public decimal TotalPrice => Quantity * (UnitPrice + (IsPersonalised ? EngravingPrice : 0));
}