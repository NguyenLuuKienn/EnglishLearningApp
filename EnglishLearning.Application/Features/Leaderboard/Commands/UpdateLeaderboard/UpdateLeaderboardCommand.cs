using MediatR;

namespace EnglishLearning.Application.Features.Leaderboard.Commands.UpdateLeaderboard;

public record UpdateLeaderboardCommand(
    string UserId,
    decimal Score) : IRequest<Guid>;
