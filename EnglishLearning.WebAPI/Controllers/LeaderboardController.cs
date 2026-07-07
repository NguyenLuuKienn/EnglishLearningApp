using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Leaderboard.Queries.GetLeaderboard;
using EnglishLearning.Application.Features.Leaderboard.Queries.GetUserRank;
using EnglishLearning.WebAPI.Models.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaderboardController(IMediator _mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetLeaderboard([FromQuery] int count = 10)
    {
        var query = new GetLeaderboardQuery(count);
        var list = await _mediator.Send(query);
        return Ok(ApiResponse<List<LeaderboardDto>>.Ok(list));
    }

    [HttpGet("user/{userId}/rank")]
    public async Task<IActionResult> GetUserRank(string userId)
    {
        var query = new GetUserRankQuery(userId);
        var rank = await _mediator.Send(query);
        return Ok(ApiResponse<int>.Ok(rank));
    }
}
