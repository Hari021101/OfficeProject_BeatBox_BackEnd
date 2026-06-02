namespace Domain.Entities;

public class InventoryHistory
{
    public Guid Id { get; set; }

    public Guid InventoryId { get; set; }
    public Inventory? Inventory { get; set; }

    public int Change { get; set; }

    public string Reason { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; }

    public string PerformedBy { get; set; } = string.Empty; // user id or system
}
