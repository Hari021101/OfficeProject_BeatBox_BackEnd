namespace Application.DTOs;

public class NotificationDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; }
    public string Type { get; set; } = string.Empty;
    public int? OrderId { get; set; }
    public Guid? ProductId { get; set; }
    public string Icon { get; set; } = string.Empty;
    public string NavigationUrl { get; set; } = string.Empty;
}