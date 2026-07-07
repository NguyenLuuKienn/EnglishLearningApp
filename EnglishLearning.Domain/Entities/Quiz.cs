using EnglishLearning.Domain.Common;
using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Domain.Entities;

public class Quiz : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DifficultyLevel Difficulty { get; set; }
    public int TimeLimitMinutes { get; set; }
    public decimal PassingScore { get; set; } = 50m;
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }

    // Navigation
    public ICollection<Question> Questions { get; set; } = new List<Question>();
    public ICollection<QuizResult> Results { get; set; } = new List<QuizResult>();
}
