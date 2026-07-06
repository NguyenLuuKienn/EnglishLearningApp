# Task 1.3: Implement Quiz Entity

## Description

Implement the Quiz entity that represents a quiz/exam in the system. A quiz contains multiple questions and has results associated with it.

## Priority
🔴 Critical — Core entity for the quiz system

## Dependencies
- Task 1.0 (BaseEntity)
- Task 1.1 (DifficultyLevel enum)

## Files to Edit

| File | Action |
|------|--------|
| `EnglishLearning.Domain/Entities/Quiz.cs` | Rewrite |

## Steps

### Step 1: Define entity properties
1. Change `internal class` to `public class`
2. Inherit from `BaseEntity` (auto gets: `Id`, `CreatedAt`, `UpdatedAt`, `CreatedBy`, `UpdatedBy`)
3. Add core properties:
   - `Title` (string, required, max 200 chars)
   - `Description` (string, optional, max 1000 chars)
   - `Difficulty` (DifficultyLevel enum)
   - `TimeLimitMinutes` (int, default 0 = no limit)
   - `PassingScore` (decimal, default 50%)

### Step 2: Add navigation properties
1. Add `ICollection<Question> Questions` — quiz contains questions
2. Add `ICollection<QuizResult> Results` — quiz has results

## Expected Code

```csharp
namespace EnglishLearning.Domain.Entities;

public class Quiz : Common.BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Enums.DifficultyLevel Difficulty { get; set; }
    public int TimeLimitMinutes { get; set; }
    public decimal PassingScore { get; set; } = 50m;

    // Navigation
    public ICollection<Question> Questions { get; set; } = new List<Question>();
    public ICollection<QuizResult> Results { get; set; } = new List<QuizResult>();
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Domain` — 0 errors ✅
- [x] Entity has all required properties ✅
- [x] Inherits from BaseEntity ✅

## Acceptance Criteria

- [x] `Quiz` is a `public class` inheriting from `BaseEntity` ✅
- [x] Has properties: Title, Description, Difficulty, TimeLimitMinutes, PassingScore ✅
- [x] Inherits from BaseEntity: Id, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy ✅
- [x] Has navigation properties: Questions, Results ✅
- [x] PassingScore defaults to 50m ✅
- [x] Domain project builds successfully ✅

---

## ✅ Completed: 2026-07-06

- Inherits from `BaseEntity` (Id, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy)
- Properties: Title, Description, Difficulty, TimeLimitMinutes, PassingScore (default 50m)
- Navigation: `ICollection<Question> Questions`, `ICollection<QuizResult> Results`
- Entity-only (no business logic — handled by Application layer services)
