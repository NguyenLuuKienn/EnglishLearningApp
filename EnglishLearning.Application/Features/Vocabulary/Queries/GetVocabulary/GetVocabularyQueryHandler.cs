using AutoMapper;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Vocabulary.Queries.GetVocabulary;

public class GetVocabularyQueryHandler(
    IVocabularyRepository _vocabularyRepository, 
    IMapper _mapper) : IRequestHandler<GetVocabularyQuery, VocabularyDto>
{
    public async Task<VocabularyDto> Handle(GetVocabularyQuery request, CancellationToken cancellationToken)
    {
        var entity = await _vocabularyRepository.GetByIdAsync(request.Id);
        if (entity == null)
            throw new KeyNotFoundException(VocabularyErrorMessages.NotFound);

        return _mapper.Map<VocabularyDto>(entity);
    }
}
