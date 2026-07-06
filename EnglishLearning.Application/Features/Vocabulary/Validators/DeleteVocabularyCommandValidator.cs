using EnglishLearning.Application.Features.Vocabulary.Commands.DeleteVocabulary;
using FluentValidation;

namespace EnglishLearning.Application.Features.Vocabulary.Validators;

public class DeleteVocabularyCommandValidator : AbstractValidator<DeleteVocabularyCommand>
{
    public DeleteVocabularyCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}
