# Task 7.14: Assignment Request Contracts

## Description

Create request DTOs for assignment endpoints.

## Priority
🔴 Critical — API input contracts for assignments

## Dependencies
- None (independent)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.WebAPI/Models/Requests/Assignments/AssignQuizRequest.cs` | Create |

## Steps

### Step 1: Create AssignQuizRequest
1. QuizId (required), TargetRole (nullable), TargetUserId (nullable), StartTime (required), EndTime (required)

## Expected Code

```csharp
using EnglishLearning.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EnglishLearning.WebAPI.Models.Requests.Assignments;

public class AssignQuizRequest
{
    [Required]
    public Guid QuizId { get; set; }

    public UserRole? TargetRole { get; set; }

    public string? TargetUserId { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.WebAPI` — 0 errors ✅
- [x] Request class has data annotations ✅

## Acceptance Criteria

- [x] `AssignQuizRequest` with QuizId, TargetRole, TargetUserId, StartTime, EndTime ✅
- [x] QuizId, StartTime, EndTime are required ✅
- [x] TargetRole and TargetUserId are nullable ✅
- [x] In `EnglishLearning.WebAPI.Models.Requests.Assignments` namespace ✅
- [x] WebAPI project builds successfully ✅

---

## ✅ Completed: 2026-07-07

- **AssignQuizRequest** — QuizId (Required), TargetRole (nullable), TargetUserId (nullable), StartTime (Required), EndTime (Required)
- Namespace: `EnglishLearning.WebAPI.Models.Requests.Assignments`
- Build verified: 0 errors
