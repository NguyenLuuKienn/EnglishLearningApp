# Task 7.11: History & Leaderboard Controllers + DI + Migration

## Description

Create HistoryController, LeaderboardController, register DI, update DbContext, and create migration.

## Priority
🔴 Critical — Complete History & Leaderboard feature

## Dependencies
- Task 7.6-7.9 (CQRS), Task 7.10 (Repositories)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.WebAPI/Controllers/HistoryController.cs` | Create |
| `EnglishLearning.WebAPI/Controllers/LeaderboardController.cs` | Create |

## Files to Edit

| File | Action |
|------|--------|
| `EnglishLearning.Infrastructure/DependencyInjection.cs` | Edit |
| `EnglishLearning.Infrastructure/Persistence/ApplicationDbContext.cs` | Edit |

## Steps

### Step 1: Create HistoryController
- `GET /api/history/user/{userId}` — GetUserHistoryQuery (paged)

### Step 2: Create LeaderboardController
- `GET /api/leaderboard` — GetLeaderboardQuery (top N)
- `GET /api/leaderboard/user/{userId}/rank` — GetUserRankQuery

### Step 3: Register DI
- ILearningHistoryRepository → LearningHistoryRepository
- ILeaderboardRepository → LeaderboardRepository

### Step 4: Update ApplicationDbContext
- Add `DbSet<LearningHistory>` and `DbSet<Leaderboard>`

### Step 5: Create migration
- `dotnet ef migrations add AddHistoryAndLeaderboard --startup-project ..\EnglishLearning.WebAPI`

## Expected Code

```csharp
// HistoryController.cs
using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.History.Queries.GetUserHistory;
using EnglishLearning.WebAPI.Models.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class HistoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public HistoryController(IMediator mediator) => _mediator = mediator;

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserHistory(
        string userId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetUserHistoryQuery(userId, pageNumber, pageSize);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return BadRequest(ApiResponse<PagedResult<LearningHistoryDto>>.BadRequest(
                result.Errors?.ToList() ?? [result.Error ?? string.Empty]));

        var paged = result.Value!;
        return Ok(PagedResponse<LearningHistoryDto>.Ok(
            paged.Items, paged.PageNumber, paged.PageSize, paged.TotalRecords));
    }
}

// LeaderboardController.cs
using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Leaderboard.Queries.GetLeaderboard;
using EnglishLearning.Application.Features.Leaderboard.Queries.GetUserRank;
using EnglishLearning.WebAPI.Models.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaderboardController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaderboardController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetLeaderboard([FromQuery] int count = 100)
    {
        var query = new GetLeaderboardQuery(count);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return BadRequest(ApiResponse<List<LeaderboardDto>>.BadRequest(
                result.Errors?.ToList() ?? [result.Error ?? string.Empty]));

        return Ok(ApiResponse<List<LeaderboardDto>>.Ok(result.Value!));
    }

    [HttpGet("user/{userId}/rank")]
    [Authorize]
    public async Task<IActionResult> GetUserRank(string userId)
    {
        var query = new GetUserRankQuery(userId);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return NotFound(ApiResponse<int>.NotFound(result.Error ?? string.Empty));

        return Ok(ApiResponse<int>.Ok(result.Value!));
    }
}
```

## Verification

- [ ] Run `dotnet build` — 0 errors
- [ ] Controllers follow existing pattern
- [ ] DI registered
- [ ] Migration created

## Acceptance Criteria

- [ ] `HistoryController` with GET /user/{userId} endpoint
- [ ] `LeaderboardController` with GET / and GET /user/{userId}/rank endpoints
- [ ] Repositories registered in DI
- [ ] DbSet added to ApplicationDbContext
- [ ] Migration created and applied
- [ ] Full solution builds successfully
