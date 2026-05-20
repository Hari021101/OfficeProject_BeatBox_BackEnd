namespace Application.DTOs;

public class PaymentDto
{
    public int PaymentId { get; set; }
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
    public string Method { get; set; }
    public string TransactionId { get; set; }
    public DateTime CreatedDate { get; set; }
}