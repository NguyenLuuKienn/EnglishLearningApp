using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;

namespace EnglishLearnningApp.UnitTest.Domain.Entities;

public class NotificationTests
{
    [Fact]
    public void Create_ShouldSetAllProperties()
    {
        var notification = new Notification
        {
            UserId = "user-123",
            Type = NotificationType.QuizAssigned,
            Title = "New Quiz",
            Message = "You have a new quiz",
            Data = "{\"quizId\":\"123\"}"
        };

        notification.UserId.Should().Be("user-123");
        notification.Type.Should().Be(NotificationType.QuizAssigned);
        notification.Title.Should().Be("New Quiz");
        notification.Message.Should().Be("You have a new quiz");
        notification.Data.Should().Be("{\"quizId\":\"123\"}");
        notification.IsRead.Should().BeFalse();
    }

    [Fact]
    public void MarkAsRead_ShouldSetIsReadToTrue()
    {
        var notification = new Notification
        {
            UserId = "user",
            Type = NotificationType.QuizStarted,
            Title = "Title",
            Message = "Message",
            Data = null
        };
        notification.IsRead = true;
        notification.IsRead.Should().BeTrue();
    }
}
