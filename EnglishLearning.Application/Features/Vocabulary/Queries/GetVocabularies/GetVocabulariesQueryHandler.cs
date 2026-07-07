using AutoMapper;
using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;
using VocabularyEntity = EnglishLearning.Domain.Entities.Vocabulary;

namespace EnglishLearning.Application.Features.Vocabulary.Queries.GetVocabularies;

public class GetVocabulariesQueryHandler(
    IVocabularyRepository _vocabularyRepository, 
    IMapper _mapper) : IRequestHandler<GetVocabulariesQuery, PagedResult<VocabularyDto>>
{
    public async Task<PagedResult<VocabularyDto>> Handle(GetVocabulariesQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<VocabularyEntity, bool>>? predicate = request.Difficulty.HasValue
            ? v => v.Difficulty == request.Difficulty.Value
            : null;

        var (items, totalRecords) = await _vocabularyRepository.GetPagedAsync(
            request.PageNumber,
            request.PageSize,
            predicate,
            (Expression<Func<VocabularyEntity, object>>) (v => v.CreatedAt),
            false);

        var dtos = _mapper.Map<List<VocabularyDto>>(items);

        return PagedResult<VocabularyDto>.Create(
            dtos, request.PageNumber, request.PageSize, totalRecords);
    }
}
