using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admin;

[ApiController]
[Route("api/admin/dashboard")]
[Authorize(Roles = "Admin")]
public class DashboardController : ControllerBase
{
    private readonly IAdminDashboardService _service;

    public DashboardController(IAdminDashboardService service)
    {
        _service = service;
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary()
    {
        var result = await _service.GetSummaryAsync();
        return Ok(result);
    }

    [HttpGet("sales")]
    public async Task<IActionResult> GetSales([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] string period = "daily")
    {
        var f = from ?? DateTime.UtcNow.AddDays(-30);
        var t = to ?? DateTime.UtcNow;
        var result = await _service.GetSalesAsync(f, t, period);
        return Ok(result);
    }

    [HttpGet("revenue")]
    public async Task<IActionResult> GetRevenue([FromQuery] int year)
    {
        var y = year == 0 ? DateTime.UtcNow.Year : year;
        var result = await _service.GetRevenueByMonthAsync(y);
        return Ok(result);
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetProducts([FromQuery] int top = 10)
    {
        var topProducts = await _service.GetTopProductsAsync(top);
        var worstProducts = await _service.GetWorstProductsAsync(top);
        return Ok(new { topProducts, worstProducts });
    }

    [HttpGet("low-stock")]
    public async Task<IActionResult> GetLowStock([FromQuery] int take = 10)
    {
        var result = await _service.GetLowStockProductsAsync(take);
        return Ok(result);
    }

    [HttpGet("recent-orders")]
    public async Task<IActionResult> GetRecentOrders([FromQuery] int take = 10)
    {
        var result = await _service.GetRecentOrdersAsync(take);
        return Ok(result);
    }

    [HttpGet("customers")]
    public async Task<IActionResult> GetTopCustomers([FromQuery] int take = 10)
    {
        var result = await _service.GetTopCustomersAsync(take);
        return Ok(result);
    }
}
