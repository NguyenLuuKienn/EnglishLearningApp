# Task 4.4: Create Controllers

## Description

Create API controllers for Vocabulary, Quiz, and QuizResult features. Each controller uses MediatR to dispatch CQRS commands and queries.

## Priority
🔴 Critical — HTTP entry points for the application

## Dependencies
- Task 4.1 (WebAPI dependencies)
- Task 4.2 (API Response models)
- Task 4.3 (Request contracts)
- Task 2.4 (Vocabulary CQRS)
- Task 2.5 (Quiz CQRS)
- Task 2.6 (QuizResult CQRS)

## Files to Create

| File | Action |
|------|--------|
| `Controllers/VocabulariesController.cs` | Create |
| `Controllers/QuizzesController.cs` | Create |
| `Controllers/QuizResultsController.cs` | Create |

## Steps

### Step 1: Create VocabulariesController
1. `[ApiController]`, `[Route("api/[controller]")]`
2. Inject `IMediator` via constructor
3. Endpoints:
   - `POST /` — CreateVocabularyCommand → return Created
   - `GET /` — GetVocabulariesQuery (paged, filter by difficulty) → return Ok
   - `GET /{id}` — GetVocabularyQuery → return Ok or NotFound
   - `PUT /{id}` — UpdateVocabularyCommand → return Ok or NotFound
   - `DELETE /{id}` — DeleteVocabularyCommand → return NoContent or NotFound

### Step 2: Create QuizzesController
1. `[ApiController]`, `[Route("api/[controller]")]`
2. Inject `IMediator` via constructor
3. Endpoints:
   - `POST /` — CreateQuizCommand → return Created
   - `GET /` — GetQuizzesQuery (paged, filter by difficulty) → return Ok
   - `GET /{id}` — GetQuizQuery → return Ok or NotFound
   - `PUT /{id}` — UpdateQuizCommand → return Ok or NotFound
   - `DELETE /{id}` — DeleteQuizCommand → return NoContent or NotFound

### Step 3: Create QuizResultsController
1. `[ApiController]`, `[Route("api/[controller]")]`
2. Inject `IMediator` via constructor
3. Endpoints:
   - `POST /submit` — SubmitQuizResultCommand → return Ok
   - `GET /{id}` — GetQuizResultQuery → return Ok or NotFound
   - `GET /user/{userId}` — GetUserQuizResultsQuery (paged) → return Ok

### Step 4: Map requests to commands/queries
1. Map `CreateVocabularyRequest` → `CreateVocabularyCommand`
2. Map `CreateQuizRequest` → `CreateQuizCommand` (with nested Questions/Choices)
3. Map `SubmitQuizResultRequest` → `SubmitQuizResultCommand`

## Expected Code Pattern

