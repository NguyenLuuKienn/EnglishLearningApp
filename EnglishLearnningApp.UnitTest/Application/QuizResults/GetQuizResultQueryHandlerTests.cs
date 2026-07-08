using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.QuizResults.Queries.GetQuizResult;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using AutoMapper;

namespace EnglishLearnningApp.UnitTest.Application.QuizResults;

public class GetQuizResultQueryHandlerTests
{
    [Fact]
    public async Task Handle_ExistingResult_ShouldReturnDto()
    {
        var repo = new Mock<IQuizResultRepository>();
        var mapper = new Mock<IMapper>();

        var totalQuestions = 10;
        var correctAnswers = 7;
        var score = totalQuestions > 0 ? (decimal)Math.Round((correctAnswers / (double)totalQuestions) * 100, 2) : 0m;
        var result = new QuizResult
        {
            QuizId = Guid.NewGuid(),
            UserId = "user",
            TotalQuestions = totalQuestions,
            CorrectAnswers = correctAnswers,
            DurationMinutes = 15,
            Score = score
        };
        var dto = new QuizResultDto { Id = result.Id, Score = result.Score };

        repo.Setup(r => r.GetByIdAsync(result.Id)).ReturnsAsync(result);
        mapper.Setup(m => m.Map<QuizResultDto>(result)).Returns(dto);

        var handler = new GetQuizResultQueryHandler(repo.Object, mapper.Object);
        var query = new GetQuizResultQuery(result.Id);

        var returned = await handler.Handle(query, CancellationToken.None);
        returned.Should().NotBeNull();
        returned.Score.Should().Be(result.Score);
    }

    [Fact]
    public async Task Handle_NonExistingResult_ShouldThrowException()
    {
        var repo = new Mock<IQuizResultRepository>();
        var mapper = new Mock<IMapper>();

        var id = Guid.NewGuid();
        repo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((QuizResult?)null);

        var handler = new GetQuizResultQueryHandler(repo.Object, mapper.Object);
        var query = new GetQuizResultQuery(id);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(query, CancellationToken.None));
    }
}
