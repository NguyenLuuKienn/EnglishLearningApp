using EnglishLearning.Domain.Entities;

namespace EnglishLearnningApp.UnitTest.Domain.Entities;

public class LeaderboardTests
{
    [Fact]
    public void Create_ShouldSetDefaults()
    {
        var leaderboard = new Leaderboard
        {
            UserId = "user-123",
            TotalScore = 0m,
            QuizzesCompleted = 0,
            AverageScore = 0m,
            Streak = 0,
            LastActiveDate = DateTime.UtcNow
        };

        leaderboard.UserId.Should().Be("user-123");
        leaderboard.TotalScore.Should().Be(0m);
        leaderboard.QuizzesCompleted.Should().Be(0);
        leaderboard.AverageScore.Should().Be(0m);
        leaderboard.Streak.Should().Be(0);
    }

    [Fact]
    public void UpdateScore_ShouldRecalculateAverage()
    {
        var leaderboard = new Leaderboard
        {
            UserId = "user-123",
            TotalScore = 0m,
            QuizzesCompleted = 0,
            AverageScore = 0m,
            Streak = 0,
            LastActiveDate = DateTime.UtcNow
        };
        leaderboard.UpdateScore(80m);
        leaderboard.UpdateScore(90m);

        leaderboard.QuizzesCompleted.Should().Be(2);
        leaderboard.TotalScore.Should().Be(170m);
        leaderboard.AverageScore.Should().Be(85m);
    }

    [Fact]
    public void UpdateScore_ShouldIncrementStreak()
    {
        var leaderboard = new Leaderboard
        {
            UserId = "user-123",
            TotalScore = 0m,
            QuizzesCompleted = 0,
            AverageScore = 0m,
            Streak = 0,
            LastActiveDate = DateTime.UtcNow
        };
        leaderboard.UpdateScore(80m);
        leaderboard.Streak.Should().Be(1);
    }
}
