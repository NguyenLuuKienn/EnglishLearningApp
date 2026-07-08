using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Leaderboard.Queries.GetLeaderboard;
using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Domain.Entities;
using AutoMapper;
using LeaderboardEntity = EnglishLearning.Domain.Entities.Leaderboard;

namespace EnglishLearnningApp.UnitTest.Application.Leaderboard;

public class GetLeaderboardQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnTopUsers()
    {
        var leaderboardRepo = new Mock<ILeaderboardRepository>();
        var userRepo = new Mock<IUserRepository>();

        var leaderboards = new List<LeaderboardEntity>
        {
            new LeaderboardEntity { UserId = "user1", TotalScore = 0m, QuizzesCompleted = 0, AverageScore = 0m, Streak = 0, LastActiveDate = DateTime.UtcNow },
            new LeaderboardEntity { UserId = "user2", TotalScore = 0m, QuizzesCompleted = 0, AverageScore = 0m, Streak = 0, LastActiveDate = DateTime.UtcNow }
        };

        leaderboardRepo.Setup(r => r.GetTopUsersAsync(10)).ReturnsAsync(leaderboards);

        var handler = new GetLeaderboardQueryHandler(leaderboardRepo.Object, userRepo.Object);
        var query = new GetLeaderboardQuery(10);

        var result = await handler.Handle(query, CancellationToken.None);
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
    }
}
