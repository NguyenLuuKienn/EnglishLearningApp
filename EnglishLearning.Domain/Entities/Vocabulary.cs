using EnglishLearning.Domain.Common;
namespace EnglishLearning.Domain.Entities;

public class Vocabulary : BaseEntity
{
    public string Word { get; set; } = string.Empty;
    public string Definition { get; set; } = string.Empty;
    public string? Example { get; set; }
    public string? PartOfSpeech { get; set; }
    public Enums.DifficultyLevel Difficulty { get; set; }

    // Navigation
    public ICollection<Question> Questions { get; set; } = new List<Question>();
}
