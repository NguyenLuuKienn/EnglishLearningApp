using EnglishLearning.Domain.Common;

namespace EnglishLearning.Domain.Entities;

public class Leaderboard : BaseEntity
{
    public string UserId { get; set; } = string.Empty;
    public decimal TotalScore { get; set; }
    public int QuizzesCompleted { get; set; }
    public decimal AverageScore { get; set; }
    public int Streak { get; set; }
    public DateTime LastActiveDate { get; set; }

    public static Leaderboard Create(string userId)
    {
        return new Leaderboard
        {
            UserId = userId,
            TotalScore = 0m,
            QuizzesCompleted = 0,
            AverageScore = 0m,
            Streak = 0,
            LastActiveDate = DateTime.UtcNow
        };
    }
}
