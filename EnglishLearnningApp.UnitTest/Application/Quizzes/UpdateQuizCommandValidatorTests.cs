using EnglishLearning.Application.Features.Quizzes.Commands.UpdateQuiz;
using EnglishLearning.Application.Features.Quizzes.Validators;
using EnglishLearning.Domain.Enums;
using FluentValidation.TestHelper;

namespace EnglishLearnningApp.UnitTest.Application.Quizzes;

public class UpdateQuizCommandValidatorTests
{
    private readonly UpdateQuizCommandValidator _validator = new();

    [Fact]
    public void Should_Have_No_Error_When_Valid()
    {
        var command = new UpdateQuizCommand(Guid.NewGuid(), "Title", null, DifficultyLevel.Beginner, 30, 50m);
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Have_Error_When_Id_Is_Empty()
    {
        var command = new UpdateQuizCommand(Guid.Empty, "Title", null, DifficultyLevel.Beginner, 30, 50m);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Should_Have_Error_When_Title_Is_Empty()
    {
        var command = new UpdateQuizCommand(Guid.NewGuid(), "", null, DifficultyLevel.Beginner, 30, 50m);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }
}
