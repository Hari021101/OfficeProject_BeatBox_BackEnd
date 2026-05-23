namespace Application.DTOs;

public class CheckoutDto
{
    public string ShippingAddress { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
}