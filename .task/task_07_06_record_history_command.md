# Task 7.6: Command — RecordHistory

## Description

Create RecordHistoryCommand and handler to log user learning activities.

## Priority
🔴 Critical — History logging

## Dependencies
- Task 7.4 (ILearningHistoryRepository)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/Features/History/Commands/RecordHistory/RecordHistoryCommand.cs` | Create |
| `EnglishLearning.Application/Features/History/Commands/RecordHistory/RecordHistoryCommandHandler.cs` | Create |

## Steps

### Step 1: Create RecordHistoryCommand
1. Properties: UserId, ActionType, TargetId, Details, Score
2. Inherits from `IRequest<Result<Guid>>`

### Step 2: Create RecordHistoryCommandHandler
1. Inject `ILearningHistoryRepository`, `IUnitOfWork`
2. Create LearningHistory entity
3. Save and return Id

## Expected Code

```csharp
// RecordHistoryCommand.cs
using EnglishLearning.Domain.Enums;
using MediatR;

namespace EnglishLearning.Application.Features.History.Commands.RecordHistory;

public record RecordHistoryCommand(
    string UserId,
    ActionType ActionType,
    Guid TargetId,
    string? Details,
    decimal? Score) : IRequest<Result<Guid>>;

// RecordHistoryCommandHandler.cs
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Application.Features.History.Commands.RecordHistory;

public class RecordHistoryCommandHandler : IRequestHandler<RecordHistoryCommand, Result<Guid>>
{
    private readonly ILearningHistoryRepository _historyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RecordHistoryCommandHandler(ILearningHistoryRepository historyRepository, IUnitOfWork unitOfWork)
    {
        _historyRepository = historyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RecordHistoryCommand request, CancellationToken cancellationToken)
    {
        var history = LearningHistory.Create(
            request.UserId, request.ActionType, request.TargetId, request.Details, request.Score);

        await _historyRepository.AddAsync(history);
        await _unitOfWork.SaveChangesAsync();

        return history.Id;
    }
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Application` — 0 errors
- [ ] Handler creates and saves LearningHistory

## Acceptance Criteria

- [ ] `RecordHistoryCommand` with UserId, ActionType, TargetId, Details, Score
- [ ] `RecordHistoryCommandHandler` creates LearningHistory entity
- [ ] Saves via UnitOfWork
- [ ] Returns `Result<Guid>` (HistoryId)
- [ ] Application project builds successfully
