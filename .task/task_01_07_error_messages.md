# Task 1.7: Implement ErrorMessages

## Description

Implement the ErrorMessages static class with common error messages used across the application.

## Priority
🟡 High — Used for consistent error handling

## Dependencies
None

## Files to Edit

| File | Action |
|------|--------|
| `EnglishLearning.Domain/Constants/ErrorMessages.cs` | Rewrite |

## Steps

### Step 1: Convert to static class
1. Change `internal class` to `public static class`

### Step 2: Add error message constants
1. Add common error messages as `public const string` properties:
   - `ResourceNotFound` — "Resource not found"
   - `InvalidInput` — "Invalid input provided"
   - `DuplicateResource` — "Resource already exists"
   - `UnauthorizedAccess` — "Unauthorized access"
   - `ValidationFailed` — "Validation failed"
   - `QuizNotFound` — "Quiz not found"
   - `VocabularyNotFound` — "Vocabulary item not found"
   - `QuestionNotFound` — "Question not found"
   - `QuizAlreadySubmitted` — "Quiz has already been submitted"

## Expected Code

```csharp
namespace EnglishLearning.Domain.Constants;

public static class ErrorMessages
{
    public const string ResourceNotFound = "Resource not found";
    public const string InvalidInput = "Invalid input provided";
    public const string DuplicateResource = "Resource already exists";
    public const string UnauthorizedAccess = "Unauthorized access";
    public const string ValidationFailed = "Validation failed";
    public const string QuizNotFound = "Quiz not found";
    public const string VocabularyNotFound = "Vocabulary item not found";
    public const string QuestionNotFound = "Question not found";
    public const string QuizAlreadySubmitted = "Quiz has already been submitted";
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Domain` — 0 errors ✅
- [x] All error messages are accessible as `ErrorMessages.MessageName` ✅

## Acceptance Criteria

- [x] `ErrorMessages` is a `public static class` ✅
- [x] Has at least 9 error message constants ✅
- [x] General messages: ResourceNotFound, InvalidInput, DuplicateResource, UnauthorizedAccess, ValidationFailed ✅
- [x] Domain-specific messages: QuizNotFound, VocabularyNotFound, QuestionNotFound, QuizAlreadySubmitted ✅
- [x] Domain project builds successfully ✅

---

## ✅ Completed: 2026-07-06

- `ErrorMessages` rewritten as `public static class`
- 9 error message constants: 5 general + 4 domain-specific
- Build verified: 0 errors
