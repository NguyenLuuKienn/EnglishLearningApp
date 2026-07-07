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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class QuizzesController(IMediator _mediator) : ControllerBase
{
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

        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] DifficultyLevel? difficulty = null)
    {
        var query = new GetQuizzesQuery(pageNumber, pageSize, difficulty);
        var paged = await _mediator.Send(query);

        return Ok(PagedResponse<QuizDto>.Ok(
            paged.Items, paged.PageNumber, paged.PageSize, paged.TotalRecords));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetQuizQuery(id);
        var dto = await _mediator.Send(query);

        return Ok(ApiResponse<QuizDto>.Ok(dto));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateQuizRequest request)
    {
        var command = new UpdateQuizCommand(
            id, request.Title, request.Description, request.Difficulty,
            request.TimeLimitMinutes, request.PassingScore);

        var updatedId = await _mediator.Send(command);
        return Ok(ApiResponse<Guid>.Ok(updatedId, "Updated successfully"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteQuizCommand(id);
        await _mediator.Send(command);

        return NoContent();
    }
}
