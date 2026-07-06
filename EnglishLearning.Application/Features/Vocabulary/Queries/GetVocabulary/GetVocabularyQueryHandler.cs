using AutoMapper;
using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Vocabulary.Queries.GetVocabulary;

public class GetVocabularyQueryHandler : IRequestHandler<GetVocabularyQuery, Result<VocabularyDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetVocabularyQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<VocabularyDto>> Handle(GetVocabularyQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Vocabularies.GetByIdAsync(request.Id);
        if (entity == null)
            return Result<VocabularyDto>.Failure(VocabularyErrorMessages.NotFound);

        return Result<VocabularyDto>.Success(_mapper.Map<VocabularyDto>(entity));
    }
}
