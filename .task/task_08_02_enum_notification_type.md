# Task 8.2: Enum — NotificationType

## Description

Create the NotificationType enum to define notification categories.

## Priority
🔴 Critical — Required for Notification entity

## Dependencies
- None (can be created in parallel with Task 8.1)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Domain/Enums/NotificationType.cs` | Create |

## Steps

### Step 1: Create NotificationType enum
1. Values: `QuizAssigned = 0`, `QuizStartingSoon = 1`, `QuizStarted = 2`, `QuizEnded = 3`, `QuizResultAvailable = 4`

## Expected Code

```csharp
namespace EnglishLearning.Domain.Enums;

public enum NotificationType
{
    QuizAssigned = 0,
    QuizStartingSoon = 1,
    QuizStarted = 2,
    QuizEnded = 3,
    QuizResultAvailable = 4
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Domain` — 0 errors ✅
- [x] NotificationType enum has all values ✅

## Acceptance Criteria

- [x] `NotificationType` enum in `EnglishLearning.Domain.Enums` namespace ✅
- [x] Values: QuizAssigned, QuizStartingSoon, QuizStarted, QuizEnded, QuizResultAvailable ✅
- [x] Domain project builds successfully ✅

---

## ✅ Completed: 2026-07-07

- **NotificationType** — QuizAssigned = 0, QuizStartingSoon = 1, QuizStarted = 2, QuizEnded = 3, QuizResultAvailable = 4
- Namespace: `EnglishLearning.Domain.Enums`
- Build verified: 0 errors ✅

---

## ✅ Completed: 2026-07-07

- **NotificationType** — QuizAssigned = 0, QuizStartingSoon = 1, QuizStarted = 2, QuizEnded = 3, QuizResultAvailable = 4
- Namespace: `EnglishLearning.Domain.Enums`
- Build verified: 0 errors
