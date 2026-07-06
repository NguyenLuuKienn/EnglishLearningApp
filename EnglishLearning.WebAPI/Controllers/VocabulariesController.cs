using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Vocabulary.Commands.CreateVocabulary;
using EnglishLearning.Application.Features.Vocabulary.Commands.DeleteVocabulary;
using EnglishLearning.Application.Features.Vocabulary.Commands.UpdateVocabulary;
using EnglishLearning.Application.Features.Vocabulary.Queries.GetVocabulary;
using EnglishLearning.Application.Features.Vocabulary.Queries.GetVocabularies;
using EnglishLearning.Domain.Enums;
using EnglishLearning.WebAPI.Models.Common;
using EnglishLearning.WebAPI.Models.Requests.Vocabulary;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VocabulariesController : ControllerBase
{
    private readonly IMediator _mediator;

    public VocabulariesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVocabularyRequest request)
    {
        var command = new CreateVocabularyCommand(
            request.Word, request.Definition, request.Example,
            request.PartOfSpeech, request.Difficulty);

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(ApiResponse<Guid>.BadRequest(
                result.Errors?.ToList() ?? [result.Error ?? string.Empty]));

        return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] DifficultyLevel? difficulty = null)
    {
        var query = new GetVocabulariesQuery(pageNumber, pageSize, difficulty);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return BadRequest(ApiResponse<PagedResult<VocabularyDto>>.BadRequest(
                result.Errors?.ToList() ?? [result.Error ?? string.Empty]));

        var paged = result.Value!;
        return Ok(PagedResponse<VocabularyDto>.Ok(
            paged.Items, paged.PageNumber, paged.PageSize, paged.TotalRecords));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetVocabularyQuery(id);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return NotFound(ApiResponse<VocabularyDto>.NotFound(result.Error ?? string.Empty));

        return Ok(ApiResponse<VocabularyDto>.Ok(result.Value!));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateVocabularyRequest request)
    {
        var command = new UpdateVocabularyCommand(
            id, request.Word, request.Definition, request.Example,
            request.PartOfSpeech, request.Difficulty);

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            if (result.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
                return NotFound(ApiResponse<Guid>.NotFound(result.Error ?? string.Empty));

            return BadRequest(ApiResponse<Guid>.BadRequest(
                result.Errors?.ToList() ?? [result.Error ?? string.Empty]));
        }

        return Ok(ApiResponse<Guid>.Ok(result.Value!, "Updated successfully"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteVocabularyCommand(id);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return NotFound(ApiResponse<string>.NotFound(result.Error ?? string.Empty));

        return NoContent();
    }
}
