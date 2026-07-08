using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Quizzes.Commands.CreateQuiz;
using EnglishLearning.Application.Features.Quizzes.Commands.DeleteQuiz;
using EnglishLearning.Application.Features.Quizzes.Commands.UpdateQuiz;
using EnglishLearning.Application.Features.Quizzes.Queries.GetQuiz;
using EnglishLearning.Application.Features.Quizzes.Queries.GetQuizForTake;
using EnglishLearning.Application.Features.Quizzes.Queries.GetQuestion;
using EnglishLearning.Application.Features.Quizzes.Queries.GetQuizzes;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Infrastructure.Persistence;
using EnglishLearning.WebAPI.Models.Common;
using EnglishLearning.WebAPI.Models.Requests.Quizzes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearning.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class QuizzesController(
    IMediator _mediator,
    IQuizRepository _quizRepository,
    ApplicationDbContext _context) : ControllerBase 
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

    [HttpGet("{id}/take")]
    public async Task<IActionResult> GetForTake(Guid id)
    {
        var query = new GetQuizForTakeQuery(id);
        var dto = await _mediator.Send(query);

        return Ok(ApiResponse<QuizForTakeDto>.Ok(dto));
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

    // Question CRUD within a Quiz
    [HttpGet("{quizId}/questions/{questionId}")]
    public async Task<IActionResult> GetQuestion(Guid quizId, Guid questionId)
    {
        var query = new GetQuestionQuery(quizId, questionId);
        var dto = await _mediator.Send(query);

        return Ok(ApiResponse<QuestionDto>.Ok(dto));
    }

    [HttpPost("{quizId}/questions")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> AddQuestion(Guid quizId, [FromBody] QuestionRequest request)
    {
        var quizExists = await _context.Quizzes.AnyAsync(q => q.Id == quizId);
        if (!quizExists)
            return NotFound(ApiResponse<Guid>.NotFound("Quiz not found"));

        var question = new Domain.Entities.Question
        {
            QuestionText = request.QuestionText,
            QuestionType = request.QuestionType,
            Difficulty = request.Difficulty,
            CorrectAnswer = request.CorrectAnswer,
            QuizId = quizId
        };

        foreach (var c in request.Choices)
        {
            question.Choices.Add(new Domain.Entities.Choice
            {
                ChoiceText = c.ChoiceText,
                IsCorrect = c.IsCorrect,
                QuestionId = question.Id
            });
        }

        await _context.Questions.AddAsync(question);
        await _context.SaveChangesAsync();

        return Ok(ApiResponse<Guid>.Ok(question.Id, "Question added"));
    }

    [HttpPut("{quizId}/questions/{questionId}")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> UpdateQuestion(Guid quizId, Guid questionId, [FromBody] QuestionRequest request)
    {
        var question = await _context.Questions
            .Include(q => q.Choices)
            .FirstOrDefaultAsync(q => q.Id == questionId && q.QuizId == quizId);

        if (question == null)
            return NotFound(ApiResponse<Guid>.NotFound("Question not found"));

        question.QuestionText = request.QuestionText;
        question.QuestionType = request.QuestionType;
        question.Difficulty = request.Difficulty;
        question.CorrectAnswer = request.CorrectAnswer;
        question.UpdatedAt = DateTime.UtcNow;

        // Remove existing choices
        foreach (var choice in question.Choices.ToList())
        {
            _context.Choices.Remove(choice);
        }

        // Add new choices
        foreach (var c in request.Choices)
        {
            await _context.Choices.AddAsync(new Domain.Entities.Choice
            {
                ChoiceText = c.ChoiceText,
                IsCorrect = c.IsCorrect,
                QuestionId = question.Id
            });
        }

        await _context.SaveChangesAsync();

        return Ok(ApiResponse<Guid>.Ok(questionId, "Question updated"));
    }

    [HttpDelete("{quizId}/questions/{questionId}")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> DeleteQuestion(Guid quizId, Guid questionId)
    {
        var question = await _context.Questions
            .FirstOrDefaultAsync(q => q.Id == questionId && q.QuizId == quizId);

        if (question == null)
            return NotFound(ApiResponse<Guid>.NotFound("Question not found"));

        _context.Questions.Remove(question);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
