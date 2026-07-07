using EnglishLearning.Domain.Entities;

namespace EnglishLearning.Domain.Interfaces;

public interface ILearningHistoryRepository : IRepository<LearningHistory>
{
    Task<(List<LearningHistory> Items, int TotalRecords)> GetByUserIdAsync(string userId, int pageNumber, int pageSize);
    Task<List<LearningHistory>> GetRecentByUserIdAsync(string userId, int count);
}