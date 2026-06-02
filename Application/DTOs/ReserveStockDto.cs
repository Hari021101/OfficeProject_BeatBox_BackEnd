namespace Application.DTOs;

public class ReserveStockDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public string? UserId { get; set; }
}
