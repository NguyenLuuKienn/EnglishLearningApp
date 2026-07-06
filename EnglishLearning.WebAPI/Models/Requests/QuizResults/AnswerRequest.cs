using System.ComponentModel.DataAnnotations;

namespace EnglishLearning.WebAPI.Models.Requests.QuizResults;

public class AnswerRequest
{
    [Required]
    public Guid QuestionId { get; set; }

    public Guid? SelectedChoiceId { get; set; }
    public string? AnswerText { get; set; }
}
