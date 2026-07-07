using EnglishLearning.Domain.Entities;

namespace EnglishLearning.Domain.Interfaces;

public interface INotificationRepository : IRepository<Notification>
{
    Task<(List<Notification> Items, int TotalRecords)> GetByUserIdAsync(string userId, int pageNumber, int pageSize, bool? isRead = null);
    Task<int> GetUnreadCountAsync(string userId);
}
