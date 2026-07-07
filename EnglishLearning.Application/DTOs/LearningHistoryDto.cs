using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Application.DTOs;

public class LearningHistoryDto
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public ActionType ActionType { get; set; }
    public Guid TargetId { get; set; }
    public string? Details { get; set; }
    public decimal? Score { get; set; }
    public DateTime CreatedAt { get; set; }
}
