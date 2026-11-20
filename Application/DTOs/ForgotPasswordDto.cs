using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class ForgotPasswordDto
{
    [Required]
    public string Identifier { get; set; } = string.Empty;
}

