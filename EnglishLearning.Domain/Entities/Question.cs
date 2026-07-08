using EnglishLearning.Domain.Common;
namespace EnglishLearning.Domain.Entities;

public class Question : BaseEntity
{
    public string QuestionText { get; set; } = string.Empty;
    public Enums.QuestionType QuestionType { get; set; }
    public Enums.DifficultyLevel Difficulty { get; set; }
    public string? CorrectAnswer { get; set; }
    public string? Explanation { get; set; }
    public Guid QuizId { get; set; }

    // Navigation
    public Quiz Quiz { get; set; } = null!;
    public ICollection<Choice> Choices { get; set; } = new List<Choice>();

    public Question() { }
}
