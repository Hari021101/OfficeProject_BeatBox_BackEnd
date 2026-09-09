namespace Application.DTOs;

public class OrderDto
{
    public int OrderId { get; set; }
    public string UserId { get; set; }
    public string Status { get; set; }
    public string ShippingAddress { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime OrderDate { get; set; }   // Alias used by frontend
    public List<OrderItemDto> Items { get; set; } = new();
    public decimal TotalAmount { get; set; }
    public string PaymentMethod { get; set; }
    public string PaymentStatus { get; set; }
    public string? Color { get; set; }

    public string? ColorCode { get; set; }

    public Guid? ProductVariantId { get; set; }

    public string? ProductImageUrl { get; set; }

    public string? PromoCode { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal ShippingAmount { get; set; }
}