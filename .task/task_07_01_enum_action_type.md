# Task 7.1: Enum — ActionType

## Description

Create the ActionType enum to define types of learning activities for history tracking.

## Priority
🔴 Critical — Required for LearningHistory entity

## Dependencies
- None (can be created in parallel)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Domain/Enums/ActionType.cs` | Create |

## Steps

### Step 1: Create ActionType enum
1. Values: `ViewVocabulary = 0`, `CompleteQuiz = 1`, `BookmarkWord = 2`, `StartQuiz = 3`

## Expected Code

```csharp
namespace EnglishLearning.Domain.Enums;

public enum ActionType
{
    ViewVocabulary = 0,
    CompleteQuiz = 1,
    BookmarkWord = 2,
    StartQuiz = 3
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Domain` — 0 errors ✅
- [x] ActionType enum has all values ✅

## Acceptance Criteria

- [x] `ActionType` enum in `EnglishLearning.Domain.Enums` namespace ✅
- [x] Values: ViewVocabulary, CompleteQuiz, BookmarkWord, StartQuiz ✅
- [x] Domain project builds successfully ✅

---

## ✅ Completed: 2026-07-07

- **ActionType** — ViewVocabulary = 0, CompleteQuiz = 1, BookmarkWord = 2, StartQuiz = 3
- Namespace: `EnglishLearning.Domain.Enums`
- Build verified: 0 errors
