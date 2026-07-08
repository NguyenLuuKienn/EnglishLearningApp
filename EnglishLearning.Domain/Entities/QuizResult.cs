using EnglishLearning.Domain.Common;
namespace EnglishLearning.Domain.Entities;

public class QuizResult : BaseEntity
{
    public Guid QuizId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public decimal Score { get; set; }
    public int TotalQuestions { get; set; }
    public int CorrectAnswers { get; set; }
    public int DurationMinutes { get; set; }
    public DateTime CompletedAt { get; set; }

    // Navigation
    public Quiz Quiz { get; set; } = null!;

    public QuizResult()
    {
        CompletedAt = DateTime.UtcNow;
    }
}
