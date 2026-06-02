using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Inventory
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public int AvailableStock { get; set; }

    public int ReservedStock { get; set; }

    public string WarehouseLocation { get; set; } = string.Empty;

    public int LowStockThreshold { get; set; }

    public DateTime LastUpdated { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();

    public ICollection<InventoryHistory> History { get; set; } = new List<InventoryHistory>();
}
