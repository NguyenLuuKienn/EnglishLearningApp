using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Application.DTOs;

public class QuizAssignmentDto
{
    public Guid Id { get; set; }
    public Guid QuizId { get; set; }
    public string QuizTitle { get; set; } = string.Empty;
    public UserRole? TargetRole { get; set; }
    public string? TargetUserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public AssignmentStatus Status { get; set; }
}
