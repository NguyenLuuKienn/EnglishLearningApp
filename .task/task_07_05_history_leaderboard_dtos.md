# Task 7.5: DTOs — History & Leaderboard

## Description

Create DTOs for LearningHistory and Leaderboard.

## Priority
🔴 Critical — Required for CQRS

## Dependencies
- Task 7.2 (LearningHistory), Task 7.3 (Leaderboard)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/DTOs/LearningHistoryDto.cs` | Create |
| `EnglishLearning.Application/DTOs/LeaderboardDto.cs` | Create |

## Steps

### Step 1: Create LearningHistoryDto
1. Properties: Id, UserId, ActionType, TargetId, Details, Score, CreatedAt

### Step 2: Create LeaderboardDto
1. Properties: Id, UserId, Username, TotalScore, QuizzesCompleted, AverageScore, Streak, Rank

## Expected Code

```csharp
// LearningHistoryDto.cs
using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Application.DTOs;

public class LearningHistoryDto
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public ActionType ActionType { get; set; }
    public Guid TargetId { get; set; }
    public string? Details { get; set; }
    public decimal? Score { get; set; }
    public DateTime CreatedAt { get; set; }
}

// LeaderboardDto.cs
namespace EnglishLearning.Application.DTOs;

public class LeaderboardDto
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public decimal TotalScore { get; set; }
    public int QuizzesCompleted { get; set; }
    public decimal AverageScore { get; set; }
    public int Streak { get; set; }
    public int Rank { get; set; }
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Application` — 0 errors
- [ ] Both DTOs have required properties

## Acceptance Criteria

- [ ] `LearningHistoryDto` with Id, UserId, ActionType, TargetId, Details, Score, CreatedAt
- [ ] `LeaderboardDto` with Id, UserId, Username, TotalScore, QuizzesCompleted, AverageScore, Streak, Rank
- [ ] Both in `EnglishLearning.Application.DTOs` namespace
- [ ] Application project builds successfully
