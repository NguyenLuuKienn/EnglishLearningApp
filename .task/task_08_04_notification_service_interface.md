# Task 8.4: INotificationService Interface

## Description

Create the INotificationService interface for sending notifications.

## Priority
🔴 Critical — Service contract for notifications

## Dependencies
- Task 8.2 (NotificationType enum)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/Interfaces/INotificationService.cs` | Create |

## Steps

### Step 1: Create INotificationService interface
1. Methods:
   - `Task SendToUserAsync(string userId, NotificationType type, string title, string message, string? data)`
   - `Task SendToRoleAsync(UserRole role, NotificationType type, string title, string message, string? data)`

## Expected Code

```csharp
using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Application.Interfaces;

public interface INotificationService
{
    Task SendToUserAsync(string userId, NotificationType type, string title, string message, string? data = null);
    Task SendToRoleAsync(UserRole role, NotificationType type, string title, string message, string? data = null);
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Application` — 0 errors
- [ ] INotificationService interface has required methods

## Acceptance Criteria

- [ ] `INotificationService` interface in `EnglishLearning.Application.Interfaces` namespace
- [ ] `SendToUserAsync` sends notification to specific user
- [ ] `SendToRoleAsync` sends notification to all users with specific role
- [ ] Application project builds successfully
