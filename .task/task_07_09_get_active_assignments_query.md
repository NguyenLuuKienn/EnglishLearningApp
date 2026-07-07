# Task 7.9: Query — GetActiveAssignments

## Description

Create GetActiveAssignmentsQuery and handler to get currently active assignments (StartTime <= now <= EndTime).

## Priority
🟡 High — For dashboard and notifications

## Dependencies
- Task 7.5 (QuizAssignmentDto)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/Features/Assignments/Queries/GetActiveAssignments/GetActiveAssignmentsQuery.cs` | Create |
| `EnglishLearning.Application/Features/Assignments/Queries/GetActiveAssignments/GetActiveAssignmentsQueryHandler.cs` | Create |

## Steps

### Step 1: Create GetActiveAssignmentsQuery
1. Inherits from `IRequest<List<QuizAssignmentDto>>`

### Step 2: Create GetActiveAssignmentsQueryHandler
1. Inject `IQuizAssignmentRepository`, `IMapper`
2. Get all assignments where StartTime <= now <= EndTime and Status != Cancelled
3. Map to DTOs

## Expected Code

```csharp
// GetActiveAssignmentsQuery.cs
using EnglishLearning.Application.DTOs;
using MediatR;

namespace EnglishLearning.Application.Features.Assignments.Queries.GetActiveAssignments;

public record GetActiveAssignmentsQuery : IRequest<List<QuizAssignmentDto>>;

// GetActiveAssignmentsQueryHandler.cs
using AutoMapper;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Application.Features.Assignments.Queries.GetActiveAssignments;

public class GetActiveAssignmentsQueryHandler(
    IQuizAssignmentRepository _assignmentRepository, 
    IMapper _mapper) 
    : IRequestHandler<GetActiveAssignmentsQuery, List<QuizAssignmentDto>>
{
    public async Task<List<QuizAssignmentDto>> Handle(GetActiveAssignmentsQuery request, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var allAssignments = await _assignmentRepository.GetAllAsync();
        
        var activeAssignments = allAssignments
            .Where(a => a.Status != AssignmentStatus.Cancelled &&
                       a.StartTime <= now && a.EndTime >= now)
            .ToList();

        return _mapper.Map<List<QuizAssignmentDto>>(activeAssignments);
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Application` — 0 errors ✅
- [x] Handler filters by time range ✅

## Acceptance Criteria

- [x] `GetActiveAssignmentsQuery` returns active assignments ✅
- [x] `GetActiveAssignmentsQueryHandler` filters by StartTime <= now <= EndTime ✅
- [x] Excludes cancelled assignments ✅
- [x] Returns `List<QuizAssignmentDto>` ✅
- [x] Application project builds successfully ✅

---

## ✅ Completed: 2026-07-07

- **GetActiveAssignmentsQuery** → `IRequest<List<QuizAssignmentDto>>`
- **GetActiveAssignmentsQueryHandler** — Filters by `StartTime <= now <= EndTime`, excludes cancelled, AutoMapper mapping
- Primary constructor injection
- Build verified: 0 errors
