using EnglishLearning.Domain.Common;
using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Domain.Entities;

public class LearningHistory : BaseEntity
{
    public string UserId { get; set; } = string.Empty;
    public ActionType ActionType { get; set; }
    public Guid TargetId { get; set; }
    public string? Details { get; set; }
    public decimal? Score { get; set; }

    public LearningHistory() { }
}
