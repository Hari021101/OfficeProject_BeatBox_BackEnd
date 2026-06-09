using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CouponController : ControllerBase
{
    private readonly ICouponService _couponService;

    public CouponController(
        ICouponService couponService)
    {
        _couponService = couponService;
    }

    [HttpPost("apply")]
    public async Task<IActionResult> ApplyCoupon(
        ApplyCouponDto dto)
    {
        var result =
            await _couponService.ApplyCouponAsync(dto);

        return Ok(result);
    }
}