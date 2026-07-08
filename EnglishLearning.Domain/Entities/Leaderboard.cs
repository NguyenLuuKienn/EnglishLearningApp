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

    public Leaderboard() { }

    public void UpdateScore(decimal score)
    {
        QuizzesCompleted++;
        TotalScore += score;
        AverageScore = TotalScore / QuizzesCompleted;
        Streak++;
        LastActiveDate = DateTime.UtcNow;
    }
}
