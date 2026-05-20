namespace Application.DTOs;

public class OrderDto
{
    public int OrderId { get; set; }
    public string UserId { get; set; }
    public string Status { get; set; }
    public string ShippingAddress { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
    public decimal TotalAmount { get; set; }
}