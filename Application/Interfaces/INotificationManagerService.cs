using Application.DTOs;

namespace Application.Interfaces;

public interface INotificationManagerService
{
    Task<IEnumerable<NotificationDto>> GetUserNotificationsAsync(string userId);

    Task CreateNotificationAsync(CreateNotificationDto dto);

    Task MarkAsReadAsync(Guid id);

    Task<IEnumerable<NotificationDto>> GetUnreadUserNotificationsAsync(string userId);

    Task MarkAllAsReadAsync(string userId);

    Task DeleteNotificationAsync(Guid id);
}