using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Notifications.Queries.GetUserNotifications;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using AutoMapper;
using NotificationEntity = EnglishLearning.Domain.Entities.Notification;

namespace EnglishLearnningApp.UnitTest.Application.Notifications;

public class GetUserNotificationsQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnPagedNotifications()
    {
        var repo = new Mock<INotificationRepository>();
        var mapper = new Mock<IMapper>();

        var notifications = new List<NotificationEntity>
        {
            new NotificationEntity
            {
                UserId = "user",
                Type = NotificationType.QuizAssigned,
                Title = "Title",
                Message = "Message",
                Data = null
            }
        };
        var dtos = new List<NotificationDto> { new() };
        var paged = (Items: notifications, TotalRecords: 1);

        repo.Setup(r => r.GetByUserIdAsync("user", 1, 10, null)).ReturnsAsync(paged);
        mapper.Setup(m => m.Map<List<NotificationDto>>(notifications)).Returns(dtos);

        var handler = new GetUserNotificationsQueryHandler(repo.Object, mapper.Object);
        var query = new GetUserNotificationsQuery("user", 1, 10, null);

        var result = await handler.Handle(query, CancellationToken.None);
        result.Should().NotBeNull();
        result.Items.Should().HaveCount(1);
    }
}
