using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Interfaces;
using MediatR;

namespace EnglishLearning.Application.Features.Leaderboard.Queries.GetLeaderboard;

public class GetLeaderboardQueryHandler(
    ILeaderboardRepository _leaderboardRepository,
    IUserRepository _userRepository) : IRequestHandler<GetLeaderboardQuery, List<LeaderboardDto>>
{
    public async Task<List<LeaderboardDto>> Handle(GetLeaderboardQuery request, CancellationToken cancellationToken)
    {
        var topUsers = await _leaderboardRepository.GetTopUsersAsync(request.Count);

        var dtos = new List<LeaderboardDto>();
        for (int i = 0; i < topUsers.Count; i++)
        {
            var l = topUsers[i];
            Guid userId;
            var user = Guid.TryParse(l.UserId, out userId)
                ? await _userRepository.GetByIdAsync(userId)
                : null;

            dtos.Add(new LeaderboardDto
            {
                Id = l.Id,
                UserId = l.UserId,
                Username = user?.Username ?? l.UserId,
                TotalScore = l.TotalScore,
                QuizzesCompleted = l.QuizzesCompleted,
                AverageScore = l.AverageScore,
                Streak = l.Streak,
                Rank = i + 1
            });
        }

        return dtos;
    }
}
