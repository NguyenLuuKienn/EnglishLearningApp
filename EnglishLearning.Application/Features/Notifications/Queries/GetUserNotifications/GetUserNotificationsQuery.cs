using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using MediatR;

namespace EnglishLearning.Application.Features.Notifications.Queries.GetUserNotifications;

public record GetUserNotificationsQuery(
    string UserId,
    int PageNumber,
    int PageSize,
    bool? IsRead) : IRequest<PagedResult<NotificationDto>>;
