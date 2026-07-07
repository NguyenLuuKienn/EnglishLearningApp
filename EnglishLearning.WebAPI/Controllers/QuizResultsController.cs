using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.QuizResults.Commands.SubmitQuizResult;
using EnglishLearning.Application.Features.QuizResults.Queries.GetQuizResult;
using EnglishLearning.Application.Features.QuizResults.Queries.GetUserQuizResults;
using EnglishLearning.WebAPI.Models.Common;
using EnglishLearning.WebAPI.Models.Requests.QuizResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class QuizResultsController(IMediator _mediator) : ControllerBase
{
    [HttpPost("submit")]
    public async Task<IActionResult> Submit([FromBody] SubmitQuizResultRequest request)
    {
        var command = new SubmitQuizResultCommand(
            request.QuizId, request.UserId, request.DurationMinutes,
            request.Answers.Select(a => new AnswerCommand(
                a.QuestionId, a.SelectedChoiceId, a.AnswerText)).ToList());

        var dto = await _mediator.Send(command);
        return Ok(ApiResponse<QuizResultDto>.Ok(dto, "Quiz submitted successfully"));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetQuizResultQuery(id);
        var dto = await _mediator.Send(query);

        return Ok(ApiResponse<QuizResultDto>.Ok(dto));
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(
        string userId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetUserQuizResultsQuery(userId, pageNumber, pageSize);
        var paged = await _mediator.Send(query);

        return Ok(PagedResponse<QuizResultDto>.Ok(
            paged.Items, paged.PageNumber, paged.PageSize, paged.TotalRecords));
    }
}
