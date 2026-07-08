using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Quizzes.Queries.GetQuestion;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using EnglishLearnningApp.UnitTest.Helpers;
using AutoMapper;

namespace EnglishLearnningApp.UnitTest.Application.Quizzes;

public class GetQuestionQueryHandlerTests
{
    [Fact]
    public async Task Handle_ExistingQuestion_ShouldReturnDto()
    {
        var repo = new Mock<IQuizRepository>();
        var mapper = new Mock<IMapper>();

        var quiz = TestDataBuilder.CreateQuizWithQuestions(1);
        var question = quiz.Questions.First();
        var dto = new QuestionDto { Id = question.Id, QuestionText = question.QuestionText };

        repo.Setup(r => r.GetQuizWithQuestionsAsync(quiz.Id)).ReturnsAsync(quiz);
        mapper.Setup(m => m.Map<QuestionDto>(question)).Returns(dto);

        var handler = new GetQuestionQueryHandler(repo.Object, mapper.Object);
        var query = new GetQuestionQuery(quiz.Id, question.Id);

        var result = await handler.Handle(query, CancellationToken.None);
        result.Should().NotBeNull();
        result.Id.Should().Be(question.Id);
    }

    [Fact]
    public async Task Handle_QuestionNotInQuiz_ShouldThrowException()
    {
        var repo = new Mock<IQuizRepository>();
        var mapper = new Mock<IMapper>();

        var quiz = TestDataBuilder.CreateQuizWithQuestions(1);
        repo.Setup(r => r.GetQuizWithQuestionsAsync(quiz.Id)).ReturnsAsync(quiz);

        var handler = new GetQuestionQueryHandler(repo.Object, mapper.Object);
        var query = new GetQuestionQuery(quiz.Id, Guid.NewGuid());

        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(query, CancellationToken.None));
    }
}
