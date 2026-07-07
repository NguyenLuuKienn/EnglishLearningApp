# Task 7.3: Entity — Leaderboard

## Description

Create the Leaderboard entity to track user rankings and statistics.

## Priority
🔴 Critical — Leaderboard foundation

## Dependencies
- None (independent)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Domain/Entities/Leaderboard.cs` | Create |

## Steps

### Step 1: Create Leaderboard entity
1. Inherit from `BaseEntity`
2. Properties:
   - `UserId` (string, required, max 200)
   - `TotalScore` (decimal)
   - `QuizzesCompleted` (int)
   - `AverageScore` (decimal, precision 5,2)
   - `Streak` (int — consecutive days)
   - `LastActiveDate` (DateTime)
3. Factory method `Create(userId)`

## Expected Code

```csharp
namespace EnglishLearning.Domain.Entities;

public class Leaderboard : Common.BaseEntity
{
    public string UserId { get; set; } = string.Empty;
    public decimal TotalScore { get; set; }
    public int QuizzesCompleted { get; set; }
    public decimal AverageScore { get; set; }
    public int Streak { get; set; }
    public DateTime LastActiveDate { get; set; }

    public static Leaderboard Create(string userId)
    {
        return new Leaderboard
        {
            UserId = userId,
            TotalScore = 0m,
            QuizzesCompleted = 0,
            AverageScore = 0m,
            Streak = 0,
            LastActiveDate = DateTime.UtcNow
        };
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Domain` — 0 errors ✅
- [x] Leaderboard entity has all properties ✅

## Acceptance Criteria

- [x] `Leaderboard` inherits from `BaseEntity` ✅
- [x] Properties: UserId, TotalScore, QuizzesCompleted, AverageScore, Streak, LastActiveDate ✅
- [x] Factory method `Create()` initializes with zeros ✅
- [x] Domain project builds successfully ✅

---

## ✅ Completed: 2026-07-07

- **Leaderboard** — UserId, TotalScore, QuizzesCompleted, AverageScore, Streak, LastActiveDate
- Factory method `Create(userId)` initializes scores/counts to 0, `LastActiveDate = DateTime.UtcNow`
- Build verified: 0 errors
