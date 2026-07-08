using EnglishLearning.Application.Features.QuizResults.Commands.SubmitQuizResult;
using EnglishLearning.Application.Features.QuizResults.Validators;
using FluentValidation.TestHelper;

namespace EnglishLearnningApp.UnitTest.Application.QuizResults;

public class SubmitQuizResultCommandValidatorTests
{
    private readonly SubmitQuizResultCommandValidator _validator = new();

    [Fact]
    public void Should_Have_No_Error_When_Valid()
    {
        var command = new SubmitQuizResultCommand(
            Guid.NewGuid(), "user-123", 15,
            new List<AnswerCommand> { new(Guid.NewGuid(), null, null) });

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Have_Error_When_QuizId_Is_Empty()
    {
        var command = new SubmitQuizResultCommand(Guid.Empty, "user", 15, new List<AnswerCommand>());
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.QuizId);
    }

    [Fact]
    public void Should_Have_Error_When_UserId_Is_Empty()
    {
        var command = new SubmitQuizResultCommand(Guid.NewGuid(), "", 15, new List<AnswerCommand>());
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Fact]
    public void Should_Have_Error_When_Answers_Is_Empty()
    {
        var command = new SubmitQuizResultCommand(Guid.NewGuid(), "user", 15, new List<AnswerCommand>());
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Answers);
    }

    [Fact]
    public void Should_Have_Error_When_Duration_Is_Negative()
    {
        var command = new SubmitQuizResultCommand(Guid.NewGuid(), "user", -1, new List<AnswerCommand> { new(Guid.NewGuid(), null, null) });
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.DurationMinutes);
    }
}
