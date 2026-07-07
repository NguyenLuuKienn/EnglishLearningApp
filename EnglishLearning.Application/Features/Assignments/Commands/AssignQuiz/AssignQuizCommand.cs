using EnglishLearning.Domain.Enums;
using MediatR;

namespace EnglishLearning.Application.Features.Assignments.Commands.AssignQuiz;

public record AssignQuizCommand(
    Guid QuizId,
    UserRole? TargetRole,
    string? TargetUserId,
    DateTime StartTime,
    DateTime EndTime) : IRequest<Guid>;
