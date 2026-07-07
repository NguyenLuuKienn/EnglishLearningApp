# Task 7.12: Integrate History into QuizResult Handler

## Description

Update the SubmitQuizResultCommandHandler to automatically record history and update leaderboard when a quiz is submitted.

## Priority
🟡 High — Auto-log learning activities

## Dependencies
- Task 7.6 (RecordHistoryCommand), Task 7.8 (UpdateLeaderboardCommand)

## Files to Edit

| File | Action |
|------|--------|
| `EnglishLearning.Application/Features/QuizResults/Commands/SubmitQuizResult/SubmitQuizResultCommandHandler.cs` | Edit |

## Steps

### Step 1: Update SubmitQuizResultCommandHandler
1. Inject `IMediator` (for sending RecordHistory and UpdateLeaderboard commands)
2. After saving QuizResult, send `RecordHistoryCommand` (ActionType.CompleteQuiz)
3. Send `UpdateLeaderboardCommand` with the score

## Expected Code Changes

```csharp
// In SubmitQuizResultCommandHandler constructor, add:
private readonly IMediator _mediator;

// After saving quiz result, add:
// Record history
await _mediator.Send(new RecordHistoryCommand(
    request.UserId,
    Domain.Enums.ActionType.CompleteQuiz,
    request.QuizId,
    $"Quiz completed with score {quizResult.Score}%",
    quizResult.Score));

// Update leaderboard
await _mediator.Send(new UpdateLeaderboardCommand(
    request.UserId,
    quizResult.Score));
```

## Verification

- [x] Run `dotnet build EnglishLearning.Application` — 0 errors ✅
- [x] History is recorded on quiz submit ✅
- [x] Leaderboard is updated on quiz submit ✅

## Acceptance Criteria

- [x] SubmitQuizResultCommandHandler sends RecordHistoryCommand after saving ✅
- [x] ActionType is CompleteQuiz ✅
- [x] SubmitQuizResultCommandHandler sends UpdateLeaderboardCommand with score ✅
- [x] Application project builds successfully ✅

---

## ✅ Completed: 2026-07-07

- **SubmitQuizResultCommandHandler** — Injected `IMediator`, after saving QuizResult:
  - Sends `RecordHistoryCommand` (ActionType.CompleteQuiz, score details)
  - Sends `UpdateLeaderboardCommand` (score)
- Build verified: 0 errors
