using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Quizzes.Commands.CreateQuiz;
using EnglishLearning.Application.Features.Quizzes.Commands.DeleteQuiz;
using EnglishLearning.Application.Features.Quizzes.Commands.UpdateQuiz;
using EnglishLearning.Application.Features.Quizzes.Queries.GetQuiz;
using EnglishLearning.Application.Features.Quizzes.Queries.GetQuizzes;
using EnglishLearning.Domain.Enums;
using EnglishLearning.WebAPI.Models.Common;
using EnglishLearning.WebAPI.Models.Requests.Quizzes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizzesController : ControllerBase
{
    private readonly IMediator _mediator;

    public QuizzesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateQuizRequest request)
    {
        var command = new CreateQuizCommand(
            request.Title, request.Description, request.Difficulty,
            request.TimeLimitMinutes, request.PassingScore,
            request.Questions.Select(q => new QuestionCommand(
                q.QuestionText, q.QuestionType, q.Difficulty,
                q.CorrectAnswer, q.Choices.Select(c => new ChoiceCommand(c.ChoiceText, c.IsCorrect)).ToList()
            )).ToList());

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(ApiResponse<Guid>.BadRequest(
                result.Errors?.ToList() ?? [result.Error ?? string.Empty]));

        return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] DifficultyLevel? difficulty = null)
    {
        var query = new GetQuizzesQuery(pageNumber, pageSize, difficulty);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return BadRequest(ApiResponse<PagedResult<QuizDto>>.BadRequest(
                result.Errors?.ToList() ?? [result.Error ?? string.Empty]));

        var paged = result.Value!;
        return Ok(PagedResponse<QuizDto>.Ok(
            paged.Items, paged.PageNumber, paged.PageSize, paged.TotalRecords));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetQuizQuery(id);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return NotFound(ApiResponse<QuizDto>.NotFound(result.Error ?? string.Empty));

        return Ok(ApiResponse<QuizDto>.Ok(result.Value!));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateQuizRequest request)
    {
        var command = new UpdateQuizCommand(
            id, request.Title, request.Description, request.Difficulty,
            request.TimeLimitMinutes, request.PassingScore);

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            if (result.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
                return NotFound(ApiResponse<Guid>.NotFound(result.Error ?? string.Empty));

            return BadRequest(ApiResponse<Guid>.BadRequest(
                result.Errors?.ToList() ?? [result.Error ?? string.Empty]));
        }

        return Ok(ApiResponse<Guid>.Ok(result.Value!, "Updated successfully"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteQuizCommand(id);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return NotFound(ApiResponse<string>.NotFound(result.Error ?? string.Empty));

        return NoContent();
    }
}
