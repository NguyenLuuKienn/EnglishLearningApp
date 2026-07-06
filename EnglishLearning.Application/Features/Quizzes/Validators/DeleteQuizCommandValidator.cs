using EnglishLearning.Application.Features.Quizzes.Commands.DeleteQuiz;
using FluentValidation;

namespace EnglishLearning.Application.Features.Quizzes.Validators;

public class DeleteQuizCommandValidator : AbstractValidator<DeleteQuizCommand>
{
    public DeleteQuizCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}
