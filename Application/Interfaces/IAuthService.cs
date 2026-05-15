using Application.DTOs;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterDto dto);

    Task<string?> LoginAsync(LoginDto dto);
}