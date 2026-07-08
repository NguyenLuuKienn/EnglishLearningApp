using EnglishLearning.Application.Features.Quizzes.Commands.UpdateQuiz;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using EnglishLearnningApp.UnitTest.Helpers;

namespace EnglishLearnningApp.UnitTest.Application.Quizzes;

public class UpdateQuizCommandHandlerTests
{
    [Fact]
    public async Task Handle_ExistingQuiz_ShouldUpdate()
    {
        var repo = new Mock<IQuizRepository>();
        var quiz = TestDataBuilder.CreateValidQuiz();
        repo.Setup(r => r.GetByIdAsync(quiz.Id)).ReturnsAsync(quiz);

        var handler = new UpdateQuizCommandHandler(repo.Object);
        var command = new UpdateQuizCommand(quiz.Id, "New Title", "New desc", DifficultyLevel.Advanced, 60, 80m);

        await handler.Handle(command, CancellationToken.None);

        quiz.Title.Should().Be("New Title");
        quiz.Difficulty.Should().Be(DifficultyLevel.Advanced);
    }

    [Fact]
    public async Task Handle_NonExistingQuiz_ShouldThrowException()
    {
        var repo = new Mock<IQuizRepository>();
        var id = Guid.NewGuid();
        repo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Quiz?)null);

        var handler = new UpdateQuizCommandHandler(repo.Object);
        var command = new UpdateQuizCommand(id, "Title", null, DifficultyLevel.Beginner, 30, 50m);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }
}
