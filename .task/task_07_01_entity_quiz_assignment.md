# Task 7.1: Entity — QuizAssignment

## Description

Create the QuizAssignment entity to track quiz assignments to roles or specific users with scheduling.

## Priority
🔴 Critical — Foundation for quiz assignment feature

## Dependencies
- Task 6.1 (User entity), Task 6.2 (UserRole enum)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Domain/Entities/QuizAssignment.cs` | Create |

## Steps

### Step 1: Create QuizAssignment entity
1. Inherit from `BaseEntity`
2. Properties:
   - `QuizId` (Guid, required)
   - `TargetRole` (UserRole?, nullable — if null, assigned to specific user)
   - `TargetUserId` (string?, nullable — if null, assigned to role)
   - `StartTime` (DateTime, required)
   - `EndTime` (DateTime, required)
   - `Status` (AssignmentStatus enum)
3. Navigation: `Quiz`
4. Factory method `Create(quizId, targetRole, targetUserId, startTime, endTime)`

## Expected Code

```csharp
using EnglishLearning.Domain.Common;
using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Domain.Entities;

public class QuizAssignment : BaseEntity
{
    public Guid QuizId { get; set; }
    public UserRole? TargetRole { get; set; }
    public string? TargetUserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public AssignmentStatus Status { get; set; }

    // Navigation
    public Quiz Quiz { get; set; } = null!;

    public static QuizAssignment Create(
        Guid quizId, 
        UserRole? targetRole, 
        string? targetUserId, 
        DateTime startTime, 
        DateTime endTime)
    {
        return new QuizAssignment
        {
            QuizId = quizId,
            TargetRole = targetRole,
            TargetUserId = targetUserId,
            StartTime = startTime,
            EndTime = endTime,
            Status = AssignmentStatus.Scheduled
        };
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Domain` — 0 errors ✅
- [x] QuizAssignment entity has all properties ✅
- [x] Factory method exists ✅

## Acceptance Criteria

- [x] `QuizAssignment` inherits from `BaseEntity` ✅
- [x] Properties: QuizId, TargetRole, TargetUserId, StartTime, EndTime, Status ✅
- [x] `TargetRole` and `TargetUserId` are nullable (mutually exclusive) ✅
- [x] Factory method `Create()` initializes Status as Scheduled ✅
- [x] Domain project builds successfully ✅

---

## ✅ Completed: 2026-07-07

- **QuizAssignment** — QuizId, TargetRole (nullable), TargetUserId (nullable), StartTime, EndTime, Status
- Factory method `Create()` sets `Status = AssignmentStatus.Scheduled`
- Navigation property: `Quiz`
- Build verified: 0 errors
