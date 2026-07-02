namespace Application.DTOs;

public class UserListDto
{
    public string Id { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public IList<string> Roles { get; set; } = new List<string>();
    public bool IsActive { get; set; }
    public DateTime JoinDate { get; set; }
}

public class ToggleStatusResponseDto
{
    public string UserId { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class ToggleRoleResponseDto
{
    public string UserId { get; set; } = string.Empty;
    public string NewRole { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
