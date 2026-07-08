using EnglishLearning.Application.Features.Quizzes.Commands.DeleteQuiz;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using EnglishLearnningApp.UnitTest.Helpers;

namespace EnglishLearnningApp.UnitTest.Application.Quizzes;

public class DeleteQuizCommandHandlerTests
{
    [Fact]
    public async Task Handle_ExistingQuiz_ShouldDelete()
    {
        var repo = new Mock<IQuizRepository>();
        var quiz = TestDataBuilder.CreateValidQuiz();
        repo.Setup(r => r.GetByIdAsync(quiz.Id)).ReturnsAsync(quiz);

        var handler = new DeleteQuizCommandHandler(repo.Object);
        var command = new DeleteQuizCommand(quiz.Id);

        await handler.Handle(command, CancellationToken.None);

        repo.Verify(r => r.Delete(quiz), Times.Once);
    }

    [Fact]
    public async Task Handle_NonExistingQuiz_ShouldThrowException()
    {
        var repo = new Mock<IQuizRepository>();
        var id = Guid.NewGuid();
        repo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Quiz?)null);

        var handler = new DeleteQuizCommandHandler(repo.Object);
        var command = new DeleteQuizCommand(id);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }
}
