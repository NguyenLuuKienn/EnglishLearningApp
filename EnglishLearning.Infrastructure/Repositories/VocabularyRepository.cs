using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearning.Infrastructure.Repositories;

public class VocabularyRepository : Repository<Vocabulary>, IVocabularyRepository
{
    public VocabularyRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Vocabulary?> GetByWordAsync(string word)
    {
        return await _dbSet
            .FirstOrDefaultAsync(v => v.Word.Equals(word, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<IEnumerable<Vocabulary>> SearchByDifficultyAsync(DifficultyLevel difficulty)
    {
        return await _dbSet
            .Where(v => v.Difficulty == difficulty)
            .ToListAsync();
    }
}
