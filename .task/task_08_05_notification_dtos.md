# Task 8.5: DTOs — NotificationDto

## Description

Create DTOs for Notification.

## Priority
🔴 Critical — Required for CQRS

## Dependencies
- Task 8.1 (Notification entity)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/DTOs/NotificationDto.cs` | Create |

## Steps

### Step 1: Create NotificationDto
1. Properties: Id, UserId, Type, Title, Message, IsRead, Data, CreatedAt

## Expected Code

```csharp
using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Application.DTOs;

public class NotificationDto
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public NotificationType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public string? Data { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Application` — 0 errors
- [ ] NotificationDto has all required properties

## Acceptance Criteria

- [ ] `NotificationDto` with Id, UserId, Type, Title, Message, IsRead, Data, CreatedAt
- [ ] In `EnglishLearning.Application.DTOs` namespace
- [ ] Application project builds successfully
