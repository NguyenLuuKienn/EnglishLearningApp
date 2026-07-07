using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearning.Infrastructure.Repositories;

public class LeaderboardRepository(ApplicationDbContext context)
    : Repository<Leaderboard>(context), ILeaderboardRepository
{
    public async Task<Leaderboard?> GetByUserIdAsync(string userId)
    {
        return await _dbSet.FirstOrDefaultAsync(l => l.UserId == userId);
    }

    public async Task<List<Leaderboard>> GetTopUsersAsync(int count)
    {
        return await _dbSet.OrderByDescending(l => l.TotalScore).Take(count).ToListAsync();
    }

    public async Task<int> GetRankByUserIdAsync(string userId)
    {
        var user = await _dbSet.FirstOrDefaultAsync(l => l.UserId == userId);
        if (user == null) return -1;

        var count = await _dbSet.CountAsync(l => l.TotalScore > user.TotalScore);
        return count + 1;
    }
}
