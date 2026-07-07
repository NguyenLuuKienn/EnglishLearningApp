# Task 8.9: NotificationService Implementation

## Description

Create NotificationService implementation that sends notifications to users.

## Priority
🔴 Critical — Notification sending logic

## Dependencies
- Task 8.4 (INotificationService), Task 8.8 (NotificationRepository)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Infrastructure/Services/NotificationService.cs` | Create |

## Steps

### Step 1: Create NotificationService
1. Implement `INotificationService`
2. Inject `INotificationRepository`, `IUserRepository`
3. `SendToUserAsync`: create notification for specific user
4. `SendToRoleAsync`: get all users with role, create notification for each

## Expected Code

```csharp
using EnglishLearning.Application.Interfaces;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Infrastructure.Services;

public class NotificationService(INotificationRepository _notificationRepository, IUserRepository _userRepository) 
    : INotificationService
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
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Infrastructure` — 0 errors
- [ ] NotificationService implements INotificationService
- [ ] SendToRoleAsync sends to all users with matching role

## Acceptance Criteria

- [ ] `NotificationService` implements `INotificationService`
- [ ] `SendToUserAsync` creates notification for specific user
- [ ] `SendToRoleAsync` gets all users with role and creates notifications
- [ ] Infrastructure project builds successfully
