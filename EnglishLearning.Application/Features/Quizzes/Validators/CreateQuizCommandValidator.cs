using EnglishLearning.Application.Features.Quizzes.Commands.CreateQuiz;
using FluentValidation;

namespace EnglishLearning.Application.Features.Quizzes.Validators;

public class CreateQuizCommandValidator : AbstractValidator<CreateQuizCommand>
{
    public CreateQuizCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");

        RuleFor(x => x.TimeLimitMinutes)
            .GreaterThanOrEqualTo(0).WithMessage("Time limit must be 0 or greater");

        RuleFor(x => x.PassingScore)
            .InclusiveBetween(0, 100).WithMessage("Passing score must be between 0 and 100");

        RuleFor(x => x.Questions)
            .NotEmpty().WithMessage("Quiz must have at least one question");
    }
}
