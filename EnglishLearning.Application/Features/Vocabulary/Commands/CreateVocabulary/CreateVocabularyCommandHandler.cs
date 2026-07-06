using EnglishLearning.Application.Common;
using EnglishLearning.Domain.Interfaces;
using MediatR;
using VocabularyEntity = EnglishLearning.Domain.Entities.Vocabulary;

namespace EnglishLearning.Application.Features.Vocabulary.Commands.CreateVocabulary;

public class CreateVocabularyCommandHandler : IRequestHandler<CreateVocabularyCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateVocabularyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateVocabularyCommand request, CancellationToken cancellationToken)
    {
        var entity = new VocabularyEntity
        {
            Word = request.Word,
            Definition = request.Definition,
            Example = request.Example,
            PartOfSpeech = request.PartOfSpeech,
            Difficulty = request.Difficulty
        };

        await _unitOfWork.Vocabularies.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(entity.Id);
    }
}
