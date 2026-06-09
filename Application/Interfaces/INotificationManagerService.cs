using Application.DTOs;

namespace Application.Interfaces;

public interface INotificationManagerService
{
    Task<IEnumerable<NotificationDto>> GetUserNotificationsAsync(string userId);

    Task CreateNotificationAsync(CreateNotificationDto dto);

    Task MarkAsReadAsync(Guid id);
}