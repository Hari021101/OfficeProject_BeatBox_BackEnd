using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces;

public interface IAuditLogService
{
    Task<IEnumerable<AuditLogDto>> GetAllLogsAsync();
    Task LogActionAsync(string adminId, string adminName, string action, string target, string details, string icon, string colorClass, string bgClass);
}
