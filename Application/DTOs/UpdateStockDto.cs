namespace Application.DTOs;

public class UpdateStockDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public string Reason { get; set; } = string.Empty;
}
