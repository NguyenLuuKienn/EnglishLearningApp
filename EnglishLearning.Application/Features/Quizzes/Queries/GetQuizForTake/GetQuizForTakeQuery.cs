using EnglishLearning.Application.DTOs;
using MediatR;

namespace EnglishLearning.Application.Features.Quizzes.Queries.GetQuizForTake;

public record GetQuizForTakeQuery(Guid Id) : IRequest<QuizForTakeDto>;
