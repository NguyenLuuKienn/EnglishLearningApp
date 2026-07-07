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

- [x] Run `dotnet build EnglishLearning.Domain` — 0 errors ✅
- [x] Notification entity has all properties ✅
- [x] Factory method exists ✅

## Acceptance Criteria

- [x] `Notification` inherits from `BaseEntity` ✅
- [x] Properties: UserId, Type, Title, Message, IsRead, Data ✅
- [x] Factory method `Create()` initializes IsRead as false ✅
- [x] Domain project builds successfully ✅

---

## ✅ Completed: 2026-07-07

- **Notification** — UserId, Type (NotificationType), Title, Message, IsRead (default false), Data (nullable)
- Factory method `Create(userId, type, title, message, data)`
- Build verified: 0 errors ✅

---

## ✅ Completed: 2026-07-07

- **Notification** — UserId, Type (NotificationType), Title, Message, IsRead (default false), Data (nullable)
- Factory method `Create(userId, type, title, message, data)`
- Build verified: 0 errors
