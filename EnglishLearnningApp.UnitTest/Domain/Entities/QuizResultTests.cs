using EnglishLearning.Domain.Entities;

namespace EnglishLearnningApp.UnitTest.Domain.Entities;

public class QuizResultTests
{
    [Fact]
    public void Create_ShouldCalculateScoreCorrectly()
    {
        var totalQuestions = 10;
        var correctAnswers = 7;
        var score = totalQuestions > 0 ? (decimal)Math.Round((correctAnswers / (double)totalQuestions) * 100, 2) : 0m;
        var result = new QuizResult
        {
            QuizId = Guid.NewGuid(),
            UserId = "user-123",
            TotalQuestions = totalQuestions,
            CorrectAnswers = correctAnswers,
            DurationMinutes = 15,
            Score = score
        };

        result.TotalQuestions.Should().Be(10);
        result.CorrectAnswers.Should().Be(7);
        result.Score.Should().Be(70m);
    }

    [Fact]
    public void Create_WithAllCorrect_ShouldReturn100Percent()
    {
        var totalQuestions = 5;
        var correctAnswers = 5;
        var score = totalQuestions > 0 ? (decimal)Math.Round((correctAnswers / (double)totalQuestions) * 100, 2) : 0m;
        var result = new QuizResult
        {
            QuizId = Guid.NewGuid(),
            UserId = "user",
            TotalQuestions = totalQuestions,
            CorrectAnswers = correctAnswers,
            DurationMinutes = 10,
            Score = score
        };
        result.Score.Should().Be(100m);
    }

    [Fact]
    public void Create_WithNoCorrect_ShouldReturn0Percent()
    {
        var totalQuestions = 5;
        var correctAnswers = 0;
        var score = totalQuestions > 0 ? (decimal)Math.Round((correctAnswers / (double)totalQuestions) * 100, 2) : 0m;
        var result = new QuizResult
        {
            QuizId = Guid.NewGuid(),
            UserId = "user",
            TotalQuestions = totalQuestions,
            CorrectAnswers = correctAnswers,
            DurationMinutes = 10,
            Score = score
        };
        result.Score.Should().Be(0m);
    }

    [Fact]
    public void Create_WithZeroQuestions_ShouldReturn0Percent()
    {
        var totalQuestions = 0;
        var correctAnswers = 0;
        var score = totalQuestions > 0 ? (decimal)Math.Round((correctAnswers / (double)totalQuestions) * 100, 2) : 0m;
        var result = new QuizResult
        {
            QuizId = Guid.NewGuid(),
            UserId = "user",
            TotalQuestions = totalQuestions,
            CorrectAnswers = correctAnswers,
            DurationMinutes = 10,
            Score = score
        };
        result.Score.Should().Be(0m);
    }
}
