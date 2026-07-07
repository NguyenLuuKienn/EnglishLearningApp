using EnglishLearning.Application.DTOs;
using MediatR;

namespace EnglishLearning.Application.Features.QuizResults.Commands.SubmitQuizResult;

public record AnswerCommand(
    Guid QuestionId,
    Guid? SelectedChoiceId,
    string? AnswerText
);

public record SubmitQuizResultCommand(
    Guid QuizId,
    string UserId,
    int DurationMinutes,
    List<AnswerCommand> Answers
) : IRequest<QuizResultDto>;
