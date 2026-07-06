using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.QuizResults.Commands.SubmitQuizResult;
using EnglishLearning.Application.Features.QuizResults.Queries.GetQuizResult;
using EnglishLearning.Application.Features.QuizResults.Queries.GetUserQuizResults;
using EnglishLearning.WebAPI.Models.Common;
using EnglishLearning.WebAPI.Models.Requests.QuizResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizResultsController : ControllerBase
{
    private readonly IMediator _mediator;

    public QuizResultsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("submit")]
    public async Task<IActionResult> Submit([FromBody] SubmitQuizResultRequest request)
    {
        var command = new SubmitQuizResultCommand(
            request.QuizId, request.UserId, request.DurationMinutes,
            request.Answers.Select(a => new AnswerCommand(
                a.QuestionId, a.SelectedChoiceId, a.AnswerText)).ToList());

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(ApiResponse<QuizResultDto>.BadRequest(
                result.Errors?.ToList() ?? [result.Error ?? string.Empty]));

        return Ok(ApiResponse<QuizResultDto>.Ok(result.Value!, "Quiz submitted successfully"));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetQuizResultQuery(id);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return NotFound(ApiResponse<QuizResultDto>.NotFound(result.Error ?? string.Empty));

        return Ok(ApiResponse<QuizResultDto>.Ok(result.Value!));
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(
        string userId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetUserQuizResultsQuery(userId, pageNumber, pageSize);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return BadRequest(ApiResponse<PagedResult<QuizResultDto>>.BadRequest(
                result.Errors?.ToList() ?? [result.Error ?? string.Empty]));

        var paged = result.Value!;
        return Ok(PagedResponse<QuizResultDto>.Ok(
            paged.Items, paged.PageNumber, paged.PageSize, paged.TotalRecords));
    }
}
