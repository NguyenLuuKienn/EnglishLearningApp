namespace EnglishLearning.Domain.Constants;

public static class QuizErrorMessages
{
    public const string NotFound = "Quiz not found";
    public const string AlreadySubmitted = "Quiz has already been submitted";
    public const string NoQuestions = "Quiz must have at least one question";
    public const string InvalidPassingScore = "Passing score must be between 0 and 100";
}
