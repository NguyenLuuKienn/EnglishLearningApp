using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;

namespace EnglishLearnningApp.UnitTest.Domain.Entities;

public class LearningHistoryTests
{
    [Fact]
    public void Create_ShouldSetAllProperties()
    {
        var history = new LearningHistory
        {
            UserId = "user-123",
            ActionType = ActionType.CompleteQuiz,
            TargetId = Guid.NewGuid(),
            Details = "Completed quiz",
            Score = 85m
        };

        history.UserId.Should().Be("user-123");
        history.ActionType.Should().Be(ActionType.CompleteQuiz);
        history.Score.Should().Be(85m);
    }
}
