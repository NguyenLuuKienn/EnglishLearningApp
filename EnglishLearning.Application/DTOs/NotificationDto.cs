using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Application.DTOs;

public class NotificationDto
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public NotificationType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public string? Data { get; set; }
    public DateTime CreatedAt { get; set; }
}
