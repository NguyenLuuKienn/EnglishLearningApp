# Task 7.7: Command — CancelAssignment

## Description

Create CancelAssignmentCommand and handler to cancel a quiz assignment.

## Priority
🟡 High — Assignment management

## Dependencies
- Task 7.4 (IQuizAssignmentRepository)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/Features/Assignments/Commands/CancelAssignment/CancelAssignmentCommand.cs` | Create |
| `EnglishLearning.Application/Features/Assignments/Commands/CancelAssignment/CancelAssignmentCommandHandler.cs` | Create |

## Steps

### Step 1: Create CancelAssignmentCommand
1. Properties: AssignmentId
2. Inherits from `IRequest`

### Step 2: Create CancelAssignmentCommandHandler
1. Inject `IQuizAssignmentRepository`
2. Find assignment, throw if not found
3. Set Status = Cancelled
4. Save

## Expected Code

```csharp
// CancelAssignmentCommand.cs
using MediatR;

namespace EnglishLearning.Application.Features.Assignments.Commands.CancelAssignment;

public record CancelAssignmentCommand(Guid AssignmentId) : IRequest;

// CancelAssignmentCommandHandler.cs
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Application.Features.Assignments.Commands.CancelAssignment;

public class CancelAssignmentCommandHandler(IQuizAssignmentRepository _assignmentRepository) 
    : IRequestHandler<CancelAssignmentCommand>
{
    public async Task Handle(CancelAssignmentCommand request, CancellationToken cancellationToken)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(request.AssignmentId);
        if (assignment == null)
            throw new KeyNotFoundException("Assignment not found.");

        if (assignment.Status == AssignmentStatus.Cancelled)
            throw new InvalidOperationException("Assignment is already cancelled.");

        assignment.Status = AssignmentStatus.Cancelled;
        _assignmentRepository.Update(assignment);
        await _assignmentRepository.SaveChangesAsync(cancellationToken);
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Application` — 0 errors ✅
- [x] Handler validates assignment exists and is not already cancelled ✅

## Acceptance Criteria

- [x] `CancelAssignmentCommand` with AssignmentId ✅
- [x] `CancelAssignmentCommandHandler` finds assignment by Id ✅
- [x] Throws KeyNotFoundException if not found ✅
- [x] Throws InvalidOperationException if already cancelled ✅
- [x] Sets Status to Cancelled ✅
- [x] Application project builds successfully ✅

---

## ✅ Completed: 2026-07-07

- **CancelAssignmentCommand** — AssignmentId → `IRequest`
- **CancelAssignmentCommandHandler** — Validates assignment exists & not already cancelled, sets Status = Cancelled
- Primary constructor injection, throws exceptions with `AssignmentErrorMessages`
- Build verified: 0 errors
