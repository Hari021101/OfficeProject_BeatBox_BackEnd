using System;

namespace Application.DTOs;

public class ReturnRequestDto
{
    public Guid Id { get; set; }
    public int OrderId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>Short reason category.</summary>
    public string Reason { get; set; } = string.Empty;

    /// <summary>Detailed description from the customer.</summary>
    public string? Description { get; set; }

    /// <summary>Comma-separated image URLs.</summary>
    public string? ImageUrls { get; set; }

    /// <summary>Refund | Replacement</summary>
    public string? PreferredResolution { get; set; }

    public string Status { get; set; } = string.Empty;
    public string? AdminNotes { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime? ProcessedDate { get; set; }
}
