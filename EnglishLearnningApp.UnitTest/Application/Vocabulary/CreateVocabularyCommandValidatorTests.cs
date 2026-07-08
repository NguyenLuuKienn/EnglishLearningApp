using EnglishLearning.Application.Features.Vocabulary.Commands.CreateVocabulary;
using EnglishLearning.Application.Features.Vocabulary.Validators;
using EnglishLearning.Domain.Enums;
using FluentValidation.TestHelper;

namespace EnglishLearnningApp.UnitTest.Application.Vocabulary;

public class CreateVocabularyCommandValidatorTests
{
    private readonly CreateVocabularyCommandValidator _validator = new();

    [Fact]
    public void Should_Have_No_Error_When_Valid()
    {
        var command = new CreateVocabularyCommand("Hello", "A greeting", "Hello world", "Interjection", DifficultyLevel.Beginner);
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Have_Error_When_Word_Is_Empty()
    {
        var command = new CreateVocabularyCommand("", "A greeting", null, null, DifficultyLevel.Beginner);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Word);
    }

    [Fact]
    public void Should_Have_Error_When_Definition_Is_Empty()
    {
        var command = new CreateVocabularyCommand("Hello", "", null, null, DifficultyLevel.Beginner);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Definition);
    }

    [Fact]
    public void Should_Have_Error_When_Word_Exceeds_MaxLength()
    {
        var longWord = new string('a', 201);
        var command = new CreateVocabularyCommand(longWord, "Def", null, null, DifficultyLevel.Beginner);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Word);
    }
}
