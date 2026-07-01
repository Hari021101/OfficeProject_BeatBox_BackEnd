namespace Domain.Entities;

public class Notification
{
    public Guid Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string Type { get; set; } = "Info";

    public int? OrderId { get; set; }

    public Guid? ProductId { get; set; }

    public string Icon { get; set; } = "Bell";

    public string NavigationUrl { get; set; } = string.Empty;

    public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; }

    public AppUser? User { get; set; }
}