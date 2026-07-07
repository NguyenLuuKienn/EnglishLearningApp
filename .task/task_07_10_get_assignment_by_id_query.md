# Task 7.10: Query — GetAssignmentById

## Description

Create GetAssignmentByIdQuery and handler to get a single assignment by ID.

## Priority
🟡 High — Assignment detail view

## Dependencies
- Task 7.5 (QuizAssignmentDto)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/Features/Assignments/Queries/GetAssignmentById/GetAssignmentByIdQuery.cs` | Create |
| `EnglishLearning.Application/Features/Assignments/Queries/GetAssignmentById/GetAssignmentByIdQueryHandler.cs` | Create |

## Steps

### Step 1: Create GetAssignmentByIdQuery
1. Properties: AssignmentId
2. Inherits from `IRequest<QuizAssignmentDto>`

### Step 2: Create GetAssignmentByIdQueryHandler
1. Inject `IQuizAssignmentRepository`, `IMapper`
2. Find assignment, throw if not found
3. Map to DTO

## Expected Code

```csharp
// GetAssignmentByIdQuery.cs
using EnglishLearning.Application.DTOs;
using MediatR;

namespace EnglishLearning.Application.Features.Assignments.Queries.GetAssignmentById;

public record GetAssignmentByIdQuery(Guid AssignmentId) : IRequest<QuizAssignmentDto>;

// GetAssignmentByIdQueryHandler.cs
using AutoMapper;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Application.Features.Assignments.Queries.GetAssignmentById;

public class GetAssignmentByIdQueryHandler(
    IQuizAssignmentRepository _assignmentRepository, 
    IMapper _mapper) 
    : IRequestHandler<GetAssignmentByIdQuery, QuizAssignmentDto>
{
    public async Task<QuizAssignmentDto> Handle(GetAssignmentByIdQuery request, CancellationToken cancellationToken)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(request.AssignmentId);
        if (assignment == null)
            throw new KeyNotFoundException("Assignment not found.");

        return _mapper.Map<QuizAssignmentDto>(assignment);
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Application` — 0 errors ✅
- [x] Handler throws if assignment not found ✅

## Acceptance Criteria

- [x] `GetAssignmentByIdQuery` with AssignmentId ✅
- [x] `GetAssignmentByIdQueryHandler` finds assignment by Id ✅
- [x] Throws KeyNotFoundException if not found ✅
- [x] Returns `QuizAssignmentDto` ✅
- [x] Application project builds successfully ✅

---

## ✅ Completed: 2026-07-07

- **GetAssignmentByIdQuery** — AssignmentId → `IRequest<QuizAssignmentDto>`
- **GetAssignmentByIdQueryHandler** — Finds assignment by Id, AutoMapper mapping
- Primary constructor injection, throws `KeyNotFoundException` with `AssignmentErrorMessages`
- Build verified: 0 errors
