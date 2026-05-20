using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CheckoutController : ControllerBase
{
    private readonly IOrderService _orderService;

    public CheckoutController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> Checkout([FromBody] OrderCreateDto orderCreateDto)
    {
        var userId = "test-user";//var userId = User.Identity.Name;
        var order = await _orderService.CreateOrderAsync(userId, orderCreateDto);
        return Ok(order);
    }
}