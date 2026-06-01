using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OtpController : ControllerBase
{
    private readonly IOtpService _otpService;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly ILogger<OtpController> _logger;

    public OtpController(IOtpService otpService, UserManager<AppUser> userManager, ITokenService tokenService, ILogger<OtpController> logger)
    {
        _otpService = otpService;
        _userManager = userManager;
        _tokenService = tokenService;
        _logger = logger;
    }

    // POST /api/otp/send-email
    [HttpPost("send-email")]
    public async Task<IActionResult> SendEmailOtp([FromBody] SendPhoneOtpDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.UserId);
        if (user == null) return NotFound("User not found.");

        await _otpService.SendEmailOtpAsync(user.Id, user.Email!);
        return Ok(new { message = "Email OTP sent. Check your inbox (or server console in DevMode)." });
    }

    // POST /api/otp/verify-email
    [HttpPost("verify-email")]
    public async Task<IActionResult> VerifyEmailOtp([FromBody] OtpVerifyDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.UserId);
        if (user == null) return NotFound("User not found.");

        var valid = await _otpService.VerifyOtpAsync(dto.UserId, dto.Code, Domain.Entities.OtpType.Email);
        if (!valid) return BadRequest(new { message = "Invalid or expired OTP. Please try again." });

        user.IsEmailVerified = true;
        await _userManager.UpdateAsync(user);

        // Verified — issue JWT now
        var token = await _tokenService.CreateToken(user);
        return Ok(new AuthResponseDto
        {
            FullName = user.FullName ?? string.Empty,
            Email = user.Email ?? string.Empty,
            Token = token
        });
    }

    // POST /api/otp/send-phone
    [HttpPost("send-phone")]
    public async Task<IActionResult> SendPhoneOtp([FromBody] SendPhoneOtpDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.UserId);
        if (user == null) return NotFound("User not found.");

        // Store the phone number on the user if provided
        if (!string.IsNullOrWhiteSpace(dto.PhoneNumber))
        {
            user.PhoneNumber = dto.PhoneNumber;
            await _userManager.UpdateAsync(user);
        }

        await _otpService.SendPhoneOtpAsync(user.Id, user.PhoneNumber ?? dto.PhoneNumber);
        return Ok(new { message = "Phone OTP sent. Check your phone (or server console in DevMode)." });
    }

    // POST /api/otp/verify-phone  →  issues JWT on success (final step)
    [HttpPost("verify-phone")]
    public async Task<IActionResult> VerifyPhoneOtp([FromBody] OtpVerifyDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.UserId);
        if (user == null) return NotFound("User not found.");

        var valid = await _otpService.VerifyOtpAsync(dto.UserId, dto.Code, Domain.Entities.OtpType.Phone);
        if (!valid) return BadRequest(new { message = "Invalid or expired OTP. Please try again." });

        user.IsPhoneVerified = true;
        await _userManager.UpdateAsync(user);

        // Both verified — issue JWT now
        var token = await _tokenService.CreateToken(user);
        return Ok(new AuthResponseDto
        {
            FullName = user.FullName ?? string.Empty,
            Email = user.Email ?? string.Empty,
            Token = token
        });
    }
}
