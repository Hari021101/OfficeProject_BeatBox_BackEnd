using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CheckoutController : ControllerBase
{
    private readonly IOrderService _orderService;

    public CheckoutController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // POST /api/checkout
    // Accepts full OrderCreateDto matching frontend object structure
    [HttpPost]
    public async Task<IActionResult> Checkout([FromBody] OrderCreateDto orderCreateDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var order = await _orderService.CreateOrderAsync(userId, orderCreateDto);
        return Ok(order);
    }
}