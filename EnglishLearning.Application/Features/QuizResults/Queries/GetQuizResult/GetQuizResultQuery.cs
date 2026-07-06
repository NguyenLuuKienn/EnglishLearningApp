using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using MediatR;

namespace EnglishLearning.Application.Features.QuizResults.Queries.GetQuizResult;

public record GetQuizResultQuery(Guid Id) : IRequest<Result<QuizResultDto>>;
