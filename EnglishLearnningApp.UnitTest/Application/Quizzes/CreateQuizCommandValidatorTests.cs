using EnglishLearning.Application.Features.Quizzes.Commands.CreateQuiz;
using EnglishLearning.Application.Features.Quizzes.Validators;
using EnglishLearning.Domain.Enums;
using FluentValidation.TestHelper;

namespace EnglishLearnningApp.UnitTest.Application.Quizzes;

public class CreateQuizCommandValidatorTests
{
    private readonly CreateQuizCommandValidator _validator = new();

    [Fact]
    public void Should_Have_No_Error_When_Valid()
    {
        var command = new CreateQuizCommand(
            "Test Quiz", "Description", DifficultyLevel.Beginner, 30, 50m,
            new List<QuestionCommand>
            {
                new("Q?", QuestionType.MultipleChoice, DifficultyLevel.Beginner, "A",
                    new List<ChoiceCommand> { new("A", true) })
            });

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Have_Error_When_Title_Is_Empty()
    {
        var command = new CreateQuizCommand("", null, DifficultyLevel.Beginner, 30, 50m, new List<QuestionCommand>());
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void Should_Have_Error_When_PassingScore_Exceeds_100()
    {
        var command = new CreateQuizCommand("Quiz", null, DifficultyLevel.Beginner, 30, 150m, new List<QuestionCommand>());
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.PassingScore);
    }

    [Fact]
    public void Should_Have_Error_When_TimeLimit_Is_Negative()
    {
        var command = new CreateQuizCommand("Quiz", null, DifficultyLevel.Beginner, -1, 50m, new List<QuestionCommand>());
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.TimeLimitMinutes);
    }
}
