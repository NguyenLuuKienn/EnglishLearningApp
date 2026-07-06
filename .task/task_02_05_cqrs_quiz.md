# Task 2.5: Create CQRS — Quiz Features

## Description

Implement CQRS commands, queries, handlers, and validators for Quiz feature. Includes creating quizzes with questions and choices.

## Priority
🔴 Critical — Core quiz management feature

## Dependencies
- Task 2.1 (Application dependencies)
- Task 2.2 (Common classes)
- Task 2.3 (DTOs)
- Task 1.8 (Domain interfaces)

## Files to Create

| File | Action |
|------|--------|
| `Features/Quizzes/Commands/CreateQuiz/CreateQuizCommand.cs` | Create |
| `Features/Quizzes/Commands/CreateQuiz/CreateQuizCommandHandler.cs` | Create |
| `Features/Quizzes/Commands/UpdateQuiz/UpdateQuizCommand.cs` | Create |
| `Features/Quizzes/Commands/UpdateQuiz/UpdateQuizCommandHandler.cs` | Create |
| `Features/Quizzes/Commands/DeleteQuiz/DeleteQuizCommand.cs` | Create |
| `Features/Quizzes/Commands/DeleteQuiz/DeleteQuizCommandHandler.cs` | Create |
| `Features/Quizzes/Queries/GetQuiz/GetQuizQuery.cs` | Create |
| `Features/Quizzes/Queries/GetQuiz/GetQuizQueryHandler.cs` | Create |
| `Features/Quizzes/Queries/GetQuizzes/GetQuizzesQuery.cs` | Create |
| `Features/Quizzes/Queries/GetQuizzes/GetQuizzesQueryHandler.cs` | Create |
| `Features/Quizzes/Validators/CreateQuizCommandValidator.cs` | Create |
| `Features/Quizzes/Validators/UpdateQuizCommandValidator.cs` | Create |

## Steps

### Step 1: Create Commands
1. `CreateQuizCommand` — properties: Title, Description, Difficulty, TimeLimitMinutes, PassingScore, Questions (list of QuestionCommand with Choices). Implements `ISend<Result<Guid>>`
2. `UpdateQuizCommand` — properties: Id + same as Create. Implements `ISend<Result<Guid>>`
3. `DeleteQuizCommand` — properties: Id. Implements `ISend<Result>`
4. Nested `QuestionCommand` record — QuestionText, QuestionType, Difficulty, CorrectAnswer, Choices (list of ChoiceCommand)
5. Nested `ChoiceCommand` record — ChoiceText, IsCorrect

### Step 2: Create Command Handlers
1. `CreateQuizCommandHandler` — inject `IUnitOfWork`, create Quiz entity, create Questions with Choices, add to repository, SaveChangesAsync
2. `UpdateQuizCommandHandler` — inject `IUnitOfWork`, get quiz, update properties, SaveChangesAsync
3. `DeleteQuizCommandHandler` — inject `IUnitOfWork`, get quiz, delete, SaveChangesAsync

### Step 3: Create Queries
1. `GetQuizQuery` — properties: Id. Implements `ISend<Result<QuizDto>>` — returns quiz with questions and choices
2. `GetQuizzesQuery` — properties: PageNumber, PageSize, Difficulty (optional). Implements `ISend<Result<PagedResult<QuizDto>>>`

### Step 4: Create Query Handlers
1. `GetQuizQueryHandler` — inject `IUnitOfWork`, use `GetQuizWithQuestionsAsync`, map to QuizDto with nested Questions and Choices
2. `GetQuizzesQueryHandler` — inject `IUnitOfWork`, filter by difficulty if provided, return paged result

### Step 5: Create Validators
1. `CreateQuizCommandValidator` — Title required max 200, Description max 1000, TimeLimitMinutes >= 0, PassingScore between 0-100, Questions must have at least 1 item
2. `UpdateQuizCommandValidator` — Id required, same rules as Create

## Expected Code Pattern

```csharp
// Command with nested questions
namespace EnglishLearning.Application.Features.Quizzes.Commands.CreateQuiz;

public record ChoiceCommand(
    string ChoiceText,
    bool IsCorrect
);

public record QuestionCommand(
    string QuestionText,
    Domain.Enums.QuestionType QuestionType,
    Domain.Enums.DifficultyLevel Difficulty,
    string? CorrectAnswer,
    List<ChoiceCommand> Choices
);

public record CreateQuizCommand(
    string Title,
    string? Description,
    Domain.Enums.DifficultyLevel Difficulty,
    int TimeLimitMinutes,
    decimal PassingScore,
    List<QuestionCommand> Questions
) : IRequest<Common.Result<Guid>>;
```

## Verification

- [x] Run `dotnet build EnglishLearning.Application` — 0 errors ✅
- [x] CreateQuizCommand supports nested Questions and Choices ✅
- [x] Handlers properly create entity graph (Quiz → Questions → Choices) ✅
- [x] GetQuizQuery returns quiz with all questions and choices loaded ✅

## Acceptance Criteria

- [x] CreateQuizCommand with nested QuestionCommand and ChoiceCommand records ✅
- [x] CreateQuizCommandHandler creates Quiz + Questions + Choices in one transaction ✅
- [x] UpdateQuizCommand + Handler created ✅
- [x] DeleteQuizCommand + Handler created ✅
- [x] GetQuizQuery + Handler returns quiz with questions and choices ✅
- [x] GetQuizzesQuery + Handler supports filtering by difficulty ✅
- [x] Validators for Create and Update commands ✅
- [x] Application project builds successfully ✅

---

## ✅ Completed: 2026-07-06

### Commands (3)
- `CreateQuizCommand` + Handler — tạo Quiz với Questions + Choices (nested records), trả về Id
- `UpdateQuizCommand` + Handler — tìm theo Id, update properties, update UpdatedAt, save
- `DeleteQuizCommand` + Handler — tìm theo Id, delete, save

### Queries (2)
- `GetQuizQuery` + Handler — dùng `GetQuizWithQuestionsAsync`, map nested QuizDto → Questions → Choices
- `GetQuizzesQuery` + Handler — paged + filter by difficulty, sort by CreatedAt desc

### Validators (2)
- `CreateQuizCommandValidator` — Title required, PassingScore 0-100, Questions không rỗng
- `UpdateQuizCommandValidator` — Id required, same rules as Create

### Notes
- Handlers inject `IUnitOfWork` qua constructor
- CreateQuizCommandHandler tạo entity graph: Quiz → Questions → Choices trong 1 transaction
- GetQuizQueryHandler map nested DTOs (QuizDto → QuestionDto → ChoiceDto)
- Build verified: 0 errors
