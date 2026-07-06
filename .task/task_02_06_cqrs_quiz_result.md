# Task 2.6: Create CQRS — QuizResult Features

## Description

Implement CQRS commands and queries for QuizResult feature. Includes the auto-grading logic when a user submits a quiz.

## Priority
🔴 Critical — Core quiz submission and grading feature

## Dependencies
- Task 2.1 (Application dependencies)
- Task 2.2 (Common classes)
- Task 2.3 (DTOs)
- Task 1.8 (Domain interfaces)

## Files to Create

| File | Action |
|------|--------|
| `Features/QuizResults/Commands/SubmitQuizResult/SubmitQuizResultCommand.cs` | Create |
| `Features/QuizResults/Commands/SubmitQuizResult/SubmitQuizResultCommandHandler.cs` | Create |
| `Features/QuizResults/Queries/GetQuizResult/GetQuizResultQuery.cs` | Create |
| `Features/QuizResults/Queries/GetQuizResult/GetQuizResultQueryHandler.cs` | Create |
| `Features/QuizResults/Queries/GetUserQuizResults/GetUserQuizResultsQuery.cs` | Create |
| `Features/QuizResults/Queries/GetUserQuizResults/GetUserQuizResultsQueryHandler.cs` | Create |
| `Features/QuizResults/Validators/SubmitQuizResultCommandValidator.cs` | Create |

## Steps

### Step 1: Create SubmitQuizResultCommand
1. Properties:
   - `QuizId` (Guid)
   - `UserId` (string)
   - `DurationMinutes` (int)
   - `Answers` (List<AnswerCommand>) — user's answers
2. Nested `AnswerCommand` record — `QuestionId` (Guid), `SelectedChoiceId` (Guid?) for multiple choice, or `AnswerText` (string?) for fill-in-blank
3. Implements `IRequest<Result<QuizResultDto>>`

### Step 2: Create SubmitQuizResultCommandHandler (Auto-Grading Logic)
1. Inject `IUnitOfWork`
2. Get quiz with questions and choices using `GetQuizWithQuestionsAsync`
3. If quiz not found, return `Result<QuizResultDto>.Failure(QuizNotFound)`
4. **Auto-grading logic:**
   - Iterate through user's answers
   - For MultipleChoice: compare `SelectedChoiceId` with the Choice where `IsCorrect == true`
   - For FillInBlank: compare `AnswerText` with `Question.CorrectAnswer` (case-insensitive)
   - Count correct answers
5. Create `QuizResult` using static `Create()` factory method
6. Add to repository and save
7. Return `QuizResultDto`

### Step 3: Create GetQuizResultQuery
1. Properties: `Id` (Guid)
2. Implements `IRequest<Result<QuizResultDto>>`
3. Handler: get by Id, map to DTO

### Step 4: Create GetUserQuizResultsQuery
1. Properties: `UserId` (string), `PageNumber` (int), `PageSize` (int)
2. Implements `IRequest<Result<PagedResult<QuizResultDto>>>`
3. Handler: get by UserId, return paged results ordered by CompletedAt descending

### Step 5: Create Validator
1. `SubmitQuizResultCommandValidator` — QuizId required, UserId required, DurationMinutes >= 0, Answers must have at least 1 item

## Expected Code Pattern

```csharp
// Auto-grading logic in handler
public async Task<Common.Result<DTOs.QuizResultDto>> Handle(SubmitQuizResultCommand request, CancellationToken cancellationToken)
{
    var quiz = await _unitOfWork.Quizzes.GetQuizWithQuestionsAsync(request.QuizId);
    if (quiz == null)
        return Common.Result<DTOs.QuizResultDto>.Failure(Domain.Constants.ErrorMessages.QuizNotFound);

    int correctAnswers = 0;

    foreach (var answer in request.Answers)
    {
        var question = quiz.Questions.FirstOrDefault(q => q.Id == answer.QuestionId);
        if (question == null) continue;

        if (question.QuestionType == Domain.Enums.QuestionType.MultipleChoice)
        {
            var correctChoice = question.Choices.FirstOrDefault(c => c.IsCorrect);
            if (correctChoice != null && answer.SelectedChoiceId == correctChoice.Id)
                correctAnswers++;
        }
        else if (question.QuestionType == Domain.Enums.QuestionType.FillInBlank)
        {
            if (string.Equals(answer.AnswerText, question.CorrectAnswer, StringComparison.OrdinalIgnoreCase))
                correctAnswers++;
        }
    }

    var result = Domain.Entities.QuizResult.Create(
        request.QuizId, request.UserId,
        request.Answers.Count, correctAnswers,
        request.DurationMinutes
    );

    await _unitOfWork.QuizResults.AddAsync(result);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    var dto = new DTOs.QuizResultDto
    {
        Id = result.Id,
        QuizId = result.QuizId,
        UserId = result.UserId,
        Score = result.Score,
        TotalQuestions = result.TotalQuestions,
        CorrectAnswers = result.CorrectAnswers,
        DurationMinutes = result.DurationMinutes,
        CompletedAt = result.CompletedAt
    };

    return Common.Result<DTOs.QuizResultDto>.Success(dto);
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Application` — 0 errors
- [ ] Auto-grading correctly handles MultipleChoice (compare ChoiceId)
- [ ] Auto-grading correctly handles FillInBlank (case-insensitive text compare)
- [ ] Score is calculated as percentage
- [ ] QuizResult entity is created and saved

## Acceptance Criteria

- [ ] SubmitQuizResultCommand with nested AnswerCommand record
- [ ] SubmitQuizResultCommandHandler implements auto-grading for MultipleChoice and FillInBlank
- [ ] Auto-grading returns QuizResultDto with calculated score
- [ ] GetQuizResultQuery + Handler returns single result by Id
- [ ] GetUserQuizResultsQuery + Handler returns paged results by UserId
- [ ] SubmitQuizResultCommandValidator validates required fields
- [ ] Application project builds successfully
