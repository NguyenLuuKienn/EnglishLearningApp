# Task 7.4: Interfaces — History & Leaderboard Repositories

## Description

Create repository interfaces for LearningHistory and Leaderboard.

## Priority
🔴 Critical — Domain contracts

## Dependencies
- Task 7.2 (LearningHistory), Task 7.3 (Leaderboard)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Domain/Interfaces/ILearningHistoryRepository.cs` | Create |
| `EnglishLearning.Domain/Interfaces/ILeaderboardRepository.cs` | Create |

## Steps

### Step 1: Create ILearningHistoryRepository
1. Inherit from `IRepository<LearningHistory>`
2. Methods:
   - `Task<PagedResult<LearningHistory>> GetByUserIdAsync(string userId, int pageNumber, int pageSize)`
   - `Task<List<LearningHistory>> GetRecentByUserIdAsync(string userId, int count)`

### Step 2: Create ILeaderboardRepository
1. Inherit from `IRepository<Leaderboard>`
2. Methods:
   - `Task<Leaderboard?> GetByUserIdAsync(string userId)`
   - `Task<List<Leaderboard>> GetTopUsersAsync(int count)`
   - `Task<int> GetRankByUserIdAsync(string userId)`

## Expected Code

```csharp
// ILearningHistoryRepository.cs
using EnglishLearning.Domain.Entities;

namespace EnglishLearning.Domain.Interfaces;

public interface ILearningHistoryRepository : IRepository<LearningHistory>
{
    Task<List<LearningHistory>> GetByUserIdAsync(string userId, int pageNumber, int pageSize);
    Task<List<LearningHistory>> GetRecentByUserIdAsync(string userId, int count);
}

// ILeaderboardRepository.cs
using EnglishLearning.Domain.Entities;

namespace EnglishLearning.Domain.Interfaces;

public interface ILeaderboardRepository : IRepository<Leaderboard>
{
    Task<Leaderboard?> GetByUserIdAsync(string userId);
    Task<List<Leaderboard>> GetTopUsersAsync(int count);
    Task<int> GetRankByUserIdAsync(string userId);
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Domain` — 0 errors
- [ ] Both interfaces inherit from IRepository

## Acceptance Criteria

- [ ] `ILearningHistoryRepository` with GetByUserIdAsync, GetRecentByUserIdAsync
- [ ] `ILeaderboardRepository` with GetByUserIdAsync, GetTopUsersAsync, GetRankByUserIdAsync
- [ ] Both inherit from `IRepository<T>`
- [ ] Domain project builds successfully
