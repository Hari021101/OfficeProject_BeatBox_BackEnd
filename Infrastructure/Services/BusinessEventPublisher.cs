using Application.Common.Events;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public class BusinessEventPublisher : IBusinessEventPublisher
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEmailService _emailService;
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly ILogger<BusinessEventPublisher> _logger;
    private readonly ITransactionActionQueue _actionQueue;

    public BusinessEventPublisher(
        AppDbContext context,
        IHttpContextAccessor httpContextAccessor,
        IEmailService emailService,
        IHubContext<NotificationHub> hubContext,
        ILogger<BusinessEventPublisher> logger,
        ITransactionActionQueue actionQueue)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _emailService = emailService;
        _hubContext = hubContext;
        _logger = logger;
        _actionQueue = actionQueue;
    }

    public async Task PublishAsync(BusinessEvent businessEvent)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        string performedByUserId = "System";
        string performedByName = "System";
        string performedByRole = "System";
        string ipAddress = "127.0.0.1";

        if (httpContext != null)
        {
            ipAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";
            var user = httpContext.User;
            if (user != null && user.Identity != null && user.Identity.IsAuthenticated)
            {
                performedByUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "System";
                performedByName = user.Identity.Name ?? user.FindFirst(ClaimTypes.Name)?.Value ?? "System";
                performedByRole = user.FindFirst(ClaimTypes.Role)?.Value ?? "User";
            }
        }

        // Fallback for user registration/profile events where identity context is empty/establishing
        if (performedByUserId == "System" && businessEvent.EntityType == "User" && !string.IsNullOrEmpty(businessEvent.EntityId))
        {
            performedByUserId = businessEvent.EntityId;
            performedByName = businessEvent.Title;
            performedByRole = "Customer";
        }

        // 1. Audit Log record
        var auditLog = new AuditLog
        {
            Id = Guid.NewGuid(),
            AdminId = performedByUserId,
            AdminName = performedByName,
            Role = performedByRole,
            Action = businessEvent.ActionType,
            Target = $"{businessEvent.EntityType}: {businessEvent.EntityId}",
            Details = businessEvent.Description,
            Timestamp = DateTime.UtcNow,
            Icon = businessEvent.Icon,
            ColorClass = businessEvent.ColorClass,
            BgClass = businessEvent.BgClass,
            IPAddress = ipAddress
        };
        _context.AuditLogs.Add(auditLog);

        // 2. In-App Notification record (if user-specific)
        Notification? notification = null;
        if (!string.IsNullOrEmpty(businessEvent.UserId))
        {
            notification = new Notification
            {
                Id = Guid.NewGuid(),
                UserId = businessEvent.UserId,
                Title = businessEvent.NotificationTitle ?? businessEvent.Title,
                Message = businessEvent.NotificationMessage ?? businessEvent.Description,
                IsRead = false,
                CreatedAt = DateTime.UtcNow,
                Type = businessEvent.NotificationType ?? "Info",
                OrderId = businessEvent.OrderId,
                ProductId = businessEvent.ProductId,
                Icon = businessEvent.Icon,
                NavigationUrl = businessEvent.NavigationUrl ?? string.Empty
            };
            _context.Notifications.Add(notification);
        }

        // Save DB context changes within current transaction scope
        await _context.SaveChangesAsync();

        Func<Task> dispatchAction = async () =>
        {
            // 3. Realtime SignalR dispatch
            if (notification != null)
            {
                var notif = notification; // local copy
                _ = Task.Run(async () =>
                {
                    try
                    {
                        var dto = new NotificationDto
                        {
                            Id = notif.Id,
                            Title = notif.Title,
                            Message = notif.Message,
                            IsRead = notif.IsRead,
                            CreatedAt = notif.CreatedAt,
                            Type = notif.Type,
                            OrderId = notif.OrderId,
                            ProductId = notif.ProductId,
                            Icon = notif.Icon,
                            NavigationUrl = notif.NavigationUrl
                        };
                        await _hubContext.Clients.User(notif.UserId).SendAsync("ReceiveNotification", dto);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "SignalR notification broadcast failed for user {UserId}", notif.UserId);
                    }
                });
            }

            // Live admin alerts
            if (businessEvent.EntityType == "Inventory" && businessEvent.ActionType == "ALERT")
            {
                _ = Task.Run(async () =>
                {
                    try
                    {
                        await _hubContext.Clients.Group("Admins").SendAsync("LowStockAlert", new
                        {
                            ProductId = businessEvent.ProductId,
                            AvailableStock = 0 // handled by frontend mapping
                        });
                    }
                    catch {}
                });
            }
            else if (businessEvent.EntityType == "Order" && businessEvent.ActionType == "CREATED")
            {
                _ = Task.Run(async () =>
                {
                    try
                    {
                        await _hubContext.Clients.Group("Admins").SendAsync("NewOrderPlaced", new
                        {
                            OrderId = businessEvent.OrderId
                        });
                    }
                    catch {}
                });
            }

            // 4. Email notification dispatch
            if (!string.IsNullOrEmpty(businessEvent.SendEmailTo) && !string.IsNullOrEmpty(businessEvent.EmailBody))
            {
                var emailTo = businessEvent.SendEmailTo;
                var subject = businessEvent.EmailSubject ?? businessEvent.Title;
                var body = businessEvent.EmailBody;
                _ = Task.Run(async () =>
                {
                    try
                    {
                        await _emailService.SendEmailAsync(emailTo, subject, body);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to send business email to {Email}", emailTo);
                    }
                });
            }

            await Task.CompletedTask;
        };

        if (_context.Database.CurrentTransaction != null)
        {
            _actionQueue.QueueAction(dispatchAction);
        }
        else
        {
            await dispatchAction();
        }
    }
}
