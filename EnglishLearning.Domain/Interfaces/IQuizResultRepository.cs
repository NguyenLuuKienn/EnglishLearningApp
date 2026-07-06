namespace EnglishLearning.Domain.Interfaces;

public interface IQuizResultRepository : IRepository<Entities.QuizResult>
{
    Task<IEnumerable<Entities.QuizResult>> GetByUserIdAsync(string userId);
    Task<IEnumerable<Entities.QuizResult>> GetRecentResultsAsync(int count = 10);
}
