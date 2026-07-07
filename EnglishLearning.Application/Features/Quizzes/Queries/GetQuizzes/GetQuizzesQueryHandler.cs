using AutoMapper;
using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;

namespace EnglishLearning.Application.Features.Quizzes.Queries.GetQuizzes;

public class GetQuizzesQueryHandler(
    IQuizRepository _quizRepository, 
    IMapper _mapper) : IRequestHandler<GetQuizzesQuery, PagedResult<QuizDto>>
{
    public async Task<PagedResult<QuizDto>> Handle(GetQuizzesQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Quiz, bool>>? predicate = request.Difficulty.HasValue
            ? q => q.Difficulty == request.Difficulty.Value
            : null;

        var (items, totalRecords) = await _quizRepository.GetPagedAsync(
            request.PageNumber,
            request.PageSize,
            predicate,
            (Expression<Func<Quiz, object>>) (q => q.CreatedAt),
            false);

        var dtos = _mapper.Map<List<QuizDto>>(items);

        return PagedResult<QuizDto>.Create(
            dtos, request.PageNumber, request.PageSize, totalRecords);
    }
}
