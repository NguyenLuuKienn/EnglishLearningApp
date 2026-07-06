using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Infrastructure.Persistence;
using EnglishLearning.Infrastructure.Repositories;

namespace EnglishLearning.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private QuizRepository? _quizRepository;
    private VocabularyRepository? _vocabularyRepository;
    private QuizResultRepository? _quizResultRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQuizRepository Quizzes => _quizRepository ??= new QuizRepository(_context);
    public IVocabularyRepository Vocabularies => _vocabularyRepository ??= new VocabularyRepository(_context);
    public IQuizResultRepository QuizResults => _quizResultRepository ??= new QuizResultRepository(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
