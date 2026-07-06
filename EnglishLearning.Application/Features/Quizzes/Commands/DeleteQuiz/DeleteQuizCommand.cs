using EnglishLearning.Application.Common;
using MediatR;

namespace EnglishLearning.Application.Features.Quizzes.Commands.DeleteQuiz;

public record DeleteQuizCommand(Guid Id) : IRequest<Result>;
