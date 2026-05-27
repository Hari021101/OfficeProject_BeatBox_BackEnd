using Application.DTOs;

namespace Application.Interfaces;

public interface IProfileService
{
    Task<UserProfileDto> GetProfileAsync(string userId);
    Task UpdateProfileAsync(string userId, UserProfileUpdateDto dto);
    Task ChangePasswordAsync(string userId, ChangePasswordDto dto);
}
