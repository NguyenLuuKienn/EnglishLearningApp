using EnglishLearning.Application.Features.Vocabulary.Commands.DeleteVocabulary;
using EnglishLearning.Application.Features.Vocabulary.Validators;
using FluentValidation.TestHelper;

namespace EnglishLearnningApp.UnitTest.Application.Vocabulary;

public class DeleteVocabularyCommandValidatorTests
{
    private readonly DeleteVocabularyCommandValidator _validator = new();

    [Fact]
    public void Should_Have_No_Error_When_Valid()
    {
        var command = new DeleteVocabularyCommand(Guid.NewGuid());
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Have_Error_When_Id_Is_Empty()
    {
        var command = new DeleteVocabularyCommand(Guid.Empty);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}
