using System;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/auditlogs")]
[Route("api/auditlog")]
[Authorize(Roles = "Admin")]
public class AuditLogController : ControllerBase
{
    private readonly IAuditLogService _auditLogService;

    public AuditLogController(IAuditLogService auditLogService)
    {
        _auditLogService = auditLogService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAuditLogs(
        [FromQuery] string? searchTerm,
        [FromQuery] string? entityType,
        [FromQuery] string? actionType,
        [FromQuery] string? user,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50)
    {
        var logs = await _auditLogService.GetFilteredLogsAsync(
            searchTerm, entityType, actionType, user, startDate, endDate, page, pageSize);
        return Ok(logs);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string searchTerm, [FromQuery] int page = 1, [FromQuery] int pageSize = 50)
    {
        var logs = await _auditLogService.GetFilteredLogsAsync(
            searchTerm, null, null, null, null, null, page, pageSize);
        return Ok(logs);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> Filter(
        [FromQuery] string? entityType,
        [FromQuery] string? actionType,
        [FromQuery] string? user,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50)
    {
        var logs = await _auditLogService.GetFilteredLogsAsync(
            null, entityType, actionType, user, startDate, endDate, page, pageSize);
        return Ok(logs);
    }
}
