using AutoMapper;
using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.History.Queries.GetUserHistory;

public class GetUserHistoryQueryHandler(
    ILearningHistoryRepository _historyRepository,
    IMapper _mapper) : IRequestHandler<GetUserHistoryQuery, PagedResult<LearningHistoryDto>>
{
    public async Task<PagedResult<LearningHistoryDto>> Handle(GetUserHistoryQuery request, CancellationToken cancellationToken)
    {
        var (items, totalRecords) = await _historyRepository.GetByUserIdAsync(
            request.UserId, request.PageNumber, request.PageSize);

        var dtos = _mapper.Map<List<LearningHistoryDto>>(items);

        return PagedResult<LearningHistoryDto>.Create(dtos, request.PageNumber, request.PageSize, totalRecords);
    }
}
