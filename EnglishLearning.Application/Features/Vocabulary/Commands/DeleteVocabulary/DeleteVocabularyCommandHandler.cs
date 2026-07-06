using EnglishLearning.Application.Common;
using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Vocabulary.Commands.DeleteVocabulary;

public class DeleteVocabularyCommandHandler : IRequestHandler<DeleteVocabularyCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteVocabularyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteVocabularyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Vocabularies.GetByIdAsync(request.Id);
        if (entity == null)
            return Result.Failure(VocabularyErrorMessages.NotFound);

        _unitOfWork.Vocabularies.Delete(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
