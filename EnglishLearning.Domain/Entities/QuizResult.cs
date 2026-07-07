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

    public static QuizResult Create(Guid quizId, string userId, int totalQuestions, int correctAnswers, int durationMinutes)
    {
        var score = totalQuestions > 0 ? (decimal)Math.Round((correctAnswers / (double)totalQuestions) * 100, 2) : 0m;

        return new QuizResult
        {
            QuizId = quizId,
            UserId = userId,
            TotalQuestions = totalQuestions,
            CorrectAnswers = correctAnswers,
            DurationMinutes = durationMinutes,
            Score = score
        };
    }
}
