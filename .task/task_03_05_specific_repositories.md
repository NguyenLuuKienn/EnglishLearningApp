# Task 3.5: Implement Specific Repositories

## Description

Implement specific repository classes for Quiz, Vocabulary, and QuizResult that extend the base Repository and implement custom query methods.

## Priority
🔴 Critical — Custom queries for business logic

## Dependencies
- Task 3.2 (DbContext)
- Task 3.4 (Base Repository)
- Task 1.8 (Specific repository interfaces)

## Files to Create

| File | Action |
|------|--------|
| `Repositories/QuizRepository.cs` | Create |
| `Repositories/VocabularyRepository.cs` | Create |
| `Repositories/QuizResultRepository.cs` | Create |

## Steps

### Step 1: Create QuizRepository
1. Inherit from `Repository<Quiz>` and implement `IQuizRepository`
2. Implement `GetQuizWithQuestionsAsync(Guid id)` — use `Include().ThenInclude()` to eager load Questions and Choices
3. Implement `GetQuizzesByDifficultyAsync(DifficultyLevel difficulty)` — filter by Difficulty property

### Step 2: Create VocabularyRepository
1. Inherit from `Repository<Vocabulary>` and implement `IVocabularyRepository`
2. Implement `GetByWordAsync(string word)` — filter by Word (case-insensitive)
3. Implement `SearchByDifficultyAsync(DifficultyLevel difficulty)` — filter by Difficulty

### Step 3: Create QuizResultRepository
1. Inherit from `Repository<QuizResult>` and implement `IQuizResultRepository`
2. Implement `GetByUserIdAsync(string userId)` — filter by UserId
3. Implement `GetRecentResultsAsync(int count)` — order by CompletedAt descending, take count

## Expected Code

```csharp
// QuizRepository.cs
using Microsoft.EntityFrameworkCore;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Infrastructure.Persistence;

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

// VocabularyRepository.cs
using Microsoft.EntityFrameworkCore;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Infrastructure.Persistence;

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

// QuizResultRepository.cs
using Microsoft.EntityFrameworkCore;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Infrastructure.Persistence;

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
        return await _dbSet
            .OrderByDescending(r => r.CompletedAt)
            .Take(count)
            .ToListAsync();
    }
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Infrastructure` — 0 errors
- [ ] QuizRepository eager loads Questions and Choices
- [ ] VocabularyRepository does case-insensitive word search
- [ ] QuizResultRepository orders by CompletedAt descending

## Acceptance Criteria

- [ ] `QuizRepository` extends `Repository<Quiz>` and implements `IQuizRepository`
- [ ] `GetQuizWithQuestionsAsync` uses Include/ThenInclude for Questions + Choices
- [ ] `GetQuizzesByDifficultyAsync` filters by DifficultyLevel
- [ ] `VocabularyRepository` extends `Repository<Vocabulary>` and implements `IVocabularyRepository`
- [ ] `GetByWordAsync` does case-insensitive comparison
- [ ] `SearchByDifficultyAsync` filters by DifficultyLevel
- [ ] `QuizResultRepository` extends `Repository<QuizResult>` and implements `IQuizResultRepository`
- [ ] `GetByUserIdAsync` filters by UserId, ordered by CompletedAt desc
- [ ] `GetRecentResultsAsync` returns top N recent results
- [ ] Infrastructure project builds successfully
