using AutoMapper;
using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Notifications.Queries.GetUserNotifications;

public class GetUserNotificationsQueryHandler(
    INotificationRepository _notificationRepository,
    IMapper _mapper) : IRequestHandler<GetUserNotificationsQuery, PagedResult<NotificationDto>>
{
    public async Task<PagedResult<NotificationDto>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
    {
        var (items, totalRecords) = await _notificationRepository.GetByUserIdAsync(
            request.UserId, request.PageNumber, request.PageSize, request.IsRead);

        var dtos = _mapper.Map<List<NotificationDto>>(items);

        return PagedResult<NotificationDto>.Create(dtos, request.PageNumber, request.PageSize, totalRecords);
    }
}
