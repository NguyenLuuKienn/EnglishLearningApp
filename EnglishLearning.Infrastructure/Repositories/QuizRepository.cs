using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearning.Infrastructure.Repositories;

public class QuizRepository : Repository<Quiz>, IQuizRepository
{
    public QuizRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Quiz?> GetQuizWithQuestionsAsync(Guid id)
    {
        return await _dbSet
            .Include(q => q.Questions)
                .ThenInclude(q => q.Choices)
            .FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IEnumerable<Quiz>> GetQuizzesByDifficultyAsync(DifficultyLevel difficulty)
    {
        return await _dbSet
            .Where(q => q.Difficulty == difficulty)
            .ToListAsync();
    }
}
