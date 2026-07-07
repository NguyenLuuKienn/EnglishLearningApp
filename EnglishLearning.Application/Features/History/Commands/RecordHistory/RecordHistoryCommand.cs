using EnglishLearning.Domain.Enums;
using MediatR;

namespace EnglishLearning.Application.Features.History.Commands.RecordHistory;

public record RecordHistoryCommand(
    string UserId,
    ActionType ActionType,
    Guid TargetId,
    string? Details,
    decimal? Score) : IRequest<Guid>;
