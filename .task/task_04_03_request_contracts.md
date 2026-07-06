# Task 4.3: Create Request Contracts

## Description

Create request DTOs (contracts) for all API endpoints. These define the expected input format for each operation.

## Priority
🔴 Critical — Defines API input contracts

## Dependencies
- Task 4.1 (WebAPI dependencies)

## Files to Create

| File | Action |
|------|--------|
| `Contracts/Requests/CreateVocabularyRequest.cs` | Create |
| `Contracts/Requests/UpdateVocabularyRequest.cs` | Create |
| `Contracts/Requests/CreateQuizRequest.cs` | Create |
| `Contracts/Requests/UpdateQuizRequest.cs` | Create |
| `Contracts/Requests/SubmitQuizResultRequest.cs` | Create |

## Steps

### Step 1: Create Vocabulary requests
1. `CreateVocabularyRequest` — Word, Definition, Example, PartOfSpeech, Difficulty
2. `UpdateVocabularyRequest` — same as Create

### Step 2: Create Quiz requests
1. `CreateQuizRequest` — Title, Description, Difficulty, TimeLimitMinutes, PassingScore, Questions (list of QuestionRequest)
2. `QuestionRequest` — QuestionText, QuestionType, Difficulty, CorrectAnswer, Choices (list of ChoiceRequest)
3. `ChoiceRequest` — ChoiceText, IsCorrect
4. `UpdateQuizRequest` — same as Create

### Step 3: Create QuizResult requests
1. `SubmitQuizResultRequest` — QuizId, UserId, DurationMinutes, Answers (list of AnswerRequest)
2. `AnswerRequest` — QuestionId, SelectedChoiceId (for MultipleChoice), AnswerText (for FillInBlank)

### Step 4: Add data annotations
1. Add `[Required]`, `[StringLength]` attributes where appropriate

## Expected Code

```csharp
// CreateVocabularyRequest.cs
using System.ComponentModel.DataAnnotations;

namespace EnglishLearning.WebAPI.Contracts.Requests;

public class CreateVocabularyRequest
{
    [Required]
    [StringLength(200)]
    public string Word { get; set; } = string.Empty;

    [Required]
    [StringLength(1000)]
    public string Definition { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Example { get; set; }

    [StringLength(50)]
    public string? PartOfSpeech { get; set; }

    public Domain.Enums.DifficultyLevel Difficulty { get; set; }
}

// UpdateVocabularyRequest.cs
using System.ComponentModel.DataAnnotations;

namespace EnglishLearning.WebAPI.Contracts.Requests;

public class UpdateVocabularyRequest
{
    [Required]
    [StringLength(200)]
    public string Word { get; set; } = string.Empty;

    [Required]
    [StringLength(1000)]
    public string Definition { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Example { get; set; }

    [StringLength(50)]
    public string? PartOfSpeech { get; set; }

    public Domain.Enums.DifficultyLevel Difficulty { get; set; }
}

// CreateQuizRequest.cs
using System.ComponentModel.DataAnnotations;

namespace EnglishLearning.WebAPI.Contracts.Requests;

public class CreateQuizRequest
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    public Domain.Enums.DifficultyLevel Difficulty { get; set; }
    public int TimeLimitMinutes { get; set; }
    public decimal PassingScore { get; set; } = 50m;

    [MinLength(1)]
    public List<QuestionRequest> Questions { get; set; } = new();
}

public class QuestionRequest
{
    [Required]
    [StringLength(2000)]
    public string QuestionText { get; set; } = string.Empty;

    public Domain.Enums.QuestionType QuestionType { get; set; }
    public Domain.Enums.DifficultyLevel Difficulty { get; set; }
    public string? CorrectAnswer { get; set; }
    public List<ChoiceRequest> Choices { get; set; } = new();
}

public class ChoiceRequest
{
    [Required]
    [StringLength(500)]
    public string ChoiceText { get; set; } = string.Empty;

    public bool IsCorrect { get; set; }
}

// UpdateQuizRequest.cs
using System.ComponentModel.DataAnnotations;

namespace EnglishLearning.WebAPI.Contracts.Requests;

public class UpdateQuizRequest
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    public Domain.Enums.DifficultyLevel Difficulty { get; set; }
    public int TimeLimitMinutes { get; set; }
    public decimal PassingScore { get; set; }
}

// SubmitQuizResultRequest.cs
using System.ComponentModel.DataAnnotations;

namespace EnglishLearning.WebAPI.Contracts.Requests;

public class SubmitQuizResultRequest
{
    [Required]
    public Guid QuizId { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    public int DurationMinutes { get; set; }

    [MinLength(1)]
    public List<AnswerRequest> Answers { get; set; } = new();
}

public class AnswerRequest
{
    [Required]
    public Guid QuestionId { get; set; }

    public Guid? SelectedChoiceId { get; set; }
    public string? AnswerText { get; set; }
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.WebAPI` — 0 errors
- [ ] All request classes have `[Required]` and `[StringLength]` attributes
- [ ] Nested request objects (QuestionRequest, ChoiceRequest, AnswerRequest) are defined
- [ ] Enums are properly referenced from Domain layer

## Acceptance Criteria

- [ ] `CreateVocabularyRequest` with Word, Definition, Example, PartOfSpeech, Difficulty
- [ ] `UpdateVocabularyRequest` with same properties
- [ ] `CreateQuizRequest` with Title, Description, Difficulty, TimeLimitMinutes, PassingScore, Questions
- [ ] `QuestionRequest` with QuestionText, QuestionType, Difficulty, CorrectAnswer, Choices
- [ ] `ChoiceRequest` with ChoiceText, IsCorrect
- [ ] `UpdateQuizRequest` with Title, Description, Difficulty, TimeLimitMinutes, PassingScore
- [ ] `SubmitQuizResultRequest` with QuizId, UserId, DurationMinutes, Answers
- [ ] `AnswerRequest` with QuestionId, SelectedChoiceId, AnswerText
- [ ] All required fields have `[Required]` attribute
- [ ] String fields have `[StringLength]` attribute
- [ ] WebAPI project builds successfully
