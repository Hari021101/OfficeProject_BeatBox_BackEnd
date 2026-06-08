using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotificationsController : ControllerBase
{
    private readonly INotificationManagerService _service;

    public NotificationsController(
        INotificationManagerService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var result = await _service.GetUserNotificationsAsync(userId);

        return Ok(result);
    }

    [HttpPost("send")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Send(CreateNotificationDto dto)
    {
        await _service.CreateNotificationAsync(dto);

        return Ok();
    }

    [HttpPut("{id}/read")]
    public async Task<IActionResult> Read(Guid id)
    {
        await _service.MarkAsReadAsync(id);

        return NoContent();
    }
}