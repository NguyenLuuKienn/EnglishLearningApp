using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Assignments.Queries.GetAssignmentById;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using AutoMapper;
using QuizAssignmentEntity = EnglishLearning.Domain.Entities.QuizAssignment;

namespace EnglishLearnningApp.UnitTest.Application.Assignments;

public class GetAssignmentByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_ExistingAssignment_ShouldReturnDto()
    {
        var repo = new Mock<IQuizAssignmentRepository>();
        var mapper = new Mock<IMapper>();

        var assignment = new QuizAssignmentEntity
        {
            QuizId = Guid.NewGuid(),
            TargetRole = UserRole.Student,
            TargetUserId = null,
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddDays(7),
            Status = AssignmentStatus.Scheduled
        };
        var dto = new QuizAssignmentDto { Id = assignment.Id };

        repo.Setup(r => r.GetByIdAsync(assignment.Id)).ReturnsAsync(assignment);
        mapper.Setup(m => m.Map<QuizAssignmentDto>(assignment)).Returns(dto);

        var handler = new GetAssignmentByIdQueryHandler(repo.Object, mapper.Object);
        var query = new GetAssignmentByIdQuery(assignment.Id);

        var result = await handler.Handle(query, CancellationToken.None);
        result.Should().NotBeNull();
        result.Id.Should().Be(assignment.Id);
    }

    [Fact]
    public async Task Handle_NonExistingAssignment_ShouldThrowException()
    {
        var repo = new Mock<IQuizAssignmentRepository>();
        var mapper = new Mock<IMapper>();

        var id = Guid.NewGuid();
        repo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((QuizAssignmentEntity?)null);

        var handler = new GetAssignmentByIdQueryHandler(repo.Object, mapper.Object);
        var query = new GetAssignmentByIdQuery(id);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(query, CancellationToken.None));
    }
}
