# Task 7.7: Query — GetUserHistory

## Description

Create GetUserHistoryQuery and handler to get user's learning history (paged).

## Priority
🟡 High — History retrieval

## Dependencies
- Task 7.5 (LearningHistoryDto)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/Features/History/Queries/GetUserHistory/GetUserHistoryQuery.cs` | Create |
| `EnglishLearning.Application/Features/History/Queries/GetUserHistory/GetUserHistoryQueryHandler.cs` | Create |

## Steps

### Step 1: Create GetUserHistoryQuery
1. Properties: UserId, PageNumber, PageSize
2. Inherits from `IRequest<Result<PagedResult<LearningHistoryDto>>>`

### Step 2: Create GetUserHistoryQueryHandler
1. Inject `ILearningHistoryRepository`
2. Get history by userId (paged, ordered by CreatedAt desc)
3. Map to LearningHistoryDto

## Expected Code

```csharp
// GetUserHistoryQuery.cs
using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using MediatR;

namespace EnglishLearning.Application.Features.History.Queries.GetUserHistory;

public record GetUserHistoryQuery(
    string UserId,
    int PageNumber,
    int PageSize) : IRequest<Result<PagedResult<LearningHistoryDto>>>;

// GetUserHistoryQueryHandler.cs
using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearning.Application.Features.History.Queries.GetUserHistory;

public class GetUserHistoryQueryHandler : IRequestHandler<GetUserHistoryQuery, Result<PagedResult<LearningHistoryDto>>>
{
    private readonly ILearningHistoryRepository _historyRepository;

    public GetUserHistoryQueryHandler(ILearningHistoryRepository historyRepository)
    {
        _historyRepository = historyRepository;
    }

    public async Task<Result<PagedResult<LearningHistoryDto>>> Handle(GetUserHistoryQuery request, CancellationToken cancellationToken)
    {
        var allHistory = await _historyRepository.GetAllAsync();
        var userHistory = allHistory
            .Where(h => h.UserId == request.UserId)
            .OrderByDescending(h => h.CreatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        var totalRecords = userHistory.Count(h => h.UserId == request.UserId);

        var dtos = userHistory.Select(h => new LearningHistoryDto
        {
            Id = h.Id,
            UserId = h.UserId,
            ActionType = h.ActionType,
            TargetId = h.TargetId,
            Details = h.Details,
            Score = h.Score,
            CreatedAt = h.CreatedAt
        }).ToList();

        var paged = PagedResult<LearningHistoryDto>.Create(dtos, request.PageNumber, request.PageSize, totalRecords);
        return paged;
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Application` — 0 errors ✅
- [x] Returns paged LearningHistoryDto ✅

## Acceptance Criteria

- [x] `GetUserHistoryQuery` with UserId, PageNumber, PageSize ✅
- [x] Handler filters by UserId, orders by CreatedAt desc ✅
- [ ] Returns `Result<PagedResult<LearningHistoryDto>>`
- [ ] Application project builds successfully
