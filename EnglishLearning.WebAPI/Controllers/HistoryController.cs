using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.History.Queries.GetUserHistory;
using EnglishLearning.WebAPI.Models.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class HistoryController(IMediator _mediator) : ControllerBase
{
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserHistory(
        string userId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetUserHistoryQuery(userId, pageNumber, pageSize);
        var paged = await _mediator.Send(query);

        return Ok(PagedResponse<LearningHistoryDto>.Ok(
            paged.Items, paged.PageNumber, paged.PageSize, paged.TotalRecords));
    }
}
