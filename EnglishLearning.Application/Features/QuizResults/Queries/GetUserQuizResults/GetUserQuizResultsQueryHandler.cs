using AutoMapper;
using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;

namespace EnglishLearning.Application.Features.QuizResults.Queries.GetUserQuizResults;

public class GetUserQuizResultsQueryHandler(IQuizResultRepository _quizResultRepository, IMapper _mapper) : IRequestHandler<GetUserQuizResultsQuery, PagedResult<QuizResultDto>>
{
    public async Task<PagedResult<QuizResultDto>> Handle(GetUserQuizResultsQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<QuizResult, bool>> predicate = r => r.UserId == request.UserId;

        var (items, totalRecords) = await _quizResultRepository.GetPagedAsync(
            request.PageNumber,
            request.PageSize,
            predicate,
            (Expression<Func<QuizResult, object>>) (r => r.CompletedAt),
            false);

        var dtos = _mapper.Map<List<QuizResultDto>>(items);

        return PagedResult<QuizResultDto>.Create(
            dtos, request.PageNumber, request.PageSize, totalRecords);
    }
}
