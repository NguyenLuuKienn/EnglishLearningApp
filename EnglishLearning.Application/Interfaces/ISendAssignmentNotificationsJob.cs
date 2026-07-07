namespace EnglishLearning.Application.Interfaces;

public interface ISendAssignmentNotificationsJob
{
    Task SendNotifications(Guid assignmentId);
}
