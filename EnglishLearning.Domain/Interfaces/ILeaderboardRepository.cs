using EnglishLearning.Domain.Entities;

namespace EnglishLearning.Domain.Interfaces;

public interface ILeaderboardRepository : IRepository<Leaderboard>
{
    Task<Leaderboard?> GetByUserIdAsync(string userId);
    Task<List<Leaderboard>> GetTopUsersAsync(int count);
    Task<int> GetRankByUserIdAsync(string userId);
}