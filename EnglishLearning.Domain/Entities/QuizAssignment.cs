using EnglishLearning.Domain.Common;
using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Domain.Entities;

public class QuizAssignment : BaseEntity
{
    public Guid QuizId { get; set; }
    public UserRole? TargetRole { get; set; }
    public string? TargetUserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public AssignmentStatus Status { get; set; }

    // Navigation
    public Quiz Quiz { get; set; } = null!;

    public QuizAssignment() { }
}
