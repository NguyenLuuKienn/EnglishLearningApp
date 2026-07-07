# Task 7.2: Enum — AssignmentStatus

## Description

Create the AssignmentStatus enum to track quiz assignment lifecycle.

## Priority
🔴 Critical — Required for QuizAssignment entity

## Dependencies
- None (can be created in parallel with Task 7.1)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Domain/Enums/AssignmentStatus.cs` | Create |

## Steps

### Step 1: Create AssignmentStatus enum
1. Values: `Scheduled = 0`, `Active = 1`, `Completed = 2`, `Cancelled = 3`

## Expected Code

```csharp
namespace EnglishLearning.Domain.Enums;

public enum AssignmentStatus
{
    Scheduled = 0,
    Active = 1,
    Completed = 2,
    Cancelled = 3
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Domain` — 0 errors
- [ ] AssignmentStatus enum has all values

## Acceptance Criteria

- [ ] `AssignmentStatus` enum in `EnglishLearning.Domain.Enums` namespace
- [ ] Values: Scheduled, Active, Completed, Cancelled
- [ ] Domain project builds successfully
