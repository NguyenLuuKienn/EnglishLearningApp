using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using MediatR;

namespace EnglishLearning.Application.Features.History.Queries.GetUserHistory;

public record GetUserHistoryQuery(
    string UserId,
    int PageNumber,
    int PageSize) : IRequest<PagedResult<LearningHistoryDto>>;
