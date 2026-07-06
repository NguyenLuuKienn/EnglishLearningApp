using AutoMapper;
using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;
using VocabularyEntity = EnglishLearning.Domain.Entities.Vocabulary;

namespace EnglishLearning.Application.Features.Vocabulary.Queries.GetVocabularies;

public class GetVocabulariesQueryHandler : IRequestHandler<GetVocabulariesQuery, Result<PagedResult<VocabularyDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetVocabulariesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<VocabularyDto>>> Handle(GetVocabulariesQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<VocabularyEntity, bool>>? predicate = request.Difficulty.HasValue
            ? v => v.Difficulty == request.Difficulty.Value
            : null;

        var (items, totalRecords) = await _unitOfWork.Vocabularies.GetPagedAsync(
            request.PageNumber,
            request.PageSize,
            predicate,
            (Expression<Func<VocabularyEntity, object>>) (v => v.CreatedAt),
            false);

        var dtos = _mapper.Map<List<VocabularyDto>>(items);

        var pagedResult = PagedResult<VocabularyDto>.Create(
            dtos, request.PageNumber, request.PageSize, totalRecords);

        return Result<PagedResult<VocabularyDto>>.Success(pagedResult);
    }
}
