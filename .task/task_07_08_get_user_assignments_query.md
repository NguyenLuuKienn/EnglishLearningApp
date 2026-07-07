# Task 7.8: Query — GetUserAssignments

## Description

Create GetUserAssignmentsQuery and handler to get all assignments for a user (by role or direct assignment).

## Priority
🔴 Critical — User sees their assigned quizzes

## Dependencies
- Task 7.5 (QuizAssignmentDto)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/Features/Assignments/Queries/GetUserAssignments/GetUserAssignmentsQuery.cs` | Create |
| `EnglishLearning.Application/Features/Assignments/Queries/GetUserAssignments/GetUserAssignmentsQueryHandler.cs` | Create |

## Steps

### Step 1: Create GetUserAssignmentsQuery
1. Properties: UserId
2. Inherits from `IRequest<List<QuizAssignmentDto>>`

### Step 2: Create GetUserAssignmentsQueryHandler
1. Inject `IQuizAssignmentRepository`, `IUserRepository`, `IMapper`
2. Get user to determine role
3. Get assignments by userId + assignments by user's role
4. Filter out cancelled assignments
5. Map to DTOs

## Expected Code

```csharp
// GetUserAssignmentsQuery.cs
using EnglishLearning.Application.DTOs;
using MediatR;

namespace EnglishLearning.Application.Features.Assignments.Queries.GetUserAssignments;

public record GetUserAssignmentsQuery(string UserId) : IRequest<List<QuizAssignmentDto>>;

// GetUserAssignmentsQueryHandler.cs
using AutoMapper;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Application.Features.Assignments.Queries.GetUserAssignments;

public class GetUserAssignmentsQueryHandler(
    IQuizAssignmentRepository _assignmentRepository, 
    IUserRepository _userRepository, 
    IMapper _mapper) 
    : IRequestHandler<GetUserAssignmentsQuery, List<QuizAssignmentDto>>
{
    public async Task<List<QuizAssignmentDto>> Handle(GetUserAssignmentsQuery request, CancellationToken cancellationToken)
    {
        // Get user to determine role
        var user = await _userRepository.GetByUserIdAsync(request.UserId);
        if (user == null)
            throw new KeyNotFoundException("User not found.");

        // Get all assignments
        var allAssignments = await _assignmentRepository.GetAllAsync();
        
        // Filter: assignments for this user OR for this user's role, excluding cancelled
        var userAssignments = allAssignments
            .Where(a => a.Status != AssignmentStatus.Cancelled &&
                       (a.TargetUserId == request.UserId || a.TargetRole == user.Role))
            .OrderByDescending(a => a.StartTime)
            .ToList();

        return _mapper.Map<List<QuizAssignmentDto>>(userAssignments);
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Application` — 0 errors ✅
- [x] Handler gets assignments by userId and role ✅

## Acceptance Criteria

- [x] `GetUserAssignmentsQuery` with UserId ✅
- [x] `GetUserAssignmentsQueryHandler` gets user's role ✅
- [x] Returns assignments for specific user AND user's role ✅
- [x] Excludes cancelled assignments ✅
- [x] Returns `List<QuizAssignmentDto>` ✅
- [x] Application project builds successfully ✅

---

## ✅ Completed: 2026-07-07

- **GetUserAssignmentsQuery** — UserId → `IRequest<List<QuizAssignmentDto>>`
- **GetUserAssignmentsQueryHandler** — Gets user role, filters assignments by userId + role, excludes cancelled, AutoMapper mapping
- Primary constructor injection, throws `KeyNotFoundException` with `AuthErrorMessages`
- Build verified: 0 errors
