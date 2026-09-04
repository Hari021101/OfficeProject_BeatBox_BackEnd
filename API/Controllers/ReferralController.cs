using Application.DTOs.Referral;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReferralController : ControllerBase
{
    private readonly IReferralService _referralService;

    public ReferralController(IReferralService referralService)
    {
        _referralService = referralService;
    }

    /// <summary>
    /// GET /api/referral/my-code
    /// Obtains or generates the authenticated user's unique referral code.
    /// </summary>
    [HttpGet("my-code")]
    [Authorize]
    public async Task<IActionResult> GetMyCode(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var code = await _referralService.GetUserReferralCodeAsync(userId, cancellationToken);
        return Ok(new { code });
    }

    /// <summary>
    /// GET /api/referral/dashboard
    /// Retrieves the referral progress metrics and history for the authenticated user.
    /// </summary>
    [HttpGet("dashboard")]
    [Authorize]
    public async Task<IActionResult> GetDashboard(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var dashboard = await _referralService.GetReferralDashboardAsync(userId, cancellationToken);
        return Ok(dashboard);
    }

    /// <summary>
    /// POST /api/referral/validate
    /// Validates a referral code against business rules.
    /// </summary>
    [HttpPost("validate")]
    [HttpGet("validate/{code}")]
    [AllowAnonymous]
    public async Task<IActionResult> ValidateCode([FromQuery] string? codeQuery, [FromBody] ValidateReferralRequest? request, [FromRoute] string? code, CancellationToken cancellationToken)
    {
        var targetCode = request?.Code ?? codeQuery ?? code;
        if (string.IsNullOrWhiteSpace(targetCode))
        {
            return BadRequest(new { message = "Referral code is required." });
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _referralService.ValidateReferralCodeAsync(targetCode, userId, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// POST /api/referral/apply
    /// Links a referral code to the authenticated user's account.
    /// </summary>
    [HttpPost("apply")]
    [Authorize]
    public async Task<IActionResult> ApplyReferral([FromBody] ApplyReferralRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        if (string.IsNullOrWhiteSpace(request?.Code))
        {
            return BadRequest(new { message = "Referral code is required." });
        }

        var result = await _referralService.ApplyReferralAsync(request.Code, userId, cancellationToken);
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}
