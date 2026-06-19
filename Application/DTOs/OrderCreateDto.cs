namespace Application.DTOs;

public class OrderCreateDto
{
    public ShippingAddressDto ShippingAddress { get; set; }

    public string PaymentMethod { get; set; }

    public PaymentDetailsDto? PaymentDetails { get; set; }
    public string? PromoCode { get; set; }
    public decimal DiscountAmount { get; set; }
    public List<OrderCreateItemDto>? Items { get; set; }
}

public class OrderCreateItemDto
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public string? Color { get; set; }
    public string? ColorCode { get; set; }
    public Guid? ProductVariantId { get; set; }
    public string? ProductImageUrl { get; set; }
}

public class ShippingAddressDto
{
    public string FullName { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
}

public class PaymentDetailsDto
{
    public string? RazorpayOrderId { get; set; }
    public string? RazorpayPaymentId { get; set; }
    public string? RazorpaySignature { get; set; }
    public string? TransactionReference { get; set; }

}