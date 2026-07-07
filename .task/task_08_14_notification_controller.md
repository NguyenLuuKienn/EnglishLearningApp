# Task 8.14: NotificationController

## Description

Create NotificationController with endpoints for user notifications.

## Priority
🔴 Critical — HTTP entry points for notifications

## Dependencies
- Task 8.6-8.7 (CQRS commands/queries)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.WebAPI/Controllers/NotificationsController.cs` | Create |

## Steps

### Step 1: Create NotificationsController
1. `[ApiController]`, `[Route("api/[controller]")]`
2. Inject `IMediator`
3. Endpoints:
   - `GET /user/{userId}` — GetUserNotificationsQuery (paged, filter by read status)
   - `PATCH /{id}/read` — MarkNotificationReadCommand

## Expected Code

```csharp
using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Notifications.Commands.MarkNotificationRead;
using EnglishLearning.Application.Features.Notifications.Queries.GetUserNotifications;
using EnglishLearning.WebAPI.Models.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotificationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserNotifications(
        string userId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] bool? isRead = null)
    {
        var query = new GetUserNotificationsQuery(userId, pageNumber, pageSize, isRead);
        var paged = await _mediator.Send(query);

        return Ok(PagedResponse<NotificationDto>.Ok(
            paged.Items, paged.PageNumber, paged.PageSize, paged.TotalRecords));
    }

    [HttpPatch("{id}/read")]
    public async Task<IActionResult> MarkAsRead(Guid id)
    {
        var command = new MarkNotificationReadCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.WebAPI` — 0 errors
- [ ] All endpoints require authorization
- [ ] GET returns paged notifications

## Acceptance Criteria

- [ ] `NotificationsController` with `[ApiController]`, `[Route("api/[controller]")]`
- [ ] `GET /user/{userId}` — GetUserNotifications with pagination and read filter
- [ ] `PATCH /{id}/read` — MarkNotificationRead
- [ ] All endpoints require `[Authorize]`
- [ ] Responses wrapped in `ApiResponse<T>` or `PagedResponse<T>`
- [ ] WebAPI project builds successfully
