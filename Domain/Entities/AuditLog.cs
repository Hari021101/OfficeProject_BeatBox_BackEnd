using System;

namespace Domain.Entities;

public class AuditLog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string AdminId { get; set; } = string.Empty;
    public string AdminName { get; set; } = string.Empty;
    public string Role { get; set; } = "Admin";
    
    public string Action { get; set; } = string.Empty; // CREATED, UPDATED, DELETED, ALERT, REFUNDED
    
    public string Target { get; set; } = string.Empty; // e.g. "Product: BeatBox Soundbar Pro"
    
    public string Details { get; set; } = string.Empty; 
    
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    public string Icon { get; set; } = "Edit"; // lucide-react icon name hint
    public string ColorClass { get; set; } = "text-info";
    public string BgClass { get; set; } = "bg-info";
    
    public string IPAddress { get; set; } = string.Empty;
}
