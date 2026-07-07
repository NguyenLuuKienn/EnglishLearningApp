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
public class NotificationsController(IMediator _mediator) : ControllerBase
{
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
