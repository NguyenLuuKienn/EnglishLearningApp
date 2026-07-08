using EnglishLearning.Application.Features.Notifications.Commands.MarkNotificationRead;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using NotificationEntity = EnglishLearning.Domain.Entities.Notification;

namespace EnglishLearnningApp.UnitTest.Application.Notifications;

public class MarkNotificationReadCommandHandlerTests
{
    [Fact]
    public async Task Handle_ExistingNotification_ShouldMarkAsRead()
    {
        var repo = new Mock<INotificationRepository>();
        var notification = new NotificationEntity
        {
            UserId = "user",
            Type = NotificationType.QuizAssigned,
            Title = "Title",
            Message = "Message",
            Data = null
        };
        repo.Setup(r => r.GetByIdAsync(notification.Id)).ReturnsAsync(notification);

        var handler = new MarkNotificationReadCommandHandler(repo.Object);
        var command = new MarkNotificationReadCommand(notification.Id);

        await handler.Handle(command, CancellationToken.None);

        notification.IsRead.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_NonExistingNotification_ShouldThrowException()
    {
        var repo = new Mock<INotificationRepository>();
        var id = Guid.NewGuid();
        repo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((NotificationEntity?)null);

        var handler = new MarkNotificationReadCommandHandler(repo.Object);
        var command = new MarkNotificationReadCommand(id);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }
}
