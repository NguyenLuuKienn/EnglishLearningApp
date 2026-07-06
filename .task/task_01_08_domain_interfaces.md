# Task 1.8: Add Domain Repository Interfaces

## Description

Create repository and Unit of Work interfaces in the Domain layer. These define the contract that Infrastructure will implement.

## Priority
🔴 Critical — Foundation for data access layer

## Dependencies
- Task 1.0 (BaseEntity)
- Task 1.2 (Vocabulary entity)
- Task 1.3 (Quiz entity)
- Task 1.4 (Question entity)
- Task 1.5 (Choice entity)
- Task 1.6 (QuizResult entity)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Domain/Interfaces/IRepository.cs` | Create |
| `EnglishLearning.Domain/Interfaces/IQuizRepository.cs` | Create |
| `EnglishLearning.Domain/Interfaces/IVocabularyRepository.cs` | Create |
| `EnglishLearning.Domain/Interfaces/IQuizResultRepository.cs` | Create |
| `EnglishLearning.Domain/Interfaces/IUnitOfWork.cs` | Create |

## Steps

### Step 1: Create IRepository<T> generic interface
1. Create `Interfaces/` folder in Domain
2. Create `IRepository<T>` where T : class
3. Define methods:
   - `Task<T?> GetByIdAsync(Guid id)`
   - `Task<IEnumerable<T>> GetAllAsync()`
   - `Task<IEnumerable<T>> GetByExpressionAsync(Expression<Func<T, bool>> predicate)`
   - `Task<(IReadOnlyList<T> Items, int TotalRecords)> GetPagedAsync(int pageNumber, int pageSize)`
   - `Task AddAsync(T entity)`
   - `void Update(T entity)`
   - `void Delete(T entity)`

### Step 2: Create IQuizRepository
1. Inherit from `IRepository<Quiz>`
2. Add custom methods:
   - `Task<Quiz?> GetQuizWithQuestionsAsync(Guid id)` — eager load questions + choices
   - `Task<IEnumerable<Quiz>> GetQuizzesByDifficultyAsync(DifficultyLevel difficulty)`

### Step 3: Create IVocabularyRepository
1. Inherit from `IRepository<Vocabulary>`
2. Add custom methods:
   - `Task<Vocabulary?> GetByWordAsync(string word)`
   - `Task<IEnumerable<Vocabulary>> SearchByDifficultyAsync(DifficultyLevel difficulty)`

### Step 4: Create IQuizResultRepository
1. Inherit from `IRepository<QuizResult>`
2. Add custom methods:
   - `Task<IEnumerable<QuizResult>> GetByUserIdAsync(string userId)`
   - `Task<IEnumerable<QuizResult>> GetRecentResultsAsync(int count = 10)`

### Step 5: Create IUnitOfWork
1. Define properties for each repository
2. Define `Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)`

## Expected Code

```csharp
// IRepository.cs
using System.Linq.Expressions;

namespace EnglishLearning.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetByExpressionAsync(Expression<Func<T, bool>> predicate);
    Task<(IReadOnlyList<T> Items, int TotalRecords)> GetPagedAsync(int pageNumber, int pageSize);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}

// IQuizRepository.cs
namespace EnglishLearning.Domain.Interfaces;

public interface IQuizRepository : IRepository<Entities.Quiz>
{
    Task<Entities.Quiz?> GetQuizWithQuestionsAsync(Guid id);
    Task<IEnumerable<Entities.Quiz>> GetQuizzesByDifficultyAsync(Enums.DifficultyLevel difficulty);
}

// IVocabularyRepository.cs
namespace EnglishLearning.Domain.Interfaces;

public interface IVocabularyRepository : IRepository<Entities.Vocabulary>
{
    Task<Entities.Vocabulary?> GetByWordAsync(string word);
    Task<IEnumerable<Entities.Vocabulary>> SearchByDifficultyAsync(Enums.DifficultyLevel difficulty);
}

// IQuizResultRepository.cs
namespace EnglishLearning.Domain.Interfaces;

public interface IQuizResultRepository : IRepository<Entities.QuizResult>
{
    Task<IEnumerable<Entities.QuizResult>> GetByUserIdAsync(string userId);
    Task<IEnumerable<Entities.QuizResult>> GetRecentResultsAsync(int count = 10);
}

// IUnitOfWork.cs
namespace EnglishLearning.Domain.Interfaces;

public interface IUnitOfWork
{
    IQuizRepository Quizzes { get; }
    IVocabularyRepository Vocabularies { get; }
    IQuizResultRepository QuizResults { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Domain` — 0 errors ✅
- [x] All interfaces are `public` ✅
- [x] Generic IRepository uses `where T : class` constraint ✅
- [x] Specific repositories inherit IRepository with correct entity type ✅
- [x] IUnitOfWork exposes all repository properties ✅

## Acceptance Criteria

- [x] `IRepository<T>` is a generic interface with CRUD + paged + expression methods ✅
- [x] `GetPagedAsync` returns tuple `(Items, TotalRecords)` for pagination ✅
- [x] `IQuizRepository` extends `IRepository<Quiz>` with custom query methods ✅
- [x] `IVocabularyRepository` extends `IRepository<Vocabulary>` with custom query methods ✅
- [x] `IQuizResultRepository` extends `IRepository<QuizResult>` with custom query methods ✅
- [x] `IUnitOfWork` has repository properties and SaveChangesAsync method ✅
- [x] Domain project builds successfully ✅

---

## ✅ Completed: 2026-07-06

- `IRepository<T>` — generic CRUD + paged (tuple return) + expression methods
- `IQuizRepository` — custom: GetQuizWithQuestionsAsync, GetQuizzesByDifficultyAsync
- `IVocabularyRepository` — custom: GetByWordAsync, SearchByDifficultyAsync
- `IQuizResultRepository` — custom: GetByUserIdAsync, GetRecentResultsAsync
- `IUnitOfWork` — Quizzes, Vocabularies, QuizResults + SaveChangesAsync
- Build verified: 0 errors
