# Task 7.6: Command — AssignQuiz

## Description

Create AssignQuizCommand and handler to assign a quiz to a role or specific user with scheduling.

## Priority
🔴 Critical — Quiz assignment creation

## Dependencies
- Task 7.4 (IQuizAssignmentRepository), Task 7.5 (QuizAssignmentDto)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/Features/Assignments/Commands/AssignQuiz/AssignQuizCommand.cs` | Create |
| `EnglishLearning.Application/Features/Assignments/Commands/AssignQuiz/AssignQuizCommandHandler.cs` | Create |

## Steps

### Step 1: Create AssignQuizCommand
1. Properties: QuizId, TargetRole (nullable), TargetUserId (nullable), StartTime, EndTime
2. Inherits from `IRequest<Guid>`

### Step 2: Create AssignQuizCommandHandler
1. Inject `IQuizAssignmentRepository`, `IQuizRepository`
2. Validate: quiz exists, either TargetRole or TargetUserId is set
3. Create QuizAssignment entity
4. Save and return Id

## Expected Code

```csharp
// AssignQuizCommand.cs
using EnglishLearning.Domain.Enums;
using MediatR;

namespace EnglishLearning.Application.Features.Assignments.Commands.AssignQuiz;

public record AssignQuizCommand(
    Guid QuizId,
    UserRole? TargetRole,
    string? TargetUserId,
    DateTime StartTime,
    DateTime EndTime) : IRequest<Guid>;

// AssignQuizCommandHandler.cs
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Application.Features.Assignments.Commands.AssignQuiz;

public class AssignQuizCommandHandler(IQuizAssignmentRepository _assignmentRepository, IQuizRepository _quizRepository) 
    : IRequestHandler<AssignQuizCommand, Guid>
{
    public async Task<Guid> Handle(AssignQuizCommand request, CancellationToken cancellationToken)
    {
        // Validate quiz exists
        var quiz = await _quizRepository.GetByIdAsync(request.QuizId);
        if (quiz == null)
            throw new KeyNotFoundException("Quiz not found.");

        // Validate target
        if (request.TargetRole == null && string.IsNullOrEmpty(request.TargetUserId))
            throw new ArgumentException("Either TargetRole or TargetUserId must be specified.");

        // Create assignment
        var assignment = QuizAssignment.Create(
            request.QuizId,
            request.TargetRole,
            request.TargetUserId,
            request.StartTime,
            request.EndTime);

        await _assignmentRepository.AddAsync(assignment);
        await _assignmentRepository.SaveChangesAsync(cancellationToken);

        return assignment.Id;
    }
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Application` — 0 errors
- [ ] Handler validates quiz exists
- [ ] Handler validates target is specified

## Acceptance Criteria

- [ ] `AssignQuizCommand` with QuizId, TargetRole, TargetUserId, StartTime, EndTime
- [ ] `AssignQuizCommandHandler` validates quiz exists
- [ ] `AssignQuizCommandHandler` validates either TargetRole or TargetUserId is set
- [ ] Creates QuizAssignment with Status = Scheduled
- [ ] Returns `Guid` (AssignmentId)
- [ ] Application project builds successfully
