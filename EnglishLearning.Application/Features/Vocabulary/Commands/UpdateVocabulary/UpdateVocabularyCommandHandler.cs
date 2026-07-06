using EnglishLearning.Application.Common;
using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Vocabulary.Commands.UpdateVocabulary;

public class UpdateVocabularyCommandHandler : IRequestHandler<UpdateVocabularyCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateVocabularyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(UpdateVocabularyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Vocabularies.GetByIdAsync(request.Id);
        if (entity == null)
            return Result<Guid>.Failure(VocabularyErrorMessages.NotFound);

        entity.Word = request.Word;
        entity.Definition = request.Definition;
        entity.Example = request.Example;
        entity.PartOfSpeech = request.PartOfSpeech;
        entity.Difficulty = request.Difficulty;
        entity.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Vocabularies.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(entity.Id);
    }
}
