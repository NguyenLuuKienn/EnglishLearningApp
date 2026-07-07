using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.History.Commands.RecordHistory;

public class RecordHistoryCommandHandler(
    ILearningHistoryRepository _historyRepository,
    IUnitOfWork _unitOfWork) : IRequestHandler<RecordHistoryCommand, Guid>
{
    public async Task<Guid> Handle(RecordHistoryCommand request, CancellationToken cancellationToken)
    {
        var history = LearningHistory.Create(
            request.UserId, request.ActionType, request.TargetId, request.Details, request.Score);

        await _historyRepository.AddAsync(history);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return history.Id;
    }
}
