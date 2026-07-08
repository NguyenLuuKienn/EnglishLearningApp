using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Quizzes.Queries.GetQuizForTake;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using EnglishLearnningApp.UnitTest.Helpers;

namespace EnglishLearnningApp.UnitTest.Application.Quizzes;

public class GetQuizForTakeQueryHandlerTests
{
    [Fact]
    public async Task Handle_ExistingQuiz_ShouldReturnQuizForTakeDto()
    {
        var repo = new Mock<IQuizRepository>();
        var quiz = TestDataBuilder.CreateQuizWithQuestions(2);

        repo.Setup(r => r.GetQuizWithQuestionsAsync(quiz.Id)).ReturnsAsync(quiz);

        var handler = new GetQuizForTakeQueryHandler(repo.Object);
        var query = new GetQuizForTakeQuery(quiz.Id);

        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(quiz.Id);
        result.Questions.Should().HaveCount(2);
    }

    [Fact]
    public async Task Handle_ShouldNotExposeIsCorrect()
    {
        var repo = new Mock<IQuizRepository>();
        var quiz = TestDataBuilder.CreateQuizWithQuestions(1);

        repo.Setup(r => r.GetQuizWithQuestionsAsync(quiz.Id)).ReturnsAsync(quiz);

        var handler = new GetQuizForTakeQueryHandler(repo.Object);
        var query = new GetQuizForTakeQuery(quiz.Id);

        var result = await handler.Handle(query, CancellationToken.None);

        var choice = result.Questions!.First().Choices!.First();
        choice.Should().BeOfType<ChoiceForTakeDto>();
    }

    [Fact]
    public async Task Handle_NonExistingQuiz_ShouldThrowException()
    {
        var repo = new Mock<IQuizRepository>();
        var id = Guid.NewGuid();
        repo.Setup(r => r.GetQuizWithQuestionsAsync(id)).ReturnsAsync((Quiz?)null);

        var handler = new GetQuizForTakeQueryHandler(repo.Object);
        var query = new GetQuizForTakeQuery(id);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(query, CancellationToken.None));
    }
}
