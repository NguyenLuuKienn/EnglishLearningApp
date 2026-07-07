# Task 8.12: Background Job — SendAssignmentNotifications

## Description

Create background job to send notifications when new quiz assignments are created.

## Priority
🟡 High — Immediate notification on assignment

## Dependencies
- Task 8.4 (INotificationService)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/Interfaces/ISendAssignmentNotificationsJob.cs` | Create |
| `EnglishLearning.Infrastructure/Services/SendAssignmentNotificationsJob.cs` | Create |

## Steps

### Step 1: Create ISendAssignmentNotificationsJob interface
1. Method: `Task SendNotifications(Guid assignmentId)`

### Step 2: Create SendAssignmentNotificationsJob
1. Inject `IQuizAssignmentRepository`, `INotificationService`
2. Get assignment by ID
3. Send QuizAssigned notification to target user/role

## Expected Code

```csharp
// ISendAssignmentNotificationsJob.cs
namespace EnglishLearning.Application.Interfaces;

public interface ISendAssignmentNotificationsJob
{
    Task SendNotifications(Guid assignmentId);
}

// SendAssignmentNotificationsJob.cs
using EnglishLearning.Application.Interfaces;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Infrastructure.Services;

public class SendAssignmentNotificationsJob(
    IQuizAssignmentRepository _assignmentRepository, 
    INotificationService _notificationService) 
    : ISendAssignmentNotificationsJob
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
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Infrastructure` — 0 errors
- [ ] Job sends notification when called

## Acceptance Criteria

- [ ] `ISendAssignmentNotificationsJob` interface with `SendNotifications(Guid assignmentId)` method
- [ ] `SendAssignmentNotificationsJob` implements the interface
- [ ] Sends QuizAssigned notification to target user or role
- [ ] Infrastructure project builds successfully
