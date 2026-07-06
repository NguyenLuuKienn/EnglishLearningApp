using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using MediatR;

namespace EnglishLearning.Application.Features.QuizResults.Queries.GetUserQuizResults;

public record GetUserQuizResultsQuery(
    string UserId,
    int PageNumber,
    int PageSize
) : IRequest<Result<PagedResult<QuizResultDto>>>;
