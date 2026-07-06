# Task 7.8: Command — UpdateLeaderboard

## Description

Create UpdateLeaderboardCommand and handler to update user's leaderboard stats after quiz completion.

## Priority
🔴 Critical — Leaderboard calculation

## Dependencies
- Task 7.4 (ILeaderboardRepository)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/Features/Leaderboard/Commands/UpdateLeaderboard/UpdateLeaderboardCommand.cs` | Create |
| `EnglishLearning.Application/Features/Leaderboard/Commands/UpdateLeaderboard/UpdateLeaderboardCommandHandler.cs` | Create |

## Steps

### Step 1: Create UpdateLeaderboardCommand
1. Properties: UserId, Score
2. Inherits from `IRequest<Result<Guid>>`

### Step 2: Create UpdateLeaderboardCommandHandler
1. Get or create Leaderboard for user
2. Update TotalScore, QuizzesCompleted, AverageScore
3. Calculate streak (check LastActiveDate)
4. Save

## Expected Code

```csharp
// UpdateLeaderboardCommand.cs
using MediatR;

namespace EnglishLearning.Application.Features.Leaderboard.Commands.UpdateLeaderboard;

public record UpdateLeaderboardCommand(
    string UserId,
    decimal Score) : IRequest<Result<Guid>>;

// UpdateLeaderboardCommandHandler.cs
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Application.Features.Leaderboard.Commands.UpdateLeaderboard;

public class UpdateLeaderboardCommandHandler : IRequestHandler<UpdateLeaderboardCommand, Result<Guid>>
{
    private readonly ILeaderboardRepository _leaderboardRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLeaderboardCommandHandler(ILeaderboardRepository leaderboardRepository, IUnitOfWork unitOfWork)
    {
        _leaderboardRepository = leaderboardRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(UpdateLeaderboardCommand request, CancellationToken cancellationToken)
    {
        var allLeaderboards = await _leaderboardRepository.GetAllAsync();
        var leaderboard = allLeaderboards.FirstOrDefault(l => l.UserId == request.UserId);

        if (leaderboard == null)
        {
            leaderboard = Leaderboard.Create(request.UserId);
            await _leaderboardRepository.AddAsync(leaderboard);
        }

        // Update stats
        leaderboard.QuizzesCompleted++;
        leaderboard.TotalScore += request.Score;
        leaderboard.AverageScore = leaderboard.TotalScore / leaderboard.QuizzesCompleted;

        // Calculate streak
        var today = DateTime.UtcNow.Date;
        var lastActive = leaderboard.LastActiveDate.Date;
        if ((today - lastActive).Days == 1)
            leaderboard.Streak++;
        else if ((today - lastActive).Days > 1)
            leaderboard.Streak = 1;

        leaderboard.LastActiveDate = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync();

        return leaderboard.Id;
    }
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Application` — 0 errors
- [ ] Handler updates leaderboard stats correctly

## Acceptance Criteria

- [ ] `UpdateLeaderboardCommand` with UserId, Score
- [ ] Handler creates leaderboard if not exists
- [ ] Updates TotalScore, QuizzesCompleted, AverageScore
- [ ] Calculates streak based on LastActiveDate
- [ ] Returns `Result<Guid>`
- [ ] Application project builds successfully
