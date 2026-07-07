using EnglishLearning.Domain.Common;
namespace EnglishLearning.Domain.Entities;

public class Choice : BaseEntity
{
    public string ChoiceText { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public Guid QuestionId { get; set; }

    // Navigation
    public Question Question { get; set; } = null!;
}
