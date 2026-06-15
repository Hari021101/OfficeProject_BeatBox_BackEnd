using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AuditLogService : IAuditLogService
{
    private readonly AppDbContext _context;

    public AuditLogService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AuditLogDto>> GetAllLogsAsync()
    {
        var logs = await _context.AuditLogs
            .OrderByDescending(x => x.Timestamp)
            .ToListAsync();

        return logs.Select(x => new AuditLogDto
        {
            Id = x.Id,
            AdminId = x.AdminId,
            AdminName = x.AdminName,
            Role = x.Role,
            Action = x.Action,
            Target = x.Target,
            Details = x.Details,
            Timestamp = x.Timestamp,
            Icon = x.Icon,
            ColorClass = x.ColorClass,
            BgClass = x.BgClass
        });
    }

    public async Task LogActionAsync(string adminId, string adminName, string action, string target, string details, string icon, string colorClass, string bgClass)
    {
        var log = new AuditLog
        {
            AdminId = adminId,
            AdminName = adminName,
            Action = action,
            Target = target,
            Details = details,
            Icon = icon,
            ColorClass = colorClass,
            BgClass = bgClass
        };

        _context.AuditLogs.Add(log);
        await _context.SaveChangesAsync();
    }
}
