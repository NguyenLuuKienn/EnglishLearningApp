# Task 3.6: Implement Unit of Work

## Description

Implement the `UnitOfWork` class that coordinates all repositories and provides a single `SaveChangesAsync` method for transactional consistency.

## Priority
🔴 Critical — Transactional consistency across repositories

## Dependencies
- Task 3.2 (DbContext)
- Task 3.4 (Base Repository)
- Task 3.5 (Specific Repositories)
- Task 1.8 (IUnitOfWork interface)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Infrastructure/UnitOfWork/UnitOfWork.cs` | Create |

## Steps

### Step 1: Create UnitOfWork class
1. Implement `IUnitOfWork` interface
2. Constructor accepts `ApplicationDbContext`
3. Create lazy-initialized repository properties:
   - `_quizRepository` → `Quizzes` property
   - `_vocabularyRepository` → `Vocabularies` property
   - `_quizResultRepository` → `QuizResults` property

### Step 2: Implement SaveChangesAsync
1. Call `_context.SaveChangesAsync(cancellationToken)`

## Expected Code

```csharp
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
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Infrastructure` — 0 errors
- [ ] All repository properties use lazy initialization (`??=`)
- [ ] `SaveChangesAsync` delegates to DbContext

## Acceptance Criteria

- [ ] `UnitOfWork` implements `IUnitOfWork`
- [ ] Constructor accepts `ApplicationDbContext`
- [ ] `Quizzes` property returns `IQuizRepository` (lazy initialized)
- [ ] `Vocabularies` property returns `IVocabularyRepository` (lazy initialized)
- [ ] `QuizResults` property returns `IQuizResultRepository` (lazy initialized)
- [ ] `SaveChangesAsync` calls `_context.SaveChangesAsync()`
- [ ] Infrastructure project builds successfully
