using EnglishLearning.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EnglishLearning.WebAPI.Models.Requests.Quizzes;

public class UpdateQuizRequest
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    public DifficultyLevel Difficulty { get; set; }
    public int TimeLimitMinutes { get; set; }
    public decimal PassingScore { get; set; }
}
