using EnglishLearning.Domain.Interfaces;
using MediatR;
using VocabularyEntity = EnglishLearning.Domain.Entities.Vocabulary;

namespace EnglishLearning.Application.Features.Vocabulary.Commands.CreateVocabulary;

public class CreateVocabularyCommandHandler(
    IVocabularyRepository _vocabularyRepository) : IRequestHandler<CreateVocabularyCommand, Guid>
{
    public async Task<Guid> Handle(CreateVocabularyCommand request, CancellationToken cancellationToken)
    {
        var entity = new VocabularyEntity
        {
            Word = request.Word,
            Definition = request.Definition,
            Example = request.Example,
            PartOfSpeech = request.PartOfSpeech,
            Difficulty = request.Difficulty
        };

        await _vocabularyRepository.AddAsync(entity);
        await _vocabularyRepository.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
