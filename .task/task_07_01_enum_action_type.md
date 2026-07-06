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

- [ ] Run `dotnet build EnglishLearning.Domain` — 0 errors
- [ ] ActionType enum has all values

## Acceptance Criteria

- [ ] `ActionType` enum in `EnglishLearning.Domain.Enums` namespace
- [ ] Values: ViewVocabulary, CompleteQuiz, BookmarkWord, StartQuiz
- [ ] Domain project builds successfully
