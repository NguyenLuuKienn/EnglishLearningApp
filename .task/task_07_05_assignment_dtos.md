# Task 7.5: DTOs — QuizAssignment

## Description

Create DTOs for QuizAssignment.

## Priority
🔴 Critical — Required for CQRS

## Dependencies
- Task 7.1 (QuizAssignment entity)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/DTOs/QuizAssignmentDto.cs` | Create |

## Steps

### Step 1: Create QuizAssignmentDto
1. Properties: Id, QuizId, QuizTitle, TargetRole, TargetUserId, StartTime, EndTime, Status

## Expected Code

```csharp
using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Application.DTOs;

public class QuizAssignmentDto
{
    public Guid Id { get; set; }
    public Guid QuizId { get; set; }
    public string QuizTitle { get; set; } = string.Empty;
    public UserRole? TargetRole { get; set; }
    public string? TargetUserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public AssignmentStatus Status { get; set; }
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Application` — 0 errors
- [ ] QuizAssignmentDto has all required properties

## Acceptance Criteria

- [ ] `QuizAssignmentDto` with Id, QuizId, QuizTitle, TargetRole, TargetUserId, StartTime, EndTime, Status
- [ ] In `EnglishLearning.Application.DTOs` namespace
- [ ] Application project builds successfully
