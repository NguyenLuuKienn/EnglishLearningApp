# Task 7.9: Query — GetLeaderboard & GetUserRank

## Description

Create queries for getting leaderboard (top users) and user's current rank.

## Priority
🟡 High — Leaderboard retrieval

## Dependencies
- Task 7.5 (LeaderboardDto)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/Features/Leaderboard/Queries/GetLeaderboard/GetLeaderboardQuery.cs` | Create |
| `EnglishLearning.Application/Features/Leaderboard/Queries/GetLeaderboard/GetLeaderboardQueryHandler.cs` | Create |
| `EnglishLearning.Application/Features/Leaderboard/Queries/GetUserRank/GetUserRankQuery.cs` | Create |
| `EnglishLearning.Application/Features/Leaderboard/Queries/GetUserRank/GetUserRankQueryHandler.cs` | Create |

## Steps

### Step 1: Create GetLeaderboardQuery
1. Properties: Count (top N users)
2. Returns `Result<List<LeaderboardDto>>`

### Step 2: Create GetUserRankQuery
1. Properties: UserId
2. Returns `Result<int>`

## Expected Code

```csharp
// GetLeaderboardQuery.cs
using EnglishLearning.Application.DTOs;
using MediatR;

namespace EnglishLearning.Application.Features.Leaderboard.Queries.GetLeaderboard;

public record GetLeaderboardQuery(int Count) : IRequest<Result<List<LeaderboardDto>>>;

// GetLeaderboardQueryHandler.cs
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Application.Features.Leaderboard.Queries.GetLeaderboard;

public class GetLeaderboardQueryHandler : IRequestHandler<GetLeaderboardQuery, Result<List<LeaderboardDto>>>
{
    private readonly ILeaderboardRepository _leaderboardRepository;

    public GetLeaderboardQueryHandler(ILeaderboardRepository leaderboardRepository)
    {
        _leaderboardRepository = leaderboardRepository;
    }

    public async Task<Result<List<LeaderboardDto>>> Handle(GetLeaderboardQuery request, CancellationToken cancellationToken)
    {
        var all = await _leaderboardRepository.GetAllAsync();
        var topUsers = all
            .OrderByDescending(l => l.TotalScore)
            .Take(request.Count)
            .ToList();

        var dtos = topUsers.Select((l, index) => new LeaderboardDto
        {
            Id = l.Id,
            UserId = l.UserId,
            Username = l.UserId, // TODO: Join with User table for actual username
            TotalScore = l.TotalScore,
            QuizzesCompleted = l.QuizzesCompleted,
            AverageScore = l.AverageScore,
            Streak = l.Streak,
            Rank = index + 1
        }).ToList();

        return dtos;
    }
}

// GetUserRankQuery.cs
using MediatR;

namespace EnglishLearning.Application.Features.Leaderboard.Queries.GetUserRank;

public record GetUserRankQuery(string UserId) : IRequest<Result<int>>;

// GetUserRankQueryHandler.cs
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Application.Features.Leaderboard.Queries.GetUserRank;

public class GetUserRankQueryHandler : IRequestHandler<GetUserRankQuery, Result<int>>
{
    private readonly ILeaderboardRepository _leaderboardRepository;

    public GetUserRankQueryHandler(ILeaderboardRepository leaderboardRepository)
    {
        _leaderboardRepository = leaderboardRepository;
    }

    public async Task<Result<int>> Handle(GetUserRankQuery request, CancellationToken cancellationToken)
    {
        var all = await _leaderboardRepository.GetAllAsync();
        var userLeaderboard = all.FirstOrDefault(l => l.UserId == request.UserId);

        if (userLeaderboard == null)
            return Result<int>.Failure("Leaderboard entry not found for this user.");

        var rank = all
            .OrderByDescending(l => l.TotalScore)
            .ToList()
            .FindIndex(l => l.UserId == request.UserId) + 1;

        return rank > 0 ? rank : Result<int>.Failure("Rank not found.");
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Application` — 0 errors ✅
- [x] GetLeaderboard returns top N users ✅
- [x] GetUserRank returns user's rank ✅

## Acceptance Criteria

- [x] `GetLeaderboardQuery` returns top N users ordered by TotalScore ✅
- [x] `GetUserRankQuery` returns user's current rank ✅
- [x] Rank is 1-based index ✅
- [x] Application project builds successfully ✅

---

## ✅ Completed: 2026-07-07

- **GetLeaderboardQuery** — Count → `IRequest<List<LeaderboardDto>>`
- **GetLeaderboardQueryHandler** — Uses `GetTopUsersAsync` + `GetByIdAsync` per user for actual Username
- **GetUserRankQuery** — UserId → `IRequest<int>`
- **GetUserRankQueryHandler** — Uses `GetRankByUserIdAsync` from repository
- Primary constructor injection, throws `KeyNotFoundException` with `LeaderboardErrorMessages`
- Build verified: 0 errors
