using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var profile = await _profileService.GetProfileAsync(userId);
        return Ok(profile);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProfile([FromBody] UserProfileUpdateDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _profileService.UpdateProfileAsync(userId, dto);
        return Ok(new { Message = "Profile updated successfully." });
    }

    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _profileService.ChangePasswordAsync(userId, dto);
        return Ok(new { Message = "Password changed successfully." });
    }
}
