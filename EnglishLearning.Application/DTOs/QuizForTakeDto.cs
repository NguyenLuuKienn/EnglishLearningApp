using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Application.DTOs;

public class QuizForTakeDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DifficultyLevel Difficulty { get; set; }
    public int TimeLimitMinutes { get; set; }
    public decimal PassingScore { get; set; }
    public List<QuestionForTakeDto>? Questions { get; set; }
}
