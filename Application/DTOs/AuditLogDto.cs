using System;

namespace Application.DTOs;

public class AuditLogDto
{
    public Guid Id { get; set; }
    public string AdminId { get; set; } = string.Empty;
    public string AdminName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string Target { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string Icon { get; set; } = string.Empty;
    public string ColorClass { get; set; } = string.Empty;
    public string BgClass { get; set; } = string.Empty;
    public string IPAddress { get; set; } = string.Empty;
}
