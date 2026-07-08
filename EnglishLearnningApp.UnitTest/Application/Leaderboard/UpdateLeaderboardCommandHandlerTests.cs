using EnglishLearning.Application.Features.Leaderboard.Commands.UpdateLeaderboard;
using EnglishLearning.Domain.Interfaces;
using LeaderboardEntity = EnglishLearning.Domain.Entities.Leaderboard;

namespace EnglishLearnningApp.UnitTest.Application.Leaderboard;

public class UpdateLeaderboardCommandHandlerTests
{
    [Fact]
    public async Task Handle_NewUser_ShouldCreateLeaderboardEntry()
    {
        var leaderboardRepo = new Mock<ILeaderboardRepository>();
        var uow = new Mock<IUnitOfWork>();

        leaderboardRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<LeaderboardEntity>());

        var handler = new UpdateLeaderboardCommandHandler(leaderboardRepo.Object, uow.Object);
        var command = new UpdateLeaderboardCommand("user-123", 80m);

        await handler.Handle(command, CancellationToken.None);

        leaderboardRepo.Verify(r => r.AddAsync(It.IsAny<LeaderboardEntity>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ExistingUser_ShouldUpdateLeaderboard()
    {
        var leaderboardRepo = new Mock<ILeaderboardRepository>();
        var uow = new Mock<IUnitOfWork>();

        var leaderboard = new LeaderboardEntity
        {
            UserId = "user-123",
            TotalScore = 0m,
            QuizzesCompleted = 0,
            AverageScore = 0m,
            Streak = 0,
            LastActiveDate = DateTime.UtcNow
        };
        leaderboardRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<LeaderboardEntity> { leaderboard });

        var handler = new UpdateLeaderboardCommandHandler(leaderboardRepo.Object, uow.Object);
        var command = new UpdateLeaderboardCommand("user-123", 80m);

        await handler.Handle(command, CancellationToken.None);

        leaderboard.QuizzesCompleted.Should().Be(1);
    }
}
