namespace EnglishLearning.Domain.Entities;

public class Quiz : Common.BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Enums.DifficultyLevel Difficulty { get; set; }
    public int TimeLimitMinutes { get; set; }
    public decimal PassingScore { get; set; } = 50m;

    // Navigation
    public ICollection<Question> Questions { get; set; } = new List<Question>();
    public ICollection<QuizResult> Results { get; set; } = new List<QuizResult>();
}
