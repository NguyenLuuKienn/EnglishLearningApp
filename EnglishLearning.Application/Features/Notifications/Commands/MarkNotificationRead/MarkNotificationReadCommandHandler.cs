using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Notifications.Commands.MarkNotificationRead;

public class MarkNotificationReadCommandHandler(
    INotificationRepository _notificationRepository) : IRequestHandler<MarkNotificationReadCommand>
{
    public async Task Handle(MarkNotificationReadCommand request, CancellationToken cancellationToken)
    {
        var notification = await _notificationRepository.GetByIdAsync(request.NotificationId);
        if (notification == null)
            throw new KeyNotFoundException(CommonErrorMessages.ResourceNotFound);

        notification.IsRead = true;
        _notificationRepository.Update(notification);
        await _notificationRepository.SaveChangesAsync(cancellationToken);
    }
}
