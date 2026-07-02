using System;

namespace Domain.Entities;

public class ReturnRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    
    public string UserId { get; set; } = string.Empty;
    public AppUser User { get; set; } = null!;
    
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    
    public string Reason { get; set; } = string.Empty;
    
    public string Status { get; set; } = "Pending Approval"; // Pending Approval, Approved, Refunded, Rejected
    
    public string? AdminNotes { get; set; }
    
    public DateTime RequestDate { get; set; } = DateTime.UtcNow;
    public DateTime? ProcessedDate { get; set; }
}
