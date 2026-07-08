using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Assignments.Commands.AssignQuiz;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using EnglishLearnningApp.UnitTest.Helpers;
using QuizEntity = EnglishLearning.Domain.Entities.Quiz;
using QuizAssignmentEntity = EnglishLearning.Domain.Entities.QuizAssignment;

namespace EnglishLearnningApp.UnitTest.Application.Assignments;

public class AssignQuizCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidCommand_WithTargetRole_ShouldCreateAssignment()
    {
        var assignmentRepo = new Mock<IQuizAssignmentRepository>();
        var quizRepo = new Mock<IQuizRepository>();

        var quiz = TestDataBuilder.CreateValidQuiz();
        quizRepo.Setup(r => r.GetByIdAsync(quiz.Id)).ReturnsAsync(quiz);

        var handler = new AssignQuizCommandHandler(assignmentRepo.Object, quizRepo.Object);
        var command = new AssignQuizCommand(quiz.Id, UserRole.Student, null, DateTime.Now, DateTime.Now.AddDays(7));

        await handler.Handle(command, CancellationToken.None);

        assignmentRepo.Verify(r => r.AddAsync(It.IsAny<QuizAssignmentEntity>()), Times.Once);
    }

    [Fact]
    public async Task Handle_QuizNotFound_ShouldThrowException()
    {
        var assignmentRepo = new Mock<IQuizAssignmentRepository>();
        var quizRepo = new Mock<IQuizRepository>();

        var quizId = Guid.NewGuid();
        quizRepo.Setup(r => r.GetByIdAsync(quizId)).ReturnsAsync((QuizEntity?)null);

        var handler = new AssignQuizCommandHandler(assignmentRepo.Object, quizRepo.Object);
        var command = new AssignQuizCommand(quizId, UserRole.Student, null, DateTime.Now, DateTime.Now.AddDays(1));

        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_NoTarget_ShouldThrowException()
    {
        var assignmentRepo = new Mock<IQuizAssignmentRepository>();
        var quizRepo = new Mock<IQuizRepository>();

        var quiz = TestDataBuilder.CreateValidQuiz();
        quizRepo.Setup(r => r.GetByIdAsync(quiz.Id)).ReturnsAsync(quiz);

        var handler = new AssignQuizCommandHandler(assignmentRepo.Object, quizRepo.Object);
        var command = new AssignQuizCommand(quiz.Id, null, null, DateTime.Now, DateTime.Now.AddDays(1));

        await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(command, CancellationToken.None));
    }
}
