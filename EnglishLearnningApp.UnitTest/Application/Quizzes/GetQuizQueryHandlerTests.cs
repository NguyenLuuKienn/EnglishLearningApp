using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Quizzes.Queries.GetQuiz;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using EnglishLearnningApp.UnitTest.Helpers;
using AutoMapper;

namespace EnglishLearnningApp.UnitTest.Application.Quizzes;

public class GetQuizQueryHandlerTests
{
    [Fact]
    public async Task Handle_ExistingQuiz_ShouldReturnDto()
    {
        var repo = new Mock<IQuizRepository>();
        var mapper = new Mock<IMapper>();

        var quiz = TestDataBuilder.CreateQuizWithQuestions(2);
        var dto = new QuizDto { Id = quiz.Id, Title = quiz.Title };

        repo.Setup(r => r.GetQuizWithQuestionsAsync(quiz.Id)).ReturnsAsync(quiz);
        mapper.Setup(m => m.Map<QuizDto>(quiz)).Returns(dto);

        var handler = new GetQuizQueryHandler(repo.Object, mapper.Object);
        var query = new GetQuizQuery(quiz.Id);

        var result = await handler.Handle(query, CancellationToken.None);
        result.Should().NotBeNull();
        result.Id.Should().Be(quiz.Id);
    }

    [Fact]
    public async Task Handle_NonExistingQuiz_ShouldThrowException()
    {
        var repo = new Mock<IQuizRepository>();
        var mapper = new Mock<IMapper>();

        var id = Guid.NewGuid();
        repo.Setup(r => r.GetQuizWithQuestionsAsync(id)).ReturnsAsync((Quiz?)null);

        var handler = new GetQuizQueryHandler(repo.Object, mapper.Object);
        var query = new GetQuizQuery(id);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(query, CancellationToken.None));
    }
}
