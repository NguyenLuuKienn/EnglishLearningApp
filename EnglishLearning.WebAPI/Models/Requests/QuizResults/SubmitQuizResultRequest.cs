using System.ComponentModel.DataAnnotations;

namespace EnglishLearning.WebAPI.Models.Requests.QuizResults;

public class SubmitQuizResultRequest
{
    [Required]
    public Guid QuizId { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    public int DurationMinutes { get; set; }

    [MinLength(1)]
    public List<AnswerRequest> Answers { get; set; } = new();
}
