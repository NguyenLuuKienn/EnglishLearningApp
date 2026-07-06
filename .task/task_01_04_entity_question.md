# Task 1.4: Implement Question Entity

## Description

Implement the Question entity that represents a question within a quiz. Supports multiple question types (MultipleChoice, FillInBlank, Listening).

## Priority
🔴 Critical — Links Quiz and Choice entities

## Dependencies
- Task 1.0 (BaseEntity)
- Task 1.1 (DifficultyLevel enum, QuestionType enum)
- Task 1.3 (Quiz entity)

## Files to Edit

| File | Action |
|------|--------|
| `EnglishLearning.Domain/Entities/Question.cs` | Rewrite |

## Steps

### Step 1: Define entity properties
1. Change `internal class` to `public class`
2. Inherit from `BaseEntity` (auto gets: `Id`, `CreatedAt`, `UpdatedAt`, `CreatedBy`, `UpdatedBy`)
3. Add core properties:
   - `QuestionText` (string, required, max 2000 chars)
   - `QuestionType` (QuestionType enum)
   - `Difficulty` (DifficultyLevel enum)
   - `CorrectAnswer` (string, required for FillInBlank, optional for MultipleChoice since Choices track IsCorrect)
   - `Explanation` (string, optional, max 1000 chars)
   - `QuizId` (Guid, foreign key)

### Step 2: Add navigation properties
1. Add `Quiz Quiz` — belongs to a quiz
2. Add `ICollection<Choice> Choices` — has multiple choices (for MultipleChoice type)

## Expected Code

```csharp
namespace EnglishLearning.Domain.Entities;

public class Question : Common.BaseEntity
{
    public string QuestionText { get; set; } = string.Empty;
    public Enums.QuestionType QuestionType { get; set; }
    public Enums.DifficultyLevel Difficulty { get; set; }
    public string? CorrectAnswer { get; set; }
    public string? Explanation { get; set; }
    public Guid QuizId { get; set; }

    // Navigation
    public Quiz Quiz { get; set; } = null!;
    public ICollection<Choice> Choices { get; set; } = new List<Choice>();
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Domain` — 0 errors ✅
- [x] Entity has all required properties ✅
- [x] Inherits from BaseEntity ✅

## Acceptance Criteria

- [x] `Question` is a `public class` inheriting from `BaseEntity` ✅
- [x] Has properties: QuestionText, QuestionType, Difficulty, CorrectAnswer, Explanation, QuizId ✅
- [x] Inherits from BaseEntity: Id, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy ✅
- [x] Has navigation properties: Quiz, Choices ✅
- [x] Domain project builds successfully ✅

---

## ✅ Completed: 2026-07-06

- Inherits from `BaseEntity` (Id, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy)
- Properties: QuestionText, QuestionType, Difficulty, CorrectAnswer, Explanation, QuizId
- Navigation: `Quiz Quiz`, `ICollection<Choice> Choices`
- Entity-only (no business logic — handled by Application layer services)
