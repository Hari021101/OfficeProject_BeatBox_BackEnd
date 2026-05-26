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
    // Accepts: { "shippingAddress": "Full address string" }
    [HttpPost]
    public async Task<IActionResult> Checkout([FromBody] CheckoutDto checkoutDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // Build OrderCreateDto from the simple string payload the frontend sends
        var orderCreateDto = new OrderCreateDto
        {
            ShippingAddress = new ShippingAddressDto
            {
                FullName     = string.Empty,
                AddressLine1 = checkoutDto.ShippingAddress ?? string.Empty,
                AddressLine2 = string.Empty,
                City         = string.Empty,
                State        = string.Empty,
                PostalCode   = string.Empty,
                Country      = "India",
                Phone        = string.Empty
            },
            PaymentMethod  = "Online",
            PaymentDetails = new PaymentDetailsDto()
        };

        var order = await _orderService.CreateOrderAsync(userId, orderCreateDto);
        return Ok(order);
    }
}