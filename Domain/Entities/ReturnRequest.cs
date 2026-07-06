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
    
    /// <summary>Short reason category (dropdown selection).</summary>
    public string Reason { get; set; } = string.Empty;

    /// <summary>Detailed description from customer.</summary>
    public string? Description { get; set; }

    /// <summary>Comma-separated image URLs or JSON array.</summary>
    public string? ImageUrls { get; set; }

    /// <summary>Refund | Replacement</summary>
    public string? PreferredResolution { get; set; }
    
    /// <summary>Pending Approval | Under Review | Approved | Rejected | Refunded | Replacement Sent | Completed</summary>
    public string Status { get; set; } = "Pending Approval";
    
    public string? AdminNotes { get; set; }
    
    public DateTime RequestDate { get; set; } = DateTime.UtcNow;
    public DateTime? ProcessedDate { get; set; }
}
