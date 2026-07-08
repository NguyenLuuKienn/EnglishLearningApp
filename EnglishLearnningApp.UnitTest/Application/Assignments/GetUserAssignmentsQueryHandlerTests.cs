using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Assignments.Queries.GetUserAssignments;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using AutoMapper;
using UserEntity = EnglishLearning.Domain.Entities.User;
using QuizAssignmentEntity = EnglishLearning.Domain.Entities.QuizAssignment;

namespace EnglishLearnningApp.UnitTest.Application.Assignments;

public class GetUserAssignmentsQueryHandlerTests
{
    [Fact]
    public async Task Handle_ValidUserId_ShouldReturnAssignments()
    {
        var assignmentRepo = new Mock<IQuizAssignmentRepository>();
        var userRepo = new Mock<IUserRepository>();
        var mapper = new Mock<IMapper>();

        var userId = Guid.NewGuid();
        var user = new UserEntity
        {
            Username = "testuser",
            Email = "test@test.com",
            PasswordHash = "hash",
            Role = UserRole.Student,
            IsActive = true
        };
        var assignments = new List<QuizAssignmentEntity>
        {
            new QuizAssignmentEntity
            {
                QuizId = Guid.NewGuid(),
                TargetRole = UserRole.Student,
                TargetUserId = null,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(7),
                Status = AssignmentStatus.Scheduled
            }
        };
        var dtos = new List<QuizAssignmentDto>();

        userRepo.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);
        assignmentRepo.Setup(r => r.GetAllWithQuizAsync()).ReturnsAsync(assignments);
        mapper.Setup(m => m.Map<List<QuizAssignmentDto>>(It.IsAny<List<QuizAssignmentEntity>>())).Returns(dtos);

        var handler = new GetUserAssignmentsQueryHandler(assignmentRepo.Object, userRepo.Object, mapper.Object);
        var query = new GetUserAssignmentsQuery(userId.ToString());

        var result = await handler.Handle(query, CancellationToken.None);
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task Handle_InvalidUserId_ShouldThrowException()
    {
        var assignmentRepo = new Mock<IQuizAssignmentRepository>();
        var userRepo = new Mock<IUserRepository>();
        var mapper = new Mock<IMapper>();

        var handler = new GetUserAssignmentsQueryHandler(assignmentRepo.Object, userRepo.Object, mapper.Object);
        var query = new GetUserAssignmentsQuery("not-a-guid");

        await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(query, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_UserNotFound_ShouldThrowException()
    {
        var assignmentRepo = new Mock<IQuizAssignmentRepository>();
        var userRepo = new Mock<IUserRepository>();
        var mapper = new Mock<IMapper>();

        var userId = Guid.NewGuid();
        userRepo.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync((UserEntity?)null);

        var handler = new GetUserAssignmentsQueryHandler(assignmentRepo.Object, userRepo.Object, mapper.Object);
        var query = new GetUserAssignmentsQuery(userId.ToString());

        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(query, CancellationToken.None));
    }
}
