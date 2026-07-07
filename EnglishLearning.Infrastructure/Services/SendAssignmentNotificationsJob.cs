using EnglishLearning.Application.Interfaces;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Infrastructure.Services;

public class SendAssignmentNotificationsJob(
    IQuizAssignmentRepository _assignmentRepository,
    INotificationService _notificationService) : ISendAssignmentNotificationsJob
{
    public async Task SendNotifications(Guid assignmentId)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(assignmentId);
        if (assignment == null) return;

        var message = $"You have been assigned a new quiz. Starts: {assignment.StartTime:yyyy-MM-dd HH:mm}";

        if (assignment.TargetUserId != null)
        {
            await _notificationService.SendToUserAsync(
                assignment.TargetUserId,
                NotificationType.QuizAssigned,
                "New Quiz Assigned",
                message);
        }
        else if (assignment.TargetRole != null)
        {
            await _notificationService.SendToRoleAsync(
                assignment.TargetRole.Value,
                NotificationType.QuizAssigned,
                "New Quiz Assigned",
                message);
        }
    }
}
