namespace Application.DTOs;

public class CartDto
{
    public int CartId { get; set; }
    public List<CartItemDto> Items { get; set; } = new();
    public decimal TotalAmount => Items.Sum(item => item.TotalPrice);
}