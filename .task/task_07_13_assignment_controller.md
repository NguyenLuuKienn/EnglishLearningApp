# Task 7.13: AssignmentController

## Description

Create AssignmentController with endpoints for quiz assignment management.

## Priority
🔴 Critical — HTTP entry points for assignments

## Dependencies
- Task 7.6-7.10 (CQRS commands/queries)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.WebAPI/Controllers/AssignmentsController.cs` | Create |

## Steps

### Step 1: Create AssignmentsController
1. `[ApiController]`, `[Route("api/[controller]")]`
2. Inject `IMediator`
3. Endpoints:
   - `POST /` — AssignQuizCommand (Admin/Teacher only)
   - `POST /{id}/cancel` — CancelAssignmentCommand (Admin/Teacher only)
   - `GET /user/{userId}` — GetUserAssignmentsQuery
   - `GET /active` — GetActiveAssignmentsQuery
   - `GET /{id}` — GetAssignmentByIdQuery

## Expected Code

```csharp
using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Assignments.Commands.AssignQuiz;
using EnglishLearning.Application.Features.Assignments.Commands.CancelAssignment;
using EnglishLearning.Application.Features.Assignments.Queries.GetActiveAssignments;
using EnglishLearning.Application.Features.Assignments.Queries.GetAssignmentById;
using EnglishLearning.Application.Features.Assignments.Queries.GetUserAssignments;
using EnglishLearning.WebAPI.Models.Common;
using EnglishLearning.WebAPI.Models.Requests.Assignments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssignmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssignmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> Assign([FromBody] AssignQuizRequest request)
    {
        var command = new AssignQuizCommand(
            request.QuizId, request.TargetRole, request.TargetUserId,
            request.StartTime, request.EndTime);

        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPost("{id}/cancel")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var command = new CancelAssignmentCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("user/{userId}")]
    [Authorize]
    public async Task<IActionResult> GetUserAssignments(string userId)
    {
        var query = new GetUserAssignmentsQuery(userId);
        var assignments = await _mediator.Send(query);
        return Ok(ApiResponse<List<QuizAssignmentDto>>.Ok(assignments));
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActiveAssignments()
    {
        var query = new GetActiveAssignmentsQuery();
        var assignments = await _mediator.Send(query);
        return Ok(ApiResponse<List<QuizAssignmentDto>>.Ok(assignments));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetAssignmentByIdQuery(id);
        var assignment = await _mediator.Send(query);
        return Ok(ApiResponse<QuizAssignmentDto>.Ok(assignment));
    }
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.WebAPI` — 0 errors
- [ ] All endpoints return proper ApiResponse
- [ ] Admin/Teacher endpoints require authorization

## Acceptance Criteria

- [ ] `AssignmentsController` with `[ApiController]`, `[Route("api/[controller]")]`
- [ ] `POST /` — AssignQuiz, requires Admin/Teacher role
- [ ] `POST /{id}/cancel` — CancelAssignment, requires Admin/Teacher role
- [ ] `GET /user/{userId}` — GetUserAssignments, requires auth
- [ ] `GET /active` — GetActiveAssignments, public
- [ ] `GET /{id}` — GetAssignmentById, public
- [ ] All responses wrapped in `ApiResponse<T>`
- [ ] WebAPI project builds successfully
