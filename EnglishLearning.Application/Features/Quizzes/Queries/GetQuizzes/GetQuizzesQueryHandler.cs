using AutoMapper;
using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;

namespace EnglishLearning.Application.Features.Quizzes.Queries.GetQuizzes;

public class GetQuizzesQueryHandler : IRequestHandler<GetQuizzesQuery, Result<PagedResult<QuizDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetQuizzesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<QuizDto>>> Handle(GetQuizzesQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Quiz, bool>>? predicate = request.Difficulty.HasValue
            ? q => q.Difficulty == request.Difficulty.Value
            : null;

        var (items, totalRecords) = await _unitOfWork.Quizzes.GetPagedAsync(
            request.PageNumber,
            request.PageSize,
            predicate,
            (Expression<Func<Quiz, object>>) (q => q.CreatedAt),
            false);

        var dtos = _mapper.Map<List<QuizDto>>(items);

        var pagedResult = PagedResult<QuizDto>.Create(
            dtos, request.PageNumber, request.PageSize, totalRecords);

        return Result<PagedResult<QuizDto>>.Success(pagedResult);
    }
}
