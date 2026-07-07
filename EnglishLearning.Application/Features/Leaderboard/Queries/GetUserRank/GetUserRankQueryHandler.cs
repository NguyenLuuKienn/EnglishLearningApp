using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Leaderboard.Queries.GetUserRank;

public class GetUserRankQueryHandler(
    ILeaderboardRepository _leaderboardRepository) : IRequestHandler<GetUserRankQuery, int>
{
    public async Task<int> Handle(GetUserRankQuery request, CancellationToken cancellationToken)
    {
        var all = await _leaderboardRepository.GetAllAsync();
        var userLeaderboard = all.FirstOrDefault(l => l.UserId == request.UserId);

        if (userLeaderboard == null)
            throw new KeyNotFoundException(LeaderboardErrorMessages.NotFound);

        var rank = all
            .OrderByDescending(l => l.TotalScore)
            .ToList()
            .FindIndex(l => l.UserId == request.UserId) + 1;

        return rank > 0 ? rank : throw new KeyNotFoundException(LeaderboardErrorMessages.NotFound);
    }
}
