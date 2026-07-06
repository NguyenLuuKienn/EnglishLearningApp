using AutoMapper;
using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;

namespace EnglishLearning.Application.Features.QuizResults.Queries.GetUserQuizResults;

public class GetUserQuizResultsQueryHandler : IRequestHandler<GetUserQuizResultsQuery, Result<PagedResult<QuizResultDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserQuizResultsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<QuizResultDto>>> Handle(GetUserQuizResultsQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<QuizResult, bool>> predicate = r => r.UserId == request.UserId;

        var (items, totalRecords) = await _unitOfWork.QuizResults.GetPagedAsync(
            request.PageNumber,
            request.PageSize,
            predicate,
            (Expression<Func<QuizResult, object>>) (r => r.CompletedAt),
            false);

        var dtos = _mapper.Map<List<QuizResultDto>>(items);

        var pagedResult = PagedResult<QuizResultDto>.Create(
            dtos, request.PageNumber, request.PageSize, totalRecords);

        return Result<PagedResult<QuizResultDto>>.Success(pagedResult);
    }
}
