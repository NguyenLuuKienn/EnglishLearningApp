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

    public static LearningHistory Create(string userId, ActionType actionType, Guid targetId, string? details = null, decimal? score = null)
    {
        return new LearningHistory
        {
            UserId = userId,
            ActionType = actionType,
            TargetId = targetId,
            Details = details,
            Score = score
        };
    }
}
