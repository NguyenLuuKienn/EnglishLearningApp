# Task 1.6: Implement QuizResult Entity

## Description

Implement the QuizResult entity that stores the result when a user completes a quiz.

## Priority
🟡 High — Required for result tracking

## Dependencies
- Task 1.0 (BaseEntity)
- Task 1.3 (Quiz entity)

## Files to Edit

| File | Action |
|------|--------|
| `EnglishLearning.Domain/Entities/QuizResult.cs` | Rewrite |

## Steps

### Step 1: Define entity properties
1. Change `internal class` to `public class`
2. Inherit from `BaseEntity` (auto gets: `Id`, `CreatedAt`, `UpdatedAt`, `CreatedBy`, `UpdatedBy`)
3. Add core properties:
   - `QuizId` (Guid, foreign key)
   - `UserId` (string) — stores user identifier (supports JWT claims)
   - `Score` (decimal) — percentage score
   - `TotalQuestions` (int)
   - `CorrectAnswers` (int)
   - `DurationMinutes` (int) — time taken to complete
   - `CompletedAt` (DateTime)

### Step 2: Add navigation properties
1. Add `Quiz Quiz` — belongs to a quiz

## Expected Code

```csharp
namespace EnglishLearning.Domain.Entities;

public class QuizResult : Common.BaseEntity
{
    public Guid QuizId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public decimal Score { get; set; }
    public int TotalQuestions { get; set; }
    public int CorrectAnswers { get; set; }
    public int DurationMinutes { get; set; }
    public DateTime CompletedAt { get; set; }

    // Navigation
    public Quiz Quiz { get; set; } = null!;

    public QuizResult()
    {
        CompletedAt = DateTime.UtcNow;
    }

    public static QuizResult Create(Guid quizId, string userId, int totalQuestions, int correctAnswers, int durationMinutes)
    {
        var score = totalQuestions > 0 ? Math.Round((correctAnswers / (double)totalQuestions) * 100, 2) : 0m;

        return new QuizResult
        {
            QuizId = quizId,
            UserId = userId,
            TotalQuestions = totalQuestions,
            CorrectAnswers = correctAnswers,
            DurationMinutes = durationMinutes,
            Score = score
        };
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Domain` — 0 errors ✅
- [x] Entity has all required properties ✅
- [x] Inherits from BaseEntity ✅

## Acceptance Criteria

- [x] `QuizResult` is a `public class` inheriting from `BaseEntity` ✅
- [x] Has properties: QuizId, UserId, Score, TotalQuestions, CorrectAnswers, DurationMinutes, CompletedAt ✅
- [x] Inherits from BaseEntity: Id, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy ✅
- [x] Has navigation property: Quiz ✅
- [x] Domain project builds successfully ✅

---

## ✅ Completed: 2026-07-06

- Inherits from `BaseEntity` (Id, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy)
- Properties: QuizId, UserId, Score, TotalQuestions, CorrectAnswers, DurationMinutes, CompletedAt
- Navigation: `Quiz Quiz`
- Entity-only (no business logic — score calculation handled by Application layer services)
