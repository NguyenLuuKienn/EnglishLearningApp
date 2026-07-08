using EnglishLearning.Domain.Enums;
using MediatR;

namespace EnglishLearning.Application.Features.Quizzes.Commands.CreateQuiz;

public record ChoiceCommand(
    string ChoiceText,
    bool IsCorrect
);

public record QuestionCommand(
    string QuestionText,
    QuestionType QuestionType,
    DifficultyLevel Difficulty,
    string? CorrectAnswer,
    List<ChoiceCommand> Choices
);

public record CreateQuizCommand(
    string Title,
    string? Description,
    DifficultyLevel Difficulty,
    int TimeLimitMinutes,
    decimal PassingScore,
    List<QuestionCommand>? Questions
) : IRequest<Guid>;
