using System;

namespace Domain.Entities;

public class StockNotificationSubscription
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public Guid ProductId { get; set; }
    public Guid ProductVariantId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
    public DateTime? NotificationSentAt { get; set; }

    // Navigation properties
    public virtual AppUser? User { get; set; }
    public virtual Product? Product { get; set; }
    public virtual ProductVariant? Variant { get; set; }
}
