# Task 8.1: Entity — Notification

## Description

Create the Notification entity to store user notifications.

## Priority
🔴 Critical — Foundation for notification system

## Dependencies
- None (independent)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Domain/Entities/Notification.cs` | Create |

## Steps

### Step 1: Create Notification entity
1. Inherit from `BaseEntity`
2. Properties:
   - `UserId` (string, required, max 200)
   - `Type` (NotificationType enum)
   - `Title` (string, required, max 200)
   - `Message` (string, required, max 1000)
   - `IsRead` (bool, default false)
   - `Data` (string?, nullable, max 2000 — JSON payload)
3. Factory method `Create(userId, type, title, message, data)`

## Expected Code

```csharp
using EnglishLearning.Domain.Common;
using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Domain.Entities;

public class Notification : BaseEntity
{
    public string UserId { get; set; } = string.Empty;
    public NotificationType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    public string? Data { get; set; }

    public static Notification Create(string userId, NotificationType type, string title, string message, string? data = null)
    {
        return new Notification
        {
            UserId = userId,
            Type = type,
            Title = title,
            Message = message,
            Data = data
        };
    }
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Domain` — 0 errors
- [ ] Notification entity has all properties
- [ ] Factory method exists

## Acceptance Criteria

- [ ] `Notification` inherits from `BaseEntity`
- [ ] Properties: UserId, Type, Title, Message, IsRead, Data
- [ ] Factory method `Create()` initializes IsRead as false
- [ ] Domain project builds successfully
