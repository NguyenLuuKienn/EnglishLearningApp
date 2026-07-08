using EnglishLearning.Application.Interfaces;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Infrastructure.Services;

namespace EnglishLearnningApp.UnitTest.Infrastructure.Services;

public class NotificationServiceTests
{
    [Fact]
    public async Task SendToUserAsync_ShouldCreateAndSaveNotification()
    {
        var notificationRepo = new Mock<INotificationRepository>();
        var userRepo = new Mock<IUserRepository>();

        var service = new NotificationService(notificationRepo.Object, userRepo.Object);

        await service.SendToUserAsync("user-123", NotificationType.QuizAssigned, "New Quiz", "You have a quiz");

        notificationRepo.Verify(r => r.AddAsync(It.Is<Notification>(n =>
            n.UserId == "user-123" &&
            n.Type == NotificationType.QuizAssigned &&
            n.Title == "New Quiz" &&
            n.Message == "You have a quiz"
        )), Times.Once);
        notificationRepo.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SendToUserAsync_WithData_ShouldIncludeData()
    {
        var notificationRepo = new Mock<INotificationRepository>();
        var userRepo = new Mock<IUserRepository>();

        var service = new NotificationService(notificationRepo.Object, userRepo.Object);

        await service.SendToUserAsync("user-123", NotificationType.QuizAssigned, "Title", "Message", "{\"quizId\": \"123\"}");

        notificationRepo.Verify(r => r.AddAsync(It.Is<Notification>(n =>
            n.Data == "{\"quizId\": \"123\"}"
        )), Times.Once);
    }

    [Fact]
    public async Task SendToRoleAsync_ShouldSendToAllUsersWithRole()
    {
        var notificationRepo = new Mock<INotificationRepository>();
        var userRepo = new Mock<IUserRepository>();

        var users = new List<User>
        {
            new User { Username = "student1", Email = "s1@test.com", PasswordHash = "hash", Role = UserRole.Student, IsActive = true },
            new User { Username = "student2", Email = "s2@test.com", PasswordHash = "hash", Role = UserRole.Student, IsActive = true },
            new User { Username = "teacher1", Email = "t1@test.com", PasswordHash = "hash", Role = UserRole.Teacher, IsActive = true }
        };

        userRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(users);

        var service = new NotificationService(notificationRepo.Object, userRepo.Object);

        await service.SendToRoleAsync(UserRole.Student, NotificationType.QuizAssigned, "Title", "Message");

        notificationRepo.Verify(r => r.AddAsync(It.Is<Notification>(n =>
            (n.UserId == users[0].Id.ToString() || n.UserId == users[1].Id.ToString()) &&
            n.Type == NotificationType.QuizAssigned
        )), Times.Exactly(2));
        notificationRepo.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SendToRoleAsync_WithNoMatchingUsers_ShouldNotCreateNotifications()
    {
        var notificationRepo = new Mock<INotificationRepository>();
        var userRepo = new Mock<IUserRepository>();

        userRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<User>
        {
            new User { Username = "admin1", Email = "a@test.com", PasswordHash = "hash", Role = UserRole.Admin, IsActive = true }
        });

        var service = new NotificationService(notificationRepo.Object, userRepo.Object);

        await service.SendToRoleAsync(UserRole.Student, NotificationType.QuizAssigned, "Title", "Message");

        notificationRepo.Verify(r => r.AddAsync(It.IsAny<Notification>()), Times.Never);
    }
}
