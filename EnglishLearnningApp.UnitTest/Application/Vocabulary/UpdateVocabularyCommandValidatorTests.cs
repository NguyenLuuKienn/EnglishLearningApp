using EnglishLearning.Application.Features.Vocabulary.Commands.UpdateVocabulary;
using EnglishLearning.Application.Features.Vocabulary.Validators;
using EnglishLearning.Domain.Enums;
using FluentValidation.TestHelper;

namespace EnglishLearnningApp.UnitTest.Application.Vocabulary;

public class UpdateVocabularyCommandValidatorTests
{
    private readonly UpdateVocabularyCommandValidator _validator = new();

    [Fact]
    public void Should_Have_No_Error_When_Valid()
    {
        var command = new UpdateVocabularyCommand(
            Guid.NewGuid(), "Hello", "A greeting", "Hello world", "Interjection", DifficultyLevel.Beginner);
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Have_Error_When_Id_Is_Empty()
    {
        var command = new UpdateVocabularyCommand(
            Guid.Empty, "Hello", "A greeting", null, null, DifficultyLevel.Beginner);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Should_Have_Error_When_Word_Is_Empty()
    {
        var command = new UpdateVocabularyCommand(
            Guid.NewGuid(), "", "A greeting", null, null, DifficultyLevel.Beginner);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Word);
    }

    [Fact]
    public void Should_Have_Error_When_Definition_Is_Empty()
    {
        var command = new UpdateVocabularyCommand(
            Guid.NewGuid(), "Hello", "", null, null, DifficultyLevel.Beginner);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Definition);
    }
}
