using System;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ReturnController : ControllerBase
{
    private readonly IReturnService _returnService;

    public ReturnController(IReturnService returnService)
    {
        _returnService = returnService;
    }

    /// <summary>Admin: get all return requests.</summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var requests = await _returnService.GetAllRequestsAsync();
        return Ok(requests);
    }

    /// <summary>Customer: check if a return already exists for an order.</summary>
    [HttpGet("order/{orderId:int}")]
    public async Task<IActionResult> GetByOrder(int orderId)
    {
        var result = await _returnService.GetByOrderIdAsync(orderId);
        if (result == null) return Ok(null);
        return Ok(result);
    }

    /// <summary>Customer: create a new return request.</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ReturnRequestDto dto)
    {
        var result = await _returnService.CreateRequestAsync(dto);
        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }

    /// <summary>Admin: update the status of a return request.</summary>
    [HttpPut("{id:guid}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateReturnStatusDto request)
    {
        var result = await _returnService.UpdateRequestStatusAsync(id, request.Status, request.AdminNotes);
        return Ok(result);
    }
}

public class UpdateReturnStatusDto
{
    public string Status { get; set; } = string.Empty;
    public string? AdminNotes { get; set; }
}
