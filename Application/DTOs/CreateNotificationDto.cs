namespace Application.DTOs;

public class CreateNotificationDto
{
    public string UserId { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string? Type { get; set; }

    public int? OrderId { get; set; }

    public Guid? ProductId { get; set; }

    public string? Icon { get; set; }

    public string? NavigationUrl { get; set; }
}