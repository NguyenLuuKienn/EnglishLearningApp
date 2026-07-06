using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearning.Infrastructure.Repositories;

public class QuizResultRepository : Repository<QuizResult>, IQuizResultRepository
{
    public QuizResultRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<QuizResult>> GetByUserIdAsync(string userId)
    {
        return await _dbSet
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.CompletedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<QuizResult>> GetRecentResultsAsync(int count = 10)
    {
        if (count <= 0)
            throw new ArgumentOutOfRangeException(nameof(count), CommonErrorMessages.CountInvalid);

        return await _dbSet
            .OrderByDescending(r => r.CompletedAt)
            .Take(count)
            .ToListAsync();
    }
}
