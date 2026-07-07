using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Vocabulary.Commands.DeleteVocabulary;

public class DeleteVocabularyCommandHandler(
    IVocabularyRepository _vocabularyRepository) : IRequestHandler<DeleteVocabularyCommand>
{
    public async Task Handle(DeleteVocabularyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _vocabularyRepository.GetByIdAsync(request.Id);
        if (entity == null)
            throw new KeyNotFoundException(VocabularyErrorMessages.NotFound);

        _vocabularyRepository.Delete(entity);
        await _vocabularyRepository.SaveChangesAsync(cancellationToken);
    }
}
