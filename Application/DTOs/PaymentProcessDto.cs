namespace Application.DTOs;

public class PaymentProcessDto
{
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public string Method { get; set; }
    public string TransactionId { get; set; }

}