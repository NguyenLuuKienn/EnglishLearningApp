namespace EnglishLearning.Application.DTOs;

public class QuizResultDto
{
    public Guid Id { get; set; }
    public Guid QuizId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public decimal Score { get; set; }
    public int TotalQuestions { get; set; }
    public int CorrectAnswers { get; set; }
    public int DurationMinutes { get; set; }
    public DateTime CompletedAt { get; set; }
}
