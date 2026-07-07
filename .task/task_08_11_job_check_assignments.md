# Task 8.11: Background Job — CheckQuizAssignments

## Description

Create background job to check quiz assignments and send notifications for starting/ending quizzes.

## Priority
🔴 Critical — Automated notification system

## Dependencies
- Task 8.4 (INotificationService), Task 8.10 (Hangfire setup)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/Interfaces/ICheckQuizAssignmentsJob.cs` | Create |
| `EnglishLearning.Infrastructure/Services/CheckQuizAssignmentsJob.cs` | Create |

## Steps

### Step 1: Create ICheckQuizAssignmentsJob interface
1. Method: `Task CheckAssignments()`

### Step 2: Create CheckQuizAssignmentsJob
1. Inject `IQuizAssignmentRepository`, `INotificationService`, `IUserRepository`
2. Get all active assignments
3. Check for assignments starting soon (within 1 hour) → send QuizStartingSoon notification
4. Check for assignments that just started → send QuizStarted notification
5. Check for assignments that just ended → send QuizEnded notification

## Expected Code

```csharp
// ICheckQuizAssignmentsJob.cs
namespace EnglishLearning.Application.Interfaces;

public interface ICheckQuizAssignmentsJob
{
    Task CheckAssignments();
}

// CheckQuizAssignmentsJob.cs
using EnglishLearning.Application.Interfaces;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Infrastructure.Services;

public class CheckQuizAssignmentsJob(
    IQuizAssignmentRepository _assignmentRepository, 
    INotificationService _notificationService, 
    IUserRepository _userRepository) 
    : ICheckQuizAssignmentsJob
{
    public async Task CheckAssignments()
    {
        var now = DateTime.UtcNow;
        var allAssignments = await _assignmentRepository.GetAllAsync();
        var allUsers = await _userRepository.GetAllAsync();

        foreach (var assignment in allAssignments)
        {
            if (assignment.Status == AssignmentStatus.Cancelled) continue;

            // Quiz starting soon (within 1 hour)
            if (assignment.StartTime > now && assignment.StartTime <= now.AddHours(1) &&
                assignment.Status == AssignmentStatus.Scheduled)
            {
                await SendNotificationsForAssignment(assignment, allUsers, 
                    NotificationType.QuizStartingSoon, 
                    "Quiz Starting Soon", 
                    $"Quiz '{assignment.QuizId}' starts in 1 hour.");
            }

            // Quiz just started
            if (assignment.StartTime <= now && assignment.EndTime > now &&
                assignment.Status == AssignmentStatus.Scheduled)
            {
                assignment.Status = AssignmentStatus.Active;
                _assignmentRepository.Update(assignment);
                await _assignmentRepository.SaveChangesAsync();

                await SendNotificationsForAssignment(assignment, allUsers,
                    NotificationType.QuizStarted,
                    "Quiz Started",
                    $"Quiz '{assignment.QuizId}' has started. Good luck!");
            }

            // Quiz just ended
            if (assignment.EndTime <= now && assignment.Status == AssignmentStatus.Active)
            {
                assignment.Status = AssignmentStatus.Completed;
                _assignmentRepository.Update(assignment);
                await _assignmentRepository.SaveChangesAsync();

                await SendNotificationsForAssignment(assignment, allUsers,
                    NotificationType.QuizEnded,
                    "Quiz Ended",
                    $"Quiz '{assignment.QuizId}' has ended.");
            }
        }
    }

    private async Task SendNotificationsForAssignment(
        Domain.Entities.QuizAssignment assignment, 
        IEnumerable<Domain.Entities.User> allUsers,
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
```

## Verification

- [x] Run `dotnet build EnglishLearning.Infrastructure` — 0 errors ✅
- [x] Job checks assignments every 5 minutes ✅
- [x] Notifications sent for starting/started/ended quizzes ✅

## Acceptance Criteria

- [x] `ICheckQuizAssignmentsJob` interface with `CheckAssignments()` method ✅
- [x] `CheckQuizAssignmentsJob` implements the interface ✅
- [x] Checks for quizzes starting soon (within 1 hour) ✅
- [x] Updates assignment status from Scheduled → Active → Completed ✅
- [x] Sends appropriate notifications (QuizStartingSoon, QuizStarted, QuizEnded) ✅
- [x] Infrastructure project builds successfully ✅
