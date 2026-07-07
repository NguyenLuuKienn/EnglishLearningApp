using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearning.Infrastructure.Repositories;

public class LearningHistoryRepository(ApplicationDbContext context)
    : Repository<LearningHistory>(context), ILearningHistoryRepository
{
    public async Task<(List<LearningHistory> Items, int TotalRecords)> GetByUserIdAsync(string userId, int pageNumber, int pageSize)
    {
        var filtered = _dbSet.Where(h => h.UserId == userId);
        var totalRecords = await filtered.CountAsync();

        var items = await filtered
            .OrderByDescending(h => h.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalRecords);
    }

    public async Task<List<LearningHistory>> GetRecentByUserIdAsync(string userId, int count)
    {
        return await _dbSet
            .Where(h => h.UserId == userId)
            .OrderByDescending(h => h.CreatedAt)
            .Take(count)
            .ToListAsync();
    }
}
