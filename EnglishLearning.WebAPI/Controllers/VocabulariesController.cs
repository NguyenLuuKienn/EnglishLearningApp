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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VocabulariesController(IMediator _mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVocabularyRequest request)
    {
        var command = new CreateVocabularyCommand(
            request.Word, request.Definition, request.Example,
            request.PartOfSpeech, request.Difficulty);

        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] DifficultyLevel? difficulty = null)
    {
        var query = new GetVocabulariesQuery(pageNumber, pageSize, difficulty);
        var paged = await _mediator.Send(query);

        return Ok(PagedResponse<VocabularyDto>.Ok(
            paged.Items, paged.PageNumber, paged.PageSize, paged.TotalRecords));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetVocabularyQuery(id);
        var dto = await _mediator.Send(query);

        return Ok(ApiResponse<VocabularyDto>.Ok(dto));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateVocabularyRequest request)
    {
        var command = new UpdateVocabularyCommand(
            id, request.Word, request.Definition, request.Example,
            request.PartOfSpeech, request.Difficulty);

        var updatedId = await _mediator.Send(command);
        return Ok(ApiResponse<Guid>.Ok(updatedId, "Updated successfully"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteVocabularyCommand(id);
        await _mediator.Send(command);

        return NoContent();
    }
}
