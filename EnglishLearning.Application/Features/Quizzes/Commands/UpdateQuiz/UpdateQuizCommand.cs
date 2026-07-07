using EnglishLearning.Domain.Enums;
using MediatR;

namespace EnglishLearning.Application.Features.Quizzes.Commands.UpdateQuiz;

public record UpdateQuizCommand(
    Guid Id,
    string Title,
    string? Description,
    DifficultyLevel Difficulty,
    int TimeLimitMinutes,
    decimal PassingScore
) : IRequest<Guid>;
