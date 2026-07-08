using EnglishLearning.Application.Features.History.Commands.RecordHistory;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using LearningHistoryEntity = EnglishLearning.Domain.Entities.LearningHistory;

namespace EnglishLearnningApp.UnitTest.Application.History;

public class RecordHistoryCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidCommand_ShouldRecordHistory()
    {
        var repo = new Mock<ILearningHistoryRepository>();
        var uow = new Mock<IUnitOfWork>();
        var handler = new RecordHistoryCommandHandler(repo.Object, uow.Object);

        var command = new RecordHistoryCommand("user-123", ActionType.CompleteQuiz, Guid.NewGuid(), "Completed", 85m);
        await handler.Handle(command, CancellationToken.None);

        repo.Verify(r => r.AddAsync(It.IsAny<LearningHistoryEntity>()), Times.Once);
    }
}
