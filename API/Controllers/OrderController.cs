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

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrdersAsync();
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

    [Authorize(Roles = "Admin")]
    [HttpPost("bulk-status")]
    public async Task<IActionResult> BulkUpdateOrderStatus([FromBody] BulkOrderStatusUpdateDto dto)
    {
        await _orderService.UpdateBulkOrderStatusAsync(dto);
        return Ok(new { Message = "Bulk order status updated successfully." });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("bulk-delete")]
    public async Task<IActionResult> BulkDeleteOrders([FromBody] List<int> orderIds)
    {
        await _orderService.DeleteBulkOrdersAsync(orderIds);
        return Ok(new { Message = "Bulk orders deleted (cancelled) successfully." });
    }

    [HttpPut("cancel/{id}")]
    public async Task<IActionResult> CancelOrder(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);//var userId = User.Identity.Name;//var userId = "test-user";//
        await _orderService.CancelOrderAsync(userId, id);
        return Ok(new { Message = "Order cancelled successfully." });
    }

    [HttpGet("{id}/invoice")]
    public async Task<IActionResult> DownloadInvoice(int id)
    {
        var pdf = await _orderService.GenerateInvoicePdfAsync(id);

        return File(
            pdf,
            "application/pdf",
            $"Invoice-{id}.pdf"
        );
    }
}