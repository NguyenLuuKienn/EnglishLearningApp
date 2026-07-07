using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearning.Infrastructure.Repositories;

public class NotificationRepository(ApplicationDbContext context)
    : Repository<Notification>(context), INotificationRepository
{
    public async Task<(List<Notification> Items, int TotalRecords)> GetByUserIdAsync(string userId, int pageNumber, int pageSize, bool? isRead = null)
    {
        var filtered = _dbSet.Where(n => n.UserId == userId && (isRead == null || n.IsRead == isRead));
        var totalRecords = await filtered.CountAsync();

        var items = await filtered
            .OrderByDescending(n => n.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalRecords);
    }

    public async Task<int> GetUnreadCountAsync(string userId)
    {
        return await _dbSet.CountAsync(n => n.UserId == userId && !n.IsRead);
    }
}
