using EnglishLearning.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EnglishLearning.WebAPI.Models.Requests.Quizzes;

public class QuestionRequest
{
    [Required]
    [StringLength(2000)]
    public string QuestionText { get; set; } = string.Empty;

    public QuestionType QuestionType { get; set; }
    public DifficultyLevel Difficulty { get; set; }
    public string? CorrectAnswer { get; set; }
    public List<ChoiceRequest> Choices { get; set; } = new();
}
