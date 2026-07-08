using EnglishLearning.Application.Common;

namespace EnglishLearnningApp.UnitTest.Application.Common;

public class ResultTests
{
    [Fact]
    public void Success_WithNoValue_ShouldSetIsSuccessTrue()
    {
        var result = Result.Success();
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().BeNull();
    }

    [Fact]
    public void Success_WithValue_ShouldSetIsSuccessTrueAndValue()
    {
        var value = Guid.NewGuid();
        var result = Result<Guid>.Success(value);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(value);
    }

    [Fact]
    public void Failure_WithMessage_ShouldSetIsSuccessFalse()
    {
        var result = Result.Failure("Something went wrong");
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("Something went wrong");
    }

    [Fact]
    public void Failure_WithErrors_ShouldSetErrorsCollection()
    {
        var errors = new List<string> { "Error 1", "Error 2" };
        var result = Result.Failure(errors);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainInOrder(errors);
    }
}
