namespace Application.DTOs;

public class InvoiceDto
{
    public int OrderId { get; set; }

    public string CustomerName { get; set; }

    public string ShippingAddress { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public List<InvoiceItemDto> Items { get; set; } = new();

    public string PaymentMethod { get; set; }
    public string TransactionId { get; set; }
    public DateTime? PaidDate { get; set; }
}

public class InvoiceItemDto
{
    public string ProductName { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Total => Quantity * UnitPrice;
}