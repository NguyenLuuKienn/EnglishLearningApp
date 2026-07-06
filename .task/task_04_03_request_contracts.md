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
| `Models/Requests/Vocabulary/CreateVocabularyRequest.cs` | Create |
| `Models/Requests/Vocabulary/UpdateVocabularyRequest.cs` | Create |
| `Models/Requests/Quizzes/CreateQuizRequest.cs` | Create |
| `Models/Requests/Quizzes/QuestionRequest.cs` | Create |
| `Models/Requests/Quizzes/ChoiceRequest.cs` | Create |
| `Models/Requests/Quizzes/UpdateQuizRequest.cs` | Create |
| `Models/Requests/QuizResults/SubmitQuizResultRequest.cs` | Create |
| `Models/Requests/QuizResults/AnswerRequest.cs` | Create |

## Steps

### Step 1: Create Vocabulary requests
1. `CreateVocabularyRequest` — Word, Definition, Example, PartOfSpeech, Difficulty
2. `UpdateVocabularyRequest` — same as Create

### Step 2: Create Quiz requests
1. `CreateQuizRequest` — Title, Description, Difficulty, TimeLimitMinutes, PassingScore, Questions (list of QuestionRequest)
2. `QuestionRequest` — QuestionText, QuestionType, Difficulty, CorrectAnswer, Choices (list of ChoiceRequest)
3. `ChoiceRequest` — ChoiceText, IsCorrect
4. `UpdateQuizRequest` — Title, Description, Difficulty, TimeLimitMinutes, PassingScore

### Step 3: Create QuizResult requests
1. `SubmitQuizResultRequest` — QuizId, UserId, DurationMinutes, Answers (list of AnswerRequest)
2. `AnswerRequest` — QuestionId, SelectedChoiceId (for MultipleChoice), AnswerText (for FillInBlank)

### Step 4: Add data annotations
1. Add `[Required]`, `[StringLength]` attributes where appropriate

## Expected Code

```csharp
// CreateVocabularyRequest.cs
using EnglishLearning.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EnglishLearning.WebAPI.Models.Requests.Vocabulary;

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

    public DifficultyLevel Difficulty { get; set; }
}

// UpdateVocabularyRequest.cs
using EnglishLearning.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EnglishLearning.WebAPI.Models.Requests.Vocabulary;

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

    public DifficultyLevel Difficulty { get; set; }
}

// CreateQuizRequest.cs
using EnglishLearning.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EnglishLearning.WebAPI.Models.Requests.Quizzes;

public class CreateQuizRequest
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    public DifficultyLevel Difficulty { get; set; }
    public int TimeLimitMinutes { get; set; }
    public decimal PassingScore { get; set; } = 50m;

    [MinLength(1)]
    public List<QuestionRequest> Questions { get; set; } = new();
}

// QuestionRequest.cs
using EnglishLearning.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EnglishLearning.WebAPI.Models.Requests.Quizzes;

public class QuestionRequest
{
    [Required]
    [StringLength(2000)]
    public string QuestionText { get; set; } = string.Empty;

    public QuestionType QuestionType { get; set; }
    public DifficultyLevel Difficulty { get; set; }
    public string? CorrectAnswer { get; set; }
    public List<ChoiceRequest> Choices { get; set; } = new();
}

// ChoiceRequest.cs
using System.ComponentModel.DataAnnotations;

namespace EnglishLearning.WebAPI.Models.Requests.Quizzes;

public class ChoiceRequest
{
    [Required]
    [StringLength(500)]
    public string ChoiceText { get; set; } = string.Empty;

    public bool IsCorrect { get; set; }
}

// UpdateQuizRequest.cs
using EnglishLearning.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EnglishLearning.WebAPI.Models.Requests.Quizzes;

public class UpdateQuizRequest
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    public DifficultyLevel Difficulty { get; set; }
    public int TimeLimitMinutes { get; set; }
    public decimal PassingScore { get; set; }
}

// SubmitQuizResultRequest.cs
using System.ComponentModel.DataAnnotations;

namespace EnglishLearning.WebAPI.Models.Requests.QuizResults;

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

// AnswerRequest.cs
using System.ComponentModel.DataAnnotations;

namespace EnglishLearning.WebAPI.Models.Requests.QuizResults;

public class AnswerRequest
{
    [Required]
    public Guid QuestionId { get; set; }

    public Guid? SelectedChoiceId { get; set; }
    public string? AnswerText { get; set; }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.WebAPI` — 0 errors ✅
- [x] All request classes have `[Required]` and `[StringLength]` attributes ✅
- [x] Nested request objects (QuestionRequest, ChoiceRequest, AnswerRequest) are in separate files ✅
- [x] Enums are properly referenced from Domain layer ✅

## Acceptance Criteria

- [x] `CreateVocabularyRequest` with Word, Definition, Example, PartOfSpeech, Difficulty ✅
- [x] `UpdateVocabularyRequest` with same properties ✅
- [x] `CreateQuizRequest` with Title, Description, Difficulty, TimeLimitMinutes, PassingScore, Questions ✅
- [x] `QuestionRequest` with QuestionText, QuestionType, Difficulty, CorrectAnswer, Choices ✅
- [x] `ChoiceRequest` with ChoiceText, IsCorrect ✅
- [x] `UpdateQuizRequest` with Title, Description, Difficulty, TimeLimitMinutes, PassingScore ✅
- [x] `SubmitQuizResultRequest` with QuizId, UserId, DurationMinutes, Answers ✅
- [x] `AnswerRequest` with QuestionId, SelectedChoiceId, AnswerText ✅
- [x] WebAPI project builds successfully ✅

---

## ✅ Completed: 2026-07-06

- **Folder structure:** `Models/Requests/{Feature}/`
- **Vocabulary requests:**
  - `CreateVocabularyRequest` — Word (required, max 200), Definition (required, max 1000), Example (max 1000), PartOfSpeech (max 50), Difficulty
  - `UpdateVocabularyRequest` — same as Create
- **Quiz requests:**
  - `CreateQuizRequest` — Title (required, max 200), Description (max 1000), Difficulty, TimeLimitMinutes, PassingScore, Questions (min 1)
  - `QuestionRequest` — QuestionText (required, max 2000), QuestionType, Difficulty, CorrectAnswer, Choices
  - `ChoiceRequest` — ChoiceText (required, max 500), IsCorrect
  - `UpdateQuizRequest` — Title, Description, Difficulty, TimeLimitMinutes, PassingScore
- **QuizResult requests:**
  - `SubmitQuizResultRequest` — QuizId (required), UserId (required), DurationMinutes, Answers (min 1)
  - `AnswerRequest` — QuestionId (required), SelectedChoiceId, AnswerText
- Build verified: 0 errors
- [ ] All required fields have `[Required]` attribute
- [ ] String fields have `[StringLength]` attribute
- [ ] WebAPI project builds successfully
