using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Vocabulary.Commands.UpdateVocabulary;

public class UpdateVocabularyCommandHandler(
    IVocabularyRepository _vocabularyRepository) : IRequestHandler<UpdateVocabularyCommand, Guid>
{
    public async Task<Guid> Handle(UpdateVocabularyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _vocabularyRepository.GetByIdAsync(request.Id);
        if (entity == null)
            throw new KeyNotFoundException(VocabularyErrorMessages.NotFound);

        entity.Word = request.Word;
        entity.Definition = request.Definition;
        entity.Example = request.Example;
        entity.PartOfSpeech = request.PartOfSpeech;
        entity.Difficulty = request.Difficulty;
        entity.UpdatedAt = DateTime.UtcNow;

        _vocabularyRepository.Update(entity);
        await _vocabularyRepository.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
