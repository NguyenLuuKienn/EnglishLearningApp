# Task 2.4: Create CQRS — Vocabulary Features

## Description

Implement CQRS commands, queries, handlers, and validators for Vocabulary feature using MediatR pattern.

## Priority
🔴 Critical — Core CRUD feature

## Dependencies
- Task 2.1 (Application dependencies)
- Task 2.2 (Common classes)
- Task 2.3 (DTOs)
- Task 1.8 (Domain interfaces)

## Files to Create

| File | Action |
|------|--------|
| `Features/Vocabulary/Commands/CreateVocabulary/CreateVocabularyCommand.cs` | Create |
| `Features/Vocabulary/Commands/CreateVocabulary/CreateVocabularyCommandHandler.cs` | Create |
| `Features/Vocabulary/Commands/UpdateVocabulary/UpdateVocabularyCommand.cs` | Create |
| `Features/Vocabulary/Commands/UpdateVocabulary/UpdateVocabularyCommandHandler.cs` | Create |
| `Features/Vocabulary/Commands/DeleteVocabulary/DeleteVocabularyCommand.cs` | Create |
| `Features/Vocabulary/Commands/DeleteVocabulary/DeleteVocabularyCommandHandler.cs` | Create |
| `Features/Vocabulary/Queries/GetVocabulary/GetVocabularyQuery.cs` | Create |
| `Features/Vocabulary/Queries/GetVocabulary/GetVocabularyQueryHandler.cs` | Create |
| `Features/Vocabulary/Queries/GetVocabularies/GetVocabulariesQuery.cs` | Create |
| `Features/Vocabulary/Queries/GetVocabularies/GetVocabulariesQueryHandler.cs` | Create |
| `Features/Vocabulary/Validators/CreateVocabularyCommandValidator.cs` | Create |
| `Features/Vocabulary/Validators/UpdateVocabularyCommandValidator.cs` | Create |

## Steps

### Step 1: Create Commands
1. `CreateVocabularyCommand` — properties: Word, Definition, Example, PartOfSpeech, Difficulty. Implements `ISend<Result<Guid>>`
2. `UpdateVocabularyCommand` — properties: Id, Word, Definition, Example, PartOfSpeech, Difficulty. Implements `ISend<Result<Guid>>`
3. `DeleteVocabularyCommand` — properties: Id. Implements `ISend<Result>`

### Step 2: Create Command Handlers
1. `CreateVocabularyCommandHandler` — inject `IVocabularyRepository`, create entity, call AddAsync, return Id
2. `UpdateVocabularyCommandHandler` — inject `IVocabularyRepository`, GetByExpressionAsync, call Update method, return Id
3. `DeleteVocabularyCommandHandler` — inject `IVocabularyRepository`, GetByExpressionAsync, call Delete, return Result

### Step 3: Create Queries
1. `GetVocabularyQuery` — properties: Id. Implements `ISend<Result<VocabularyDto>>`
2. `GetVocabulariesQuery` — properties: PageNumber, PageSize, Difficulty (optional). Implements `ISend<Result<PagedResult<VocabularyDto>>>`

### Step 4: Create Query Handlers
1. `GetVocabularyQueryHandler` — inject `IVocabularyRepository`, GetByIdAsync, map to DTO
2. `GetVocabulariesQueryHandler` — inject `IVocabularyRepository`, filter by difficulty if provided, return paged result

### Step 5: Create Validators
1. `CreateVocabularyCommandValidator` — Word required max 200, Definition required max 1000, Example max 1000, PartOfSpeech max 50
2. `UpdateVocabularyCommandValidator` — Id required, same rules as Create

## Expected Code Pattern

```csharp
// Command
namespace EnglishLearning.Application.Features.Vocabulary.Commands.CreateVocabulary;

public record CreateVocabularyCommand(
    string Word,
    string Definition,
    string? Example,
    string? PartOfSpeech,
    Domain.Enums.DifficultyLevel Difficulty
) : IRequest<Common.Result<Guid>>;

// Handler
namespace EnglishLearning.Application.Features.Vocabulary.Commands.CreateVocabulary;

public class CreateVocabularyCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateVocabularyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Common.Result<Guid>> Handle(CreateVocabularyCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Vocabulary
        {
            Word = request.Word,
            Definition = request.Definition,
            Example = request.Example,
            PartOfSpeech = request.PartOfSpeech,
            Difficulty = request.Difficulty
        };

        await _unitOfWork.Vocabularies.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Common.Result<Guid>.Success(entity.Id);
    }
}

// Validator
using FluentValidation;

namespace EnglishLearning.Application.Features.Vocabulary.Validators;

public class CreateVocabularyCommandValidator : AbstractValidator<Commands.CreateVocabulary.CreateVocabularyCommand>
{
    public CreateVocabularyCommandValidator()
    {
        RuleFor(x => x.Word).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Definition).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.Example).MaximumLength(1000);
        RuleFor(x => x.PartOfSpeech).MaximumLength(50);
    }
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Application` — 0 errors
- [ ] All commands use `record` types implementing `IRequest<Result<T>>`
- [ ] All handlers inject `IUnitOfWork` via constructor
- [ ] Validators are registered with FluentValidation
- [ ] Commands return `Result<Guid>` for create/update, `Result` for delete
- [ ] Queries return `Result<DTO>` for single, `Result<PagedResult<DTO>>` for list

## Acceptance Criteria

- [ ] CreateVocabularyCommand + Handler + Validator created
- [ ] UpdateVocabularyCommand + Handler + Validator created
- [ ] DeleteVocabularyCommand + Handler created
- [ ] GetVocabularyQuery + Handler created (single by Id)
- [ ] GetVocabulariesQuery + Handler created (paged, filter by difficulty)
- [ ] All handlers use IUnitOfWork for data access
- [ ] All handlers return Result<T> pattern
- [ ] Application project builds successfully
