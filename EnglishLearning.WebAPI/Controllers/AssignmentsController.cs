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
[Authorize]
public class AssignmentsController(IMediator _mediator) : ControllerBase
{
    [HttpGet("user/{userId}")]
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
}
