# Task 1.5: Implement Choice Entity

## Description

Implement the Choice entity that represents an answer option for a MultipleChoice question.

## Priority
🟡 High — Required for MultipleChoice questions

## Dependencies
- Task 1.0 (BaseEntity)
- Task 1.4 (Question entity)

## Files to Edit

| File | Action |
|------|--------|
| `EnglishLearning.Domain/Entities/Choice.cs` | Rewrite |

## Steps

### Step 1: Define entity properties
1. Change `internal class` to `public class`
2. Inherit from `BaseEntity` (auto gets: `Id`, `CreatedAt`, `UpdatedAt`, `CreatedBy`, `UpdatedBy`)
3. Add core properties:
   - `ChoiceText` (string, required, max 500 chars)
   - `IsCorrect` (bool)
   - `QuestionId` (Guid, foreign key)

### Step 2: Add navigation properties
1. Add `Question Question` — belongs to a question

## Expected Code

```csharp
namespace EnglishLearning.Domain.Entities;

public class Choice : Common.BaseEntity
{
    public string ChoiceText { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public Guid QuestionId { get; set; }

    // Navigation
    public Question Question { get; set; } = null!;
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Domain` — 0 errors ✅
- [x] Entity has all required properties ✅
- [x] Navigation to Question is correctly defined ✅

## Acceptance Criteria

- [x] `Choice` is a `public class` inheriting from `BaseEntity` ✅
- [x] Has properties: ChoiceText, IsCorrect, QuestionId ✅
- [x] Inherits from BaseEntity: Id, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy ✅
- [x] Has navigation property: Question ✅
- [x] Domain project builds successfully ✅

---

## ✅ Completed: 2026-07-06

- Inherits from `BaseEntity` (Id, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy)
- Properties: ChoiceText, IsCorrect, QuestionId
- Navigation: `Question Question`
