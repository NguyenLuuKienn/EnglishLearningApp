using EnglishLearning.Application.Features.Assignments.Commands.CancelAssignment;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using QuizAssignmentEntity = EnglishLearning.Domain.Entities.QuizAssignment;

namespace EnglishLearnningApp.UnitTest.Application.Assignments;

public class CancelAssignmentCommandHandlerTests
{
    [Fact]
    public async Task Handle_ExistingAssignment_ShouldCancel()
    {
        var repo = new Mock<IQuizAssignmentRepository>();
        var assignment = new QuizAssignmentEntity
        {
            QuizId = Guid.NewGuid(),
            TargetRole = UserRole.Student,
            TargetUserId = null,
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddDays(7),
            Status = AssignmentStatus.Scheduled
        };
        repo.Setup(r => r.GetByIdAsync(assignment.Id)).ReturnsAsync(assignment);

        var handler = new CancelAssignmentCommandHandler(repo.Object);
        var command = new CancelAssignmentCommand(assignment.Id);

        await handler.Handle(command, CancellationToken.None);

        assignment.Status.Should().Be(AssignmentStatus.Cancelled);
    }

    [Fact]
    public async Task Handle_NonExistingAssignment_ShouldThrowException()
    {
        var repo = new Mock<IQuizAssignmentRepository>();
        var id = Guid.NewGuid();
        repo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((QuizAssignmentEntity?)null);

        var handler = new CancelAssignmentCommandHandler(repo.Object);
        var command = new CancelAssignmentCommand(id);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }
}
