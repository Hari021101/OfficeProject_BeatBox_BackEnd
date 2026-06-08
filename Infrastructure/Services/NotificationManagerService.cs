using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Services;

public class NotificationManagerService : INotificationManagerService
{
    private readonly INotificationRepository _repo;

    public NotificationManagerService(
        INotificationRepository repo)
    {
        _repo = repo;
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
            CreatedAt = x.CreatedAt
        });
    }

    public async Task CreateNotificationAsync(CreateNotificationDto dto)
    {
        await _repo.AddAsync(new Notification
        {
            Id = Guid.NewGuid(),
            UserId = dto.UserId,
            Title = dto.Title,
            Message = dto.Message,
            IsRead = false,
            CreatedAt = DateTime.UtcNow
        });
    }

    public async Task MarkAsReadAsync(Guid id)
    {
        var notification = await _repo.GetByIdAsync(id);

        if (notification == null)
            throw new Exception("Notification not found");

        notification.IsRead = true;

        await _repo.UpdateAsync(notification);
    }
}