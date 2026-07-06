using EnglishLearning.Application.Features.QuizResults.Commands.SubmitQuizResult;
using FluentValidation;

namespace EnglishLearning.Application.Features.QuizResults.Validators;

public class SubmitQuizResultCommandValidator : AbstractValidator<SubmitQuizResultCommand>
{
    public SubmitQuizResultCommandValidator()
    {
        RuleFor(x => x.QuizId)
            .NotEmpty().WithMessage("QuizId is required");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required");

        RuleFor(x => x.DurationMinutes)
            .GreaterThanOrEqualTo(0).WithMessage("Duration must be 0 or greater");

        RuleFor(x => x.Answers)
            .NotEmpty().WithMessage("At least one answer is required");
    }
}
