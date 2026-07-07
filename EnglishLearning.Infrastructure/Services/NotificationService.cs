using EnglishLearning.Application.Interfaces;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Infrastructure.Services;

public class NotificationService(
    INotificationRepository _notificationRepository,
    IUserRepository _userRepository) : INotificationService
{
    public async Task SendToUserAsync(string userId, NotificationType type, string title, string message, string? data = null)
    {
        var notification = Notification.Create(userId, type, title, message, data);
        await _notificationRepository.AddAsync(notification);
        await _notificationRepository.SaveChangesAsync();
    }

    public async Task SendToRoleAsync(UserRole role, NotificationType type, string title, string message, string? data = null)
    {
        var allUsers = await _userRepository.GetAllAsync();
        var usersWithRole = allUsers.Where(u => u.Role == role).ToList();

        foreach (var user in usersWithRole)
        {
            var notification = Notification.Create(user.Id.ToString(), type, title, message, data);
            await _notificationRepository.AddAsync(notification);
        }

        await _notificationRepository.SaveChangesAsync();
    }
}
