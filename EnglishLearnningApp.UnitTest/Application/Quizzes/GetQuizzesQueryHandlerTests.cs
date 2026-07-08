using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Quizzes.Queries.GetQuizzes;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using EnglishLearnningApp.UnitTest.Helpers;
using AutoMapper;
using System.Linq.Expressions;
using QuizEntity = EnglishLearning.Domain.Entities.Quiz;

namespace EnglishLearnningApp.UnitTest.Application.Quizzes;

public class GetQuizzesQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnPagedResults()
    {
        var repo = new Mock<IQuizRepository>();
        var mapper = new Mock<IMapper>();

        var quizzes = new List<QuizEntity> { TestDataBuilder.CreateValidQuiz("Quiz 1") };
        var dtos = new List<QuizDto> { new() { Title = "Quiz 1" } };
        var paged = (Items: (IReadOnlyList<QuizEntity>)quizzes, TotalRecords: 1);

        repo.Setup(r => r.GetPagedAsync(1, 10, null, It.IsAny<Expression<Func<QuizEntity, object>>>(), false)).ReturnsAsync(paged);
        mapper.Setup(m => m.Map<List<QuizDto>>(quizzes)).Returns(dtos);

        var handler = new GetQuizzesQueryHandler(repo.Object, mapper.Object);
        var query = new GetQuizzesQuery(1, 10, null);

        var result = await handler.Handle(query, CancellationToken.None);
        result.Should().NotBeNull();
        result.Items.Should().HaveCount(1);
    }

    [Fact]
    public async Task Handle_WithDifficulty_ShouldFilter()
    {
        var repo = new Mock<IQuizRepository>();
        var mapper = new Mock<IMapper>();

        var quizzes = new List<QuizEntity> { TestDataBuilder.CreateValidQuiz() };
        var dtos = new List<QuizDto>();
        var paged = (Items: (IReadOnlyList<QuizEntity>)quizzes, TotalRecords: 1);

        repo.Setup(r => r.GetPagedAsync(1, 10, It.IsAny<Expression<Func<QuizEntity, bool>>>(), It.IsAny<Expression<Func<QuizEntity, object>>>(), false))
            .ReturnsAsync(paged);
        mapper.Setup(m => m.Map<List<QuizDto>>(quizzes)).Returns(dtos);

        var handler = new GetQuizzesQueryHandler(repo.Object, mapper.Object);
        var query = new GetQuizzesQuery(1, 10, DifficultyLevel.Beginner);

        await handler.Handle(query, CancellationToken.None);

        repo.Verify(r => r.GetPagedAsync(1, 10, It.IsAny<Expression<Func<QuizEntity, bool>>>(), It.IsAny<Expression<Func<QuizEntity, object>>>(), false), Times.Once);
    }
}
