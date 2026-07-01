using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services;

public class NotificationManagerService : INotificationManagerService
{
    private readonly INotificationRepository _repo;
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly ILogger<NotificationManagerService> _logger;

    public NotificationManagerService(
        INotificationRepository repo,
        IHubContext<NotificationHub> hubContext,
        ILogger<NotificationManagerService> logger)
    {
        _repo = repo;
        _hubContext = hubContext;
        _logger = logger;
    }

    public async Task<IEnumerable<NotificationDto>> GetUserNotificationsAsync(string userId)
    {
        var notifications = await _repo.GetUserNotificationsAsync(userId);

        return notifications.Select(x => new NotificationDto
        {
            Id = x.Id,
            Title = x.Title,
            Message = x.Message,
            IsRead = x.IsRead,
            CreatedAt = x.CreatedAt,
            Type = x.Type,
            OrderId = x.OrderId,
            ProductId = x.ProductId,
            Icon = x.Icon,
            NavigationUrl = x.NavigationUrl
        });
    }

    public async Task CreateNotificationAsync(CreateNotificationDto dto)
    {
        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            UserId = dto.UserId,
            Title = dto.Title,
            Message = dto.Message,
            IsRead = false,
            CreatedAt = DateTime.UtcNow,
            Type = dto.Type ?? "Info",
            OrderId = dto.OrderId,
            ProductId = dto.ProductId,
            Icon = dto.Icon ?? "Bell",
            NavigationUrl = dto.NavigationUrl ?? string.Empty
        };

        await _repo.AddAsync(notification);

        try
        {
            var notificationDto = new NotificationDto
            {
                Id = notification.Id,
                Title = notification.Title,
                Message = notification.Message,
                IsRead = notification.IsRead,
                CreatedAt = notification.CreatedAt,
                Type = notification.Type,
                OrderId = notification.OrderId,
                ProductId = notification.ProductId,
                Icon = notification.Icon,
                NavigationUrl = notification.NavigationUrl
            };
            await _hubContext.Clients.User(notification.UserId).SendAsync("ReceiveNotification", notificationDto);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "SignalR notification failed in CreateNotificationAsync");
        }
    }

    public async Task MarkAsReadAsync(Guid id)
    {
        var notification = await _repo.GetByIdAsync(id);

        if (notification == null)
            throw new Exception("Notification not found");

        notification.IsRead = true;

        await _repo.UpdateAsync(notification);
    }

    public async Task<IEnumerable<NotificationDto>> GetUnreadUserNotificationsAsync(string userId)
    {
        var notifications = await _repo.GetUnreadUserNotificationsAsync(userId);

        return notifications.Select(x => new NotificationDto
        {
            Id = x.Id,
            Title = x.Title,
            Message = x.Message,
            IsRead = x.IsRead,
            CreatedAt = x.CreatedAt,
            Type = x.Type,
            OrderId = x.OrderId,
            ProductId = x.ProductId,
            Icon = x.Icon,
            NavigationUrl = x.NavigationUrl
        });
    }

    public async Task MarkAllAsReadAsync(string userId)
    {
        await _repo.MarkAllAsReadAsync(userId);
    }

    public async Task DeleteNotificationAsync(Guid id)
    {
        var notification = await _repo.GetByIdAsync(id);
        if (notification != null)
        {
            await _repo.DeleteAsync(notification);
        }
    }
}