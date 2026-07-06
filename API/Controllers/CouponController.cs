using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Microsoft.AspNetCore.Authorization.Authorize]
public class CouponController : ControllerBase
{
    private readonly ICouponService _couponService;

    public CouponController(ICouponService couponService)
    {
        _couponService = couponService;
    }

    // ─── Customer ──────────────────────────────────────────────────────────────

    /// <summary>Returns currently active coupons (for display purposes).</summary>
    [HttpGet]
    public async Task<IActionResult> GetActiveCoupons()
    {
        var coupons = await _couponService.GetActiveCouponsAsync();
        return Ok(coupons);
    }

    /// <summary>Validates and applies a coupon during checkout.</summary>
    [HttpPost("apply")]
    public async Task<IActionResult> ApplyCoupon([FromBody] ApplyCouponDto dto)
    {
        var result = await _couponService.ApplyCouponAsync(dto);
        if (!result.IsValid) return BadRequest(new { message = result.Message });
        return Ok(result);
    }

    // ─── Admin ─────────────────────────────────────────────────────────────────

    /// <summary>Returns ALL coupons including inactive/expired.</summary>
    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllCoupons()
    {
        var coupons = await _couponService.GetAllCouponsAsync();
        return Ok(coupons);
    }

    /// <summary>Returns summary KPI stats for the promotions dashboard.</summary>
    [HttpGet("stats")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetStats()
    {
        var stats = await _couponService.GetStatsAsync();
        return Ok(stats);
    }

    /// <summary>Create a new coupon.</summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CouponCreateDto dto)
    {
        try
        {
            var result = await _couponService.CreateCouponAsync(dto);
            return CreatedAtAction(nameof(GetAllCoupons), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>Update an existing coupon.</summary>
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
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>Delete a coupon.</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _couponService.DeleteCouponAsync(id);
        return NoContent();
    }

    /// <summary>Toggle the IsActive flag of a coupon.</summary>
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
            return BadRequest(new { message = ex.Message });
        }
    }
}