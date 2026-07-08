using EnglishLearning.Application.Features.Leaderboard.Queries.GetUserRank;
using EnglishLearning.Domain.Interfaces;
using LeaderboardEntity = EnglishLearning.Domain.Entities.Leaderboard;

namespace EnglishLearnningApp.UnitTest.Application.Leaderboard;

public class GetUserRankQueryHandlerTests
{
    [Fact]
    public async Task Handle_UserExists_ShouldReturnRank()
    {
        var repo = new Mock<ILeaderboardRepository>();
        var lb1 = new LeaderboardEntity
        {
            UserId = "user-456",
            TotalScore = 0m,
            QuizzesCompleted = 0,
            AverageScore = 0m,
            Streak = 0,
            LastActiveDate = DateTime.UtcNow
        };
        lb1.TotalScore = 200;
        var lb2 = new LeaderboardEntity
        {
            UserId = "user-123",
            TotalScore = 0m,
            QuizzesCompleted = 0,
            AverageScore = 0m,
            Streak = 0,
            LastActiveDate = DateTime.UtcNow
        };
        lb2.TotalScore = 100;
        var lb3 = new LeaderboardEntity
        {
            UserId = "user-789",
            TotalScore = 0m,
            QuizzesCompleted = 0,
            AverageScore = 0m,
            Streak = 0,
            LastActiveDate = DateTime.UtcNow
        };
        lb3.TotalScore = 300;
        var leaderboards = new List<LeaderboardEntity> { lb1, lb2, lb3 };
        repo.Setup(r => r.GetAllAsync()).ReturnsAsync(leaderboards);

        var handler = new GetUserRankQueryHandler(repo.Object);
        var command = new GetUserRankQuery("user-123");

        var result = await handler.Handle(command, CancellationToken.None);
        // user-789 has 300 (rank 1), user-456 has 200 (rank 2), user-123 has 100 (rank 3)
        result.Should().Be(3);
    }

    [Fact]
    public async Task Handle_UserNotFound_ShouldThrowKeyNotFoundException()
    {
        var repo = new Mock<ILeaderboardRepository>();
        repo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<LeaderboardEntity>());

        var handler = new GetUserRankQueryHandler(repo.Object);
        var command = new GetUserRankQuery("nonexistent");

        var act = async () => await handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
}
