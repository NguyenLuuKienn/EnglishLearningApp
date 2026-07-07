using MediatR;

namespace EnglishLearning.Application.Features.Leaderboard.Queries.GetUserRank;

public record GetUserRankQuery(string UserId) : IRequest<int>;
