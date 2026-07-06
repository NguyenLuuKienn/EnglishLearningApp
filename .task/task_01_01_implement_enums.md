# Task 1.1: Implement Enums

## Description

Fix and implement the two core enums in the Domain layer. Currently both files are defined as `internal class` instead of `public enum` with no values.

## Priority
🔴 Critical — Foundation for all entities

## Dependencies
None (first task)

## Files to Edit

| File | Action |
|------|--------|
| `EnglishLearning.Domain/Enums/DifficultyLevel.cs` | Rewrite |
| `EnglishLearning.Domain/Enums/QuestionType.cs` | Rewrite |

## Steps

### Step 1: Implement DifficultyLevel enum
1. Open `DifficultyLevel.cs`
2. Replace `internal class DifficultyLevel` with `public enum DifficultyLevel`
3. Add enum values:
   - `Beginner = 1`
   - `Intermediate = 2`
   - `Advanced = 3`

### Step 2: Implement QuestionType enum
1. Open `QuestionType.cs`
2. Replace `internal class QuestionType` with `public enum QuestionType`
3. Add enum values:
   - `MultipleChoice = 1`
   - `FillInBlank = 2`
   - `Listening = 3`

## Expected Code

```csharp
// DifficultyLevel.cs
namespace EnglishLearning.Domain.Enums;

public enum DifficultyLevel
{
    Beginner = 1,
    Intermediate = 2,
    Advanced = 3
}
```

```csharp
// QuestionType.cs
namespace EnglishLearning.Domain.Enums;

public enum QuestionType
{
    MultipleChoice = 1,
    FillInBlank = 2,
    Listening = 3
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Domain` — 0 errors ✅
- [x] Both enums are `public` and accessible from other projects ✅
- [x] Enum values are correctly defined with integer backing ✅

## Acceptance Criteria

- [x] `DifficultyLevel` enum has 3 values: Beginner, Intermediate, Advanced ✅
- [x] `QuestionType` enum has 3 values: MultipleChoice, FillInBlank, Listening ✅
- [x] Both are `public enum` (not `class`) ✅
- [x] Domain project builds successfully ✅

---

## ✅ Completed: 2026-07-06

- `DifficultyLevel.cs` — rewritten as `public enum` with Beginner=1, Intermediate=2, Advanced=3
- `QuestionType.cs` — rewritten as `public enum` with MultipleChoice=1, FillInBlank=2, Listening=3
- Build verified: 0 errors
