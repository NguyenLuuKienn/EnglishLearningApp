# Task 2.3: Create DTOs

## Description

Create Data Transfer Objects (DTOs) for all entities. DTOs define what data is exposed through the API.

## Priority
🔴 Critical — Used by all CQRS queries and API responses

## Dependencies
- Task 2.1 (Application dependencies)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/DTOs/VocabularyDto.cs` | Create |
| `EnglishLearning.Application/DTOs/QuestionDto.cs` | Create |
| `EnglishLearning.Application/DTOs/QuizDto.cs` | Create |
| `EnglishLearning.Application/DTOs/QuizResultDto.cs` | Create |
| `EnglishLearning.Application/DTOs/ChoiceDto.cs` | Create |

## Steps

### Step 1: Create VocabularyDto
- Properties: `Id` (Guid), `Word` (string), `Definition` (string), `Example` (string?), `PartOfSpeech` (string?), `Difficulty` (DifficultyLevel)

### Step 2: Create ChoiceDto
- Properties: `Id` (Guid), `ChoiceText` (string), `IsCorrect` (bool)

### Step 3: Create QuestionDto
- Properties: `Id` (Guid), `QuestionText` (string), `QuestionType` (QuestionType), `Difficulty` (DifficultyLevel), `Choices` (List<ChoiceDto>?)

### Step 4: Create QuizDto
- Properties: `Id` (Guid), `Title` (string), `Description` (string?), `Difficulty` (DifficultyLevel), `TimeLimitMinutes` (int), `PassingScore` (decimal), `Questions` (List<QuestionDto>?)

### Step 5: Create QuizResultDto
- Properties: `Id` (Guid), `QuizId` (Guid), `UserId` (string), `Score` (decimal), `TotalQuestions` (int), `CorrectAnswers` (int), `DurationMinutes` (int), `CompletedAt` (DateTime)

## Expected Code

```csharp
// VocabularyDto.cs
namespace EnglishLearning.Application.DTOs;

public class VocabularyDto
{
    public Guid Id { get; set; }
    public string Word { get; set; } = string.Empty;
    public string Definition { get; set; } = string.Empty;
    public string? Example { get; set; }
    public string? PartOfSpeech { get; set; }
    public Domain.Enums.DifficultyLevel Difficulty { get; set; }
}

// ChoiceDto.cs
namespace EnglishLearning.Application.DTOs;

public class ChoiceDto
{
    public Guid Id { get; set; }
    public string ChoiceText { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
}

// QuestionDto.cs
namespace EnglishLearning.Application.DTOs;

public class QuestionDto
{
    public Guid Id { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public Domain.Enums.QuestionType QuestionType { get; set; }
    public Domain.Enums.DifficultyLevel Difficulty { get; set; }
    public List<ChoiceDto>? Choices { get; set; }
}

// QuizDto.cs
namespace EnglishLearning.Application.DTOs;

public class QuizDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Domain.Enums.DifficultyLevel Difficulty { get; set; }
    public int TimeLimitMinutes { get; set; }
    public decimal PassingScore { get; set; }
    public List<QuestionDto>? Questions { get; set; }
}

// QuizResultDto.cs
namespace EnglishLearning.Application.DTOs;

public class QuizResultDto
{
    public Guid Id { get; set; }
    public Guid QuizId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public decimal Score { get; set; }
    public int TotalQuestions { get; set; }
    public int CorrectAnswers { get; set; }
    public int DurationMinutes { get; set; }
    public DateTime CompletedAt { get; set; }
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Application` — 0 errors
- [ ] All DTOs are `public class`
- [ ] All DTOs have appropriate properties matching entities
- [ ] Nested DTOs (ChoiceDto in QuestionDto, QuestionDto in QuizDto) are correctly referenced

## Acceptance Criteria

- [ ] `VocabularyDto` with Id, Word, Definition, Example, PartOfSpeech, Difficulty
- [ ] `ChoiceDto` with Id, ChoiceText, IsCorrect
- [ ] `QuestionDto` with Id, QuestionText, QuestionType, Difficulty, Choices
- [ ] `QuizDto` with Id, Title, Description, Difficulty, TimeLimitMinutes, PassingScore, Questions
- [ ] `QuizResultDto` with Id, QuizId, UserId, Score, TotalQuestions, CorrectAnswers, DurationMinutes, CompletedAt
- [ ] Application project builds successfully
