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
            BgClass = x.BgClass,
            IPAddress = x.IPAddress
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
            BgClass = bgClass,
            IPAddress = "127.0.0.1"
        };

        _context.AuditLogs.Add(log);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<AuditLogDto>> GetFilteredLogsAsync(string? searchTerm, string? entityType, string? actionType, string? user, DateTime? startDate, DateTime? endDate, int pageNumber, int pageSize)
    {
        var query = _context.AuditLogs.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var term = searchTerm.Trim().ToLower();
            query = query.Where(x => x.AdminName.ToLower().Contains(term) ||
                                     x.Target.ToLower().Contains(term) ||
                                     x.Details.ToLower().Contains(term) ||
                                     x.Action.ToLower().Contains(term));
        }

        if (!string.IsNullOrWhiteSpace(entityType))
        {
            var type = entityType.Trim().ToLower();
            query = query.Where(x => x.Target.ToLower().StartsWith(type + ":") || x.Target.ToLower().Contains(type));
        }

        if (!string.IsNullOrWhiteSpace(actionType))
        {
            var act = actionType.Trim().ToLower();
            query = query.Where(x => x.Action.ToLower() == act);
        }

        if (!string.IsNullOrWhiteSpace(user))
        {
            var usr = user.Trim().ToLower();
            query = query.Where(x => x.AdminName.ToLower().Contains(usr) || x.AdminId.ToLower() == usr);
        }

        if (startDate.HasValue)
        {
            query = query.Where(x => x.Timestamp >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query = query.Where(x => x.Timestamp <= endDate.Value);
        }

        var logs = await query
            .OrderByDescending(x => x.Timestamp)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
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
            BgClass = x.BgClass,
            IPAddress = x.IPAddress
        });
    }
}
