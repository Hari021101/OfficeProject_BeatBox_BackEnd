using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PromoController : ControllerBase
{
    private readonly ICouponService _couponService;

    public PromoController(ICouponService couponService)
    {
        _couponService = couponService;
    }

    /// <summary>Validates a promo code against active database coupons (Customer-facing).</summary>
    [HttpPost("validate")]
    public async Task<IActionResult> ValidatePromo([FromBody] PromoValidateRequestDto request)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(authUserId))
            {
                request.UserId = authUserId;
            }
        }
        var result = await _couponService.ValidatePromoCodeAsync(request);
        if (!result.IsValid)
        {
            return BadRequest(new { Message = result.Message, IsValid = false, Code = result.Code });
        }
        return Ok(result);
    }

    /// <summary>Returns ALL coupons for admin dashboard.</summary>
    [HttpGet]
    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllCoupons()
    {
        var coupons = await _couponService.GetAllCouponsAsync();
        return Ok(coupons);
    }

    /// <summary>Returns a single coupon by ID.</summary>
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var coupon = await _couponService.GetByIdAsync(id);
            return Ok(coupon);
        }
        catch (Exception ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }

    /// <summary>Returns summary KPI statistics for promotions dashboard.</summary>
    [HttpGet("stats")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetStats()
    {
        var stats = await _couponService.GetStatsAsync();
        return Ok(stats);
    }

    /// <summary>Creates a new coupon (Admin).</summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CouponCreateDto dto)
    {
        try
        {
            var result = await _couponService.CreateCouponAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    /// <summary>Updates an existing coupon (Admin).</summary>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] CouponCreateDto dto)
    {
        try
        {
            var result = await _couponService.UpdateCouponAsync(id, dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    /// <summary>Deletes or soft-disables a coupon (Admin).</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _couponService.DeleteCouponAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    /// <summary>Toggles the IsActive status of a coupon (Admin).</summary>
    [HttpPatch("{id:int}/toggle")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Toggle(int id)
    {
        try
        {
            var result = await _couponService.ToggleActiveAsync(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}
