using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Assignments.Queries.GetActiveAssignments;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using AutoMapper;
using QuizAssignmentEntity = EnglishLearning.Domain.Entities.QuizAssignment;

namespace EnglishLearnningApp.UnitTest.Application.Assignments;

public class GetActiveAssignmentsQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnActiveAssignments()
    {
        var repo = new Mock<IQuizAssignmentRepository>();
        var mapper = new Mock<IMapper>();

        var assignments = new List<QuizAssignmentEntity>
        {
            new QuizAssignmentEntity
            {
                QuizId = Guid.NewGuid(),
                TargetRole = UserRole.Student,
                TargetUserId = null,
                StartTime = DateTime.UtcNow.AddHours(-1),
                EndTime = DateTime.UtcNow.AddDays(7),
                Status = AssignmentStatus.Scheduled
            }
        };

        repo.Setup(r => r.GetAllAsync()).ReturnsAsync(assignments);
        mapper.Setup(m => m.Map<List<QuizAssignmentDto>>(It.IsAny<List<QuizAssignmentEntity>>())).Returns(new List<QuizAssignmentDto>());

        var handler = new GetActiveAssignmentsQueryHandler(repo.Object, mapper.Object);
        var query = new GetActiveAssignmentsQuery();

        var result = await handler.Handle(query, CancellationToken.None);
        result.Should().NotBeNull();
    }
}
