using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.QuizResults.Queries.GetUserQuizResults;
using EnglishLearning.Domain.Interfaces;
using AutoMapper;
using System.Linq.Expressions;
using QuizResultEntity = EnglishLearning.Domain.Entities.QuizResult;

namespace EnglishLearnningApp.UnitTest.Application.QuizResults;

public class GetUserQuizResultsQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnPagedResults()
    {
        var repo = new Mock<IQuizResultRepository>();
        var mapper = new Mock<IMapper>();

        var totalQuestions = 10;
        var correctAnswers = 7;
        var score = totalQuestions > 0 ? (decimal)Math.Round((correctAnswers / (double)totalQuestions) * 100, 2) : 0m;
        var results = new List<QuizResultEntity>
        {
            new QuizResultEntity
            {
                QuizId = Guid.NewGuid(),
                UserId = "user",
                TotalQuestions = totalQuestions,
                CorrectAnswers = correctAnswers,
                DurationMinutes = 15,
                Score = score
            }
        };
        var dtos = new List<QuizResultDto> { new() { Score = 70m } };
        var paged = (Items: (IReadOnlyList<QuizResultEntity>)results, TotalRecords: 1);

        repo.Setup(r => r.GetPagedAsync(1, 10, It.IsAny<Expression<Func<QuizResultEntity, bool>>>(), It.IsAny<Expression<Func<QuizResultEntity, object>>>(), false))
            .ReturnsAsync(paged);
        mapper.Setup(m => m.Map<List<QuizResultDto>>(results)).Returns(dtos);

        var handler = new GetUserQuizResultsQueryHandler(repo.Object, mapper.Object);
        var query = new GetUserQuizResultsQuery("user", 1, 10);

        var result = await handler.Handle(query, CancellationToken.None);
        result.Should().NotBeNull();
        result.Items.Should().HaveCount(1);
    }
}
