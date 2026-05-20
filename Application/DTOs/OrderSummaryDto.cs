namespace Application.DTOs;

public class OrderSummaryDto
{
    public decimal Subtotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
}