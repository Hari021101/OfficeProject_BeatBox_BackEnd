namespace Application.DTOs;

public class InventoryDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int AvailableStock { get; set; }
    public int ReservedStock { get; set; }
    public string WarehouseLocation { get; set; } = string.Empty;
    public int LowStockThreshold { get; set; }
    public DateTime LastUpdated { get; set; }
}
