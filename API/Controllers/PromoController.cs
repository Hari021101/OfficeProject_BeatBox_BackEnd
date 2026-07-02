using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Microsoft.AspNetCore.Authorization.Authorize]
public class PromoController : ControllerBase
{
    [HttpPost("validate")]
    public IActionResult ValidatePromo([FromBody] PromoValidateRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Code))
            return BadRequest(new { Message = "Promo code is required." });

        var code = request.Code.ToUpperInvariant();

        // Mock validation
        if (code == "BEATBOX10")
        {
            return Ok(new PromoValidateResponseDto
            {
                IsValid = true,
                DiscountPercentage = 10,
                Code = code,
                Message = "10% off applied!"
            });
        }
        
        if (code == "FREESHIP")
        {
            return Ok(new PromoValidateResponseDto
            {
                IsValid = true,
                DiscountPercentage = 0,
                IsFreeShipping = true,
                Code = code,
                Message = "Free shipping applied!"
            });
        }
        
        if (code == "SUMMER50")
        {
            return Ok(new PromoValidateResponseDto
            {
                IsValid = true,
                DiscountPercentage = 50,
                Code = code,
                Message = "Massive 50% summer discount applied!"
            });
        }

        return BadRequest(new { Message = "Invalid or expired promo code." });
    }
}
