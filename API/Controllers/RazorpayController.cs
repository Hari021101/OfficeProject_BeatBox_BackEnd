using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/razorpay")]
[Microsoft.AspNetCore.Authorization.Authorize]
public class RazorpayController : ControllerBase
{
    private readonly IRazorpayService _service;

    public RazorpayController(IRazorpayService service)
    {
        _service = service;
    }

    [HttpPost("create-order")]
    public async Task<IActionResult> CreateOrder(
        RazorpayOrderDto dto)
    {
        return Ok(await _service.CreateOrderAsync(dto));
    }
}