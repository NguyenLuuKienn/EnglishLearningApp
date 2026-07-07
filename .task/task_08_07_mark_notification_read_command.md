# Task 8.7: Command — MarkNotificationRead

## Description

Create MarkNotificationReadCommand and handler to mark a notification as read.

## Priority
🟡 High — Notification management

## Dependencies
- None (independent)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/Features/Notifications/Commands/MarkNotificationRead/MarkNotificationReadCommand.cs` | Create |
| `EnglishLearning.Application/Features/Notifications/Commands/MarkNotificationRead/MarkNotificationReadCommandHandler.cs` | Create |

## Steps

### Step 1: Create MarkNotificationReadCommand
1. Properties: NotificationId
2. Inherits from `IRequest`

### Step 2: Create MarkNotificationReadCommandHandler
1. Inject `INotificationRepository`
2. Find notification, throw if not found
3. Set IsRead = true
4. Save

## Expected Code

```csharp
// MarkNotificationReadCommand.cs
using MediatR;

namespace EnglishLearning.Application.Features.Notifications.Commands.MarkNotificationRead;

public record MarkNotificationReadCommand(Guid NotificationId) : IRequest;

// MarkNotificationReadCommandHandler.cs
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Application.Features.Notifications.Commands.MarkNotificationRead;

public class MarkNotificationReadCommandHandler(INotificationRepository _notificationRepository) 
    : IRequestHandler<MarkNotificationReadCommand>
{
    public async Task Handle(MarkNotificationReadCommand request, CancellationToken cancellationToken)
    {
        var notification = await _notificationRepository.GetByIdAsync(request.NotificationId);
        if (notification == null)
            throw new KeyNotFoundException("Notification not found.");

        notification.IsRead = true;
        _notificationRepository.Update(notification);
        await _notificationRepository.SaveChangesAsync(cancellationToken);
    }
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Application` — 0 errors
- [ ] Handler validates notification exists

## Acceptance Criteria

- [ ] `MarkNotificationReadCommand` with NotificationId
- [ ] `MarkNotificationReadCommandHandler` finds notification by Id
- [ ] Throws KeyNotFoundException if not found
- [ ] Sets IsRead to true
- [ ] Application project builds successfully
