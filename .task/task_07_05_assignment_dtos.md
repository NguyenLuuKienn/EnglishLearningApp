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

- [x] Run `dotnet build EnglishLearning.Application` — 0 errors ✅
- [x] QuizAssignmentDto has all required properties ✅

## Acceptance Criteria

- [x] `QuizAssignmentDto` with Id, QuizId, QuizTitle, TargetRole, TargetUserId, StartTime, EndTime, Status ✅
- [x] In `EnglishLearning.Application.DTOs` namespace ✅
- [x] Application project builds successfully ✅

---

## ✅ Completed: 2026-07-07

- **QuizAssignmentDto** — Id, QuizId, QuizTitle, TargetRole (nullable), TargetUserId (nullable), StartTime, EndTime, Status
- Namespace: `EnglishLearning.Application.DTOs`
- Build verified: 0 errors
