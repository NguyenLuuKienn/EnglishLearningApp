using EnglishLearning.Application.DTOs;
using MediatR;

namespace EnglishLearning.Application.Features.Leaderboard.Queries.GetLeaderboard;

public record GetLeaderboardQuery(int Count) : IRequest<List<LeaderboardDto>>;
