using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class ResetPasswordDto
{
    [Required]
    public string UserId { get; set; } = string.Empty;

    public string? Code { get; set; }

    public string? ResetToken { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
    public string NewPassword { get; set; } = string.Empty;
}
