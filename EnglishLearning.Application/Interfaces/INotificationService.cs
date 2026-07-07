using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Application.Interfaces;

public interface INotificationService
{
    Task SendToUserAsync(string userId, NotificationType type, string title, string message, string? data = null);
    Task SendToRoleAsync(UserRole role, NotificationType type, string title, string message, string? data = null);
}
