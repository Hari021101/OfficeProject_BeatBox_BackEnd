using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class VerifyResetCodeDto
{
    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public string Code { get; set; } = string.Empty;
}
