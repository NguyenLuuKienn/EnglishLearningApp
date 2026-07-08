using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;

namespace EnglishLearnningApp.UnitTest.Domain.Entities;

public class QuizAssignmentTests
{
    [Fact]
    public void Create_ShouldSetAllProperties()
    {
        var quizId = Guid.NewGuid();
        var startTime = DateTime.Now;
        var endTime = DateTime.Now.AddDays(7);

        var assignment = new QuizAssignment
        {
            QuizId = quizId,
            TargetRole = UserRole.Student,
            TargetUserId = null,
            StartTime = startTime,
            EndTime = endTime,
            Status = AssignmentStatus.Scheduled
        };

        assignment.QuizId.Should().Be(quizId);
        assignment.TargetRole.Should().Be(UserRole.Student);
        assignment.TargetUserId.Should().BeNull();
        assignment.StartTime.Should().Be(startTime);
        assignment.EndTime.Should().Be(endTime);
        assignment.Status.Should().Be(AssignmentStatus.Scheduled);
    }

    [Fact]
    public void Create_WithTargetUser_ShouldSetTargetUserId()
    {
        var assignment = new QuizAssignment
        {
            QuizId = Guid.NewGuid(),
            TargetRole = null,
            TargetUserId = "user-123",
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddDays(1),
            Status = AssignmentStatus.Scheduled
        };

        assignment.TargetUserId.Should().Be("user-123");
        assignment.TargetRole.Should().BeNull();
    }
}