```csharp
// VocabulariesController.cs
using MediatR;
using Microsoft.AspNetCore.Mvc;
using EnglishLearning.Application.Common;
using EnglishLearning.Application.Features.Vocabulary.Commands.CreateVocabulary;
using EnglishLearning.Application.Features.Vocabulary.Commands.DeleteVocabulary;
using EnglishLearning.Application.Features.Vocabulary.Commands.UpdateVocabulary;
using EnglishLearning.Application.Features.Vocabulary.Queries.GetVocabulary;
using EnglishLearning.Application.Features.Vocabulary.Queries.GetVocabularies;
using EnglishLearning.WebAPI.Contracts.Requests;
using EnglishLearning.WebAPI.Extensions;

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
                result.Errors?.ToList() ?? new List<string> { result.Error }));

        return CreatedAtAction(nameof(Get), new { id = result.Value }, result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Domain.Enums.DifficultyLevel? difficulty = null)
    {
        var query = new GetVocabulariesQuery(pageNumber, pageSize, difficulty);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return BadRequest(ApiResponse<Application.Common.PagedResult<DTOs.VocabularyDto>>.BadRequest(
                result.Errors?.ToList() ?? new List<string> { result.Error }));

        var paged = result.Value!;
        return Ok(PagedResponse<DTOs.VocabularyDto>.Ok(
            paged.Items, paged.PageNumber, paged.PageSize, paged.TotalRecords));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetVocabularyQuery(id);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return NotFound(ApiResponse<DTOs.VocabularyDto>.NotFound(result.Error));

        return Ok(ApiResponse<DTOs.VocabularyDto>.Ok(result.Value!));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateVocabularyRequest request)
    {
        var command = new UpdateVocabularyCommand(
            id, request.Word, request.Definition, request.Example,
            request.PartOfSpeech, request.Difficulty);

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            if (result.Error?.Contains("not found") == true)
                return NotFound(ApiResponse<Guid>.NotFound(result.Error));

            return BadRequest(ApiResponse<Guid>.BadRequest(
                result.Errors?.ToList() ?? new List<string> { result.Error }));
        }

        return Ok(ApiResponse<Guid>.Ok(result.Value!, "Updated successfully"));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteVocabularyCommand(id);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return NotFound(ApiResponse<string>.NotFound(result.Error));

        return NoContent();
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.WebAPI` — 0 errors ✅
- [x] All controllers use `[ApiController]` and `[Route("api/[controller]")]` ✅
- [x] All controllers inject `IMediator` ✅
- [x] All endpoints return `ApiResponse<T>` wrapped responses ✅
- [x] Proper HTTP status codes: 200 OK, 201 Created, 204 NoContent, 400 BadRequest, 404 NotFound ✅

## Acceptance Criteria

- [x] `VocabulariesController` with POST, GET (list), GET (by id), PUT, DELETE endpoints ✅
- [x] `QuizzesController` with POST, GET (list), GET (by id), PUT, DELETE endpoints ✅
- [x] `QuizResultsController` with POST (submit), GET (by id), GET (by user) endpoints ✅
- [x] All controllers inject IMediator ✅
- [x] Request contracts are mapped to CQRS commands/queries ✅
- [x] Results are wrapped in ApiResponse<T> or PagedResponse<T> ✅
- [x] Proper HTTP status codes returned ✅
- [x] WebAPI project builds successfully ✅

---

## ✅ Completed: 2026-07-06

- **VocabulariesController** — 5 endpoints: POST (Create), GET (GetAll paged + filter by difficulty), GET (GetById), PUT (Update), DELETE (Delete)
  - Map `CreateVocabularyRequest` → `CreateVocabularyCommand`
  - Map `UpdateVocabularyRequest` → `UpdateVocabularyCommand`
  - GET (list) trả về `PagedResponse<VocabularyDto>`, GET (by id) trả về `ApiResponse<VocabularyDto>`
- **QuizzesController** — 5 endpoints: POST (Create), GET (GetAll paged + filter by difficulty), GET (GetById), PUT (Update), DELETE (Delete)
  - Map `CreateQuizRequest` → `CreateQuizCommand` (kèm nested Questions/Choices qua `QuestionCommand`, `ChoiceCommand`)
  - Map `UpdateQuizRequest` → `UpdateQuizCommand`
  - GET (list) trả về `PagedResponse<QuizDto>`, GET (by id) trả về `ApiResponse<QuizDto>`
- **QuizResultsController** — 3 endpoints: POST (submit), GET (GetById), GET (GetByUserId paged)
  - Map `SubmitQuizResultRequest` → `SubmitQuizResultCommand` (kèm nested Answers qua `AnswerCommand`)
  - POST submit trả về `ApiResponse<QuizResultDto>`, GET (by user) trả về `PagedResponse<QuizResultDto>`
- **Pattern chung:**
  - Tất cả controllers inject `IMediator` qua constructor
  - `[ApiController]`, `[Route("api/[controller]")]`
  - Kiểm tra `result.IsSuccess` → trả về `BadRequest` hoặc `NotFound` tương ứng
  - Success → `Ok()`, `CreatedAtAction()`, `NoContent()` theo HTTP semantics
  - Error handling: `result.Errors?.ToList() ?? [result.Error ?? string.Empty]`
  - NotFound detection: `result.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true`
- Build verified: 0 errors
