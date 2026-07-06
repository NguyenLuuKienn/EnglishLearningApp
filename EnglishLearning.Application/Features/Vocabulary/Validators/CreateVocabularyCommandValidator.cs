using EnglishLearning.Application.Features.Vocabulary.Commands.CreateVocabulary;
using FluentValidation;

namespace EnglishLearning.Application.Features.Vocabulary.Validators;

public class CreateVocabularyCommandValidator : AbstractValidator<CreateVocabularyCommand>
{
    public CreateVocabularyCommandValidator()
    {
        RuleFor(x => x.Word)
            .NotEmpty().WithMessage("Word is required")
            .MaximumLength(200).WithMessage("Word must not exceed 200 characters");

        RuleFor(x => x.Definition)
            .NotEmpty().WithMessage("Definition is required")
            .MaximumLength(1000).WithMessage("Definition must not exceed 1000 characters");

        RuleFor(x => x.Example)
            .MaximumLength(1000).WithMessage("Example must not exceed 1000 characters");

        RuleFor(x => x.PartOfSpeech)
            .MaximumLength(50).WithMessage("Part of speech must not exceed 50 characters");
    }
}
