using EnglishLearning.Application.Features.Quizzes.Commands.DeleteQuiz;
using EnglishLearning.Application.Features.Quizzes.Validators;
using FluentValidation.TestHelper;

namespace EnglishLearnningApp.UnitTest.Application.Quizzes;

public class DeleteQuizCommandValidatorTests
{
    private readonly DeleteQuizCommandValidator _validator = new();

    [Fact]
    public void Should_Have_No_Error_When_Valid()
    {
        var command = new DeleteQuizCommand(Guid.NewGuid());
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Have_Error_When_Id_Is_Empty()
    {
        var command = new DeleteQuizCommand(Guid.Empty);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}
