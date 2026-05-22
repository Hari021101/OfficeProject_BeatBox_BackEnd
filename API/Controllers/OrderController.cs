using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("my-orders")]
    public async Task<IActionResult> GetMyOrders()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var orders = await _orderService.GetUserOrdersAsync(userId);
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var order = await _orderService.GetOrderByIdAsync(userId, id);
        return Ok(order);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("status/{id}")]
    public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] OrderStatusUpdateDto orderStatusUpdateDto)
    {
        await _orderService.UpdateOrderStatusAsync(id, orderStatusUpdateDto);
        return Ok(new { Message = "Order status updated successfully." });
    }

    [HttpPut("cancel/{id}")]
    public async Task<IActionResult> CancelOrder(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);//var userId = User.Identity.Name;//var userId = "test-user";//
        await _orderService.CancelOrderAsync(userId, id);
        return Ok(new { Message = "Order cancelled successfully." });
    }
}