using EnglishLearning.Application.Interfaces;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Infrastructure.Services;

public class CheckQuizAssignmentsJob(
    IQuizAssignmentRepository _assignmentRepository,
    INotificationService _notificationService,
    IUserRepository _userRepository) : ICheckQuizAssignmentsJob
{
    public async Task CheckAssignments()
    {
        var now = DateTime.UtcNow;
        var allAssignments = await _assignmentRepository.GetAllAsync();

        foreach (var assignment in allAssignments)
        {
            if (assignment.Status == AssignmentStatus.Cancelled) continue;

            // Quiz starting soon (within 1 hour)
            if (assignment.StartTime > now && assignment.StartTime <= now.AddHours(1) &&
                assignment.Status == AssignmentStatus.Scheduled)
            {
                await SendNotificationsForAssignment(assignment,
                    NotificationType.QuizStartingSoon,
                    "Quiz Starting Soon",
                    $"Quiz starts in 1 hour.");
            }

            // Quiz just started
            if (assignment.StartTime <= now && assignment.EndTime > now &&
                assignment.Status == AssignmentStatus.Scheduled)
            {
                assignment.Status = AssignmentStatus.Active;
                _assignmentRepository.Update(assignment);
                await _assignmentRepository.SaveChangesAsync();

                await SendNotificationsForAssignment(assignment,
                    NotificationType.QuizStarted,
                    "Quiz Started",
                    "Quiz has started. Good luck!");
            }

            // Quiz just ended
            if (assignment.EndTime <= now && assignment.Status == AssignmentStatus.Active)
            {
                assignment.Status = AssignmentStatus.Completed;
                _assignmentRepository.Update(assignment);
                await _assignmentRepository.SaveChangesAsync();

                await SendNotificationsForAssignment(assignment,
                    NotificationType.QuizEnded,
                    "Quiz Ended",
                    "Quiz has ended.");
            }
        }
    }

    private async Task SendNotificationsForAssignment(
        QuizAssignment assignment,
        NotificationType type, string title, string message)
    {
        if (assignment.TargetUserId != null)
        {
            await _notificationService.SendToUserAsync(assignment.TargetUserId, type, title, message);
        }
        else if (assignment.TargetRole != null)
        {
            await _notificationService.SendToRoleAsync(assignment.TargetRole.Value, type, title, message);
        }
    }
}
