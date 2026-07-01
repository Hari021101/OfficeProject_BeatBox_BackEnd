using System;

namespace Application.Common.Events;

public class BusinessEvent
{
    public string ActionType { get; set; } = string.Empty; // CREATED, UPDATED, DELETED, ORDER, etc.
    public string EntityType { get; set; } = string.Empty; // Product, Order, etc.
    public string EntityId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = "Info";
    public string ColorClass { get; set; } = "text-info";
    public string BgClass { get; set; } = "bg-info";

    // In-app Notification info (optional)
    public string? UserId { get; set; }
    public string? NotificationTitle { get; set; }
    public string? NotificationMessage { get; set; }
    public string? NotificationType { get; set; }
    public int? OrderId { get; set; }
    public Guid? ProductId { get; set; }
    public string? NavigationUrl { get; set; }

    // Email info (optional)
    public string? SendEmailTo { get; set; }
    public string? EmailSubject { get; set; }
    public string? EmailBody { get; set; }
}
