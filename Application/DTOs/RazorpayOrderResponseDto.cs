namespace Application.DTOs;

public class RazorpayOrderResponseDto
{
    public string RazorpayOrderId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
}