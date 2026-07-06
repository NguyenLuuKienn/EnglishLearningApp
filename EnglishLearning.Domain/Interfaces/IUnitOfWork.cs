namespace EnglishLearning.Domain.Interfaces;

public interface IUnitOfWork
{
    IQuizRepository Quizzes { get; }
    IVocabularyRepository Vocabularies { get; }
    IQuizResultRepository QuizResults { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
