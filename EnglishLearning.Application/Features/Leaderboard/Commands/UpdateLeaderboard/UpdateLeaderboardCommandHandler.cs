using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using MediatR;
using LeaderboardEntity = EnglishLearning.Domain.Entities.Leaderboard;

namespace EnglishLearning.Application.Features.Leaderboard.Commands.UpdateLeaderboard;

public class UpdateLeaderboardCommandHandler(
    ILeaderboardRepository _leaderboardRepository,
    IUnitOfWork _unitOfWork) : IRequestHandler<UpdateLeaderboardCommand, Guid>
{
    public async Task<Guid> Handle(UpdateLeaderboardCommand request, CancellationToken cancellationToken)
    {
        var allLeaderboards = await _leaderboardRepository.GetAllAsync();
        var leaderboard = allLeaderboards.FirstOrDefault(l => l.UserId == request.UserId);

        if (leaderboard == null)
        {
            leaderboard = new LeaderboardEntity
            {
                UserId = request.UserId,
                TotalScore = 0m,
                QuizzesCompleted = 0,
                AverageScore = 0m,
                Streak = 0,
                LastActiveDate = DateTime.UtcNow
            };
            await _leaderboardRepository.AddAsync(leaderboard);
        }

        leaderboard.QuizzesCompleted++;
        leaderboard.TotalScore += request.Score;
        leaderboard.AverageScore = leaderboard.TotalScore / leaderboard.QuizzesCompleted;

        var today = DateTime.UtcNow.Date;
        var lastActive = leaderboard.LastActiveDate.Date;
        if ((today - lastActive).Days == 1)
            leaderboard.Streak++;
        else if ((today - lastActive).Days > 1)
            leaderboard.Streak = 1;

        leaderboard.LastActiveDate = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return leaderboard.Id;
    }
}
