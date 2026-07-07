# Task 8.6: Query — GetUserNotifications

## Description

Create GetUserNotificationsQuery and handler to get user's notifications (paged, filter by read status).

## Priority
🔴 Critical — User sees their notifications

## Dependencies
- Task 8.5 (NotificationDto)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/Features/Notifications/Queries/GetUserNotifications/GetUserNotificationsQuery.cs` | Create |
| `EnglishLearning.Application/Features/Notifications/Queries/GetUserNotifications/GetUserNotificationsQueryHandler.cs` | Create |

## Steps

### Step 1: Create GetUserNotificationsQuery
1. Properties: UserId, PageNumber, PageSize, IsRead (nullable filter)
2. Inherits from `IRequest<PagedResult<NotificationDto>>`

### Step 2: Create GetUserNotificationsQueryHandler
1. Inject `INotificationRepository`, `IMapper`
2. Get notifications by userId with pagination
3. Filter by IsRead if specified
4. Map to DTOs

## Expected Code

```csharp
// GetUserNotificationsQuery.cs
using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using MediatR;

namespace EnglishLearning.Application.Features.Notifications.Queries.GetUserNotifications;

public record GetUserNotificationsQuery(
    string UserId,
    int PageNumber,
    int PageSize,
    bool? IsRead) : IRequest<PagedResult<NotificationDto>>;

// GetUserNotificationsQueryHandler.cs
using AutoMapper;
using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Application.Features.Notifications.Queries.GetUserNotifications;

public class GetUserNotificationsQueryHandler(
    INotificationRepository _notificationRepository, 
    IMapper _mapper) 
    : IRequestHandler<GetUserNotificationsQuery, PagedResult<NotificationDto>>
{
    public async Task<PagedResult<NotificationDto>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
    {
        var allNotifications = await _notificationRepository.GetAllAsync();
        
        var userNotifications = allNotifications
            .Where(n => n.UserId == request.UserId &&
                       (request.IsRead == null || n.IsRead == request.IsRead.Value))
            .OrderByDescending(n => n.CreatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        var totalRecords = allNotifications
            .Where(n => n.UserId == request.UserId &&
                       (request.IsRead == null || n.IsRead == request.IsRead.Value))
            .Count();

        var dtos = _mapper.Map<List<NotificationDto>>(userNotifications);

        return PagedResult<NotificationDto>.Create(dtos, request.PageNumber, request.PageSize, totalRecords);
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Application` — 0 errors ✅
- [x] Handler filters by userId and read status ✅

## Acceptance Criteria

- [x] `GetUserNotificationsQuery` with UserId, PageNumber, PageSize, IsRead filter ✅
- [x] `GetUserNotificationsQueryHandler` filters by userId ✅
- [x] Optional filter by IsRead status ✅
- [x] Returns `PagedResult<NotificationDto>` ✅
- [x] Application project builds successfully ✅
