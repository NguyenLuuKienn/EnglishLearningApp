using System.ComponentModel.DataAnnotations;

namespace EnglishLearning.WebAPI.Models.Requests.Quizzes;

public class ChoiceRequest
{
    [Required]
    [StringLength(500)]
    public string ChoiceText { get; set; } = string.Empty;

    public bool IsCorrect { get; set; }
}
