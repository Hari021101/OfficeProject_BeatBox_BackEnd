using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _service;

    public InventoryController(IInventoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetByProduct(Guid productId)
    {
        var result = await _service.GetByProductIdAsync(productId);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPut("update-stock")]
    public async Task<IActionResult> UpdateStock([FromBody] UpdateStockDto dto)
    {
        await _service.UpdateStockAsync(dto, User?.Identity?.Name ?? "system");
        return NoContent();
    }

    [HttpPost("reserve")]
    [Authorize]
    public async Task<IActionResult> Reserve([FromBody] ReserveStockDto dto)
    {
        await _service.ReserveStockAsync(dto);
        return Ok();
    }

    [HttpPost("release")]
    [Authorize]
    public async Task<IActionResult> Release([FromBody] ReserveStockDto dto)
    {
        await _service.ReleaseStockAsync(dto);
        return Ok();
    }

    [HttpGet("low-stock")]
    public async Task<IActionResult> LowStock()
    {
        var result = await _service.GetLowStockAsync();
        return Ok(result);
    }


    [HttpGet("logs")]
    public async Task<IActionResult> Logs()
    {
        var logs = await _service.GetInventoryLogsAsync();
        return Ok(logs);
    }

}
