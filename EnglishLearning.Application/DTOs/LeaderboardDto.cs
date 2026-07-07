namespace EnglishLearning.Application.DTOs;

public class LeaderboardDto
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public decimal TotalScore { get; set; }
    public int QuizzesCompleted { get; set; }
    public decimal AverageScore { get; set; }
    public int Streak { get; set; }
    public int Rank { get; set; }
}
