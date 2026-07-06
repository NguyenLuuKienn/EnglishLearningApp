namespace EnglishLearning.Domain.Interfaces;

public interface IVocabularyRepository : IRepository<Entities.Vocabulary>
{
    Task<Entities.Vocabulary?> GetByWordAsync(string word);
    Task<IEnumerable<Entities.Vocabulary>> SearchByDifficultyAsync(Enums.DifficultyLevel difficulty);
}
