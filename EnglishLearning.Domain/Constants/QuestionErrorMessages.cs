namespace EnglishLearning.Domain.Constants;

public static class QuestionErrorMessages
{
    public const string NotFound = "Question not found";
    public const string QuestionTextRequired = "Question text is required";
    public const string MissingChoices = "Multiple choice question must have at least 2 choices";
    public const string NoCorrectAnswer = "At least one choice must be marked as correct";
}
