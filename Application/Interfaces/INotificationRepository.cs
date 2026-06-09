using Domain.Entities;

namespace Application.Interfaces;

public interface INotificationRepository
{
    Task<IEnumerable<Notification>> GetUserNotificationsAsync(string userId);

    Task<Notification?> GetByIdAsync(Guid id);

    Task AddAsync(Notification notification);

    Task UpdateAsync(Notification notification);
}