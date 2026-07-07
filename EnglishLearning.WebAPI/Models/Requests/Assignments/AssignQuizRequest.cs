using EnglishLearning.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EnglishLearning.WebAPI.Models.Requests.Assignments;

public class AssignQuizRequest
{
    [Required]
    public Guid QuizId { get; set; }

    public UserRole? TargetRole { get; set; }

    public string? TargetUserId { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }
}
