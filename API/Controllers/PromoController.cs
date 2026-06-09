using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PromoController : ControllerBase
{
    [HttpPost("validate")]
    public IActionResult ValidatePromo([FromBody] PromoValidateRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Code))
            return BadRequest(new { Message = "Promo code is required." });

        var code = request.Code.ToUpperInvariant();

        // Mock validation
        if (code == "BEATBOX10")
        {
            return Ok(new PromoValidateResponse
            {
                IsValid = true,
                DiscountPercentage = 10,
                Code = code,
                Message = "10% off applied!"
            });
        }
        
        if (code == "FREESHIP")
        {
            return Ok(new PromoValidateResponse
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
            return Ok(new PromoValidateResponse
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

public class PromoValidateRequest
{
    public string Code { get; set; }
}

public class PromoValidateResponse
{
    public bool IsValid { get; set; }
    public decimal DiscountPercentage { get; set; }
    public bool IsFreeShipping { get; set; }
    public string Code { get; set; }
    public string Message { get; set; }
}
