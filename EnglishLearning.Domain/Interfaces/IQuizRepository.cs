namespace EnglishLearning.Domain.Interfaces;

public interface IQuizRepository : IRepository<Entities.Quiz>
{
    Task<Entities.Quiz?> GetQuizWithQuestionsAsync(Guid id);
    Task<IEnumerable<Entities.Quiz>> GetQuizzesByDifficultyAsync(Enums.DifficultyLevel difficulty);
}
