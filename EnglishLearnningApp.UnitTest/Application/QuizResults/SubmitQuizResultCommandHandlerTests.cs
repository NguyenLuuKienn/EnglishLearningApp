using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.QuizResults.Commands.SubmitQuizResult;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using EnglishLearnningApp.UnitTest.Helpers;
using AutoMapper;
using MediatR;
using QuizEntity = EnglishLearning.Domain.Entities.Quiz;
using QuizResultEntity = EnglishLearning.Domain.Entities.QuizResult;

namespace EnglishLearnningApp.UnitTest.Application.QuizResults;

public class SubmitQuizResultCommandHandlerTests
{
    [Fact]
    public async Task Handle_AllCorrectAnswers_ShouldReturn100Score()
    {
        var quizRepo = new Mock<IQuizRepository>();
        var resultRepo = new Mock<IQuizResultRepository>();
        var mapper = new Mock<IMapper>();
        var mediator = new Mock<IMediator>();

        var quiz = TestDataBuilder.CreateQuizWithQuestions(2);
        var questions = quiz.Questions.ToList();
        var correctChoice1 = questions[0].Choices.First(c => c.IsCorrect);
        var correctChoice2 = questions[1].Choices.First(c => c.IsCorrect);

        var command = new SubmitQuizResultCommand(
            quiz.Id, "user-123", 15,
            new List<AnswerCommand>
            {
                new(questions[0].Id, correctChoice1.Id, null),
                new(questions[1].Id, correctChoice2.Id, null)
            });

        var resultDto = new QuizResultDto { Score = 100m, CorrectAnswers = 2 };

        quizRepo.Setup(r => r.GetQuizWithQuestionsAsync(quiz.Id)).ReturnsAsync(quiz);
        mapper.Setup(m => m.Map<QuizResultDto>(It.IsAny<QuizResultEntity>())).Returns(resultDto);

        var handler = new SubmitQuizResultCommandHandler(quizRepo.Object, resultRepo.Object, mapper.Object, mediator.Object);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Score.Should().Be(100m);
    }

    [Fact]
    public async Task Handle_AllWrongAnswers_ShouldReturn0Score()
    {
        var quizRepo = new Mock<IQuizRepository>();
        var resultRepo = new Mock<IQuizResultRepository>();
        var mapper = new Mock<IMapper>();
        var mediator = new Mock<IMediator>();

        var quiz = TestDataBuilder.CreateQuizWithQuestions(2);
        var questions = quiz.Questions.ToList();
        var wrongChoice1 = questions[0].Choices.First(c => !c.IsCorrect);
        var wrongChoice2 = questions[1].Choices.First(c => !c.IsCorrect);

        var command = new SubmitQuizResultCommand(
            quiz.Id, "user-123", 15,
            new List<AnswerCommand>
            {
                new(questions[0].Id, wrongChoice1.Id, null),
                new(questions[1].Id, wrongChoice2.Id, null)
            });

        var resultDto = new QuizResultDto { Score = 0m, CorrectAnswers = 0 };

        quizRepo.Setup(r => r.GetQuizWithQuestionsAsync(quiz.Id)).ReturnsAsync(quiz);
        mapper.Setup(m => m.Map<QuizResultDto>(It.IsAny<QuizResultEntity>())).Returns(resultDto);

        var handler = new SubmitQuizResultCommandHandler(quizRepo.Object, resultRepo.Object, mapper.Object, mediator.Object);
        var result = await handler.Handle(command, CancellationToken.None);

        result.Score.Should().Be(0m);
    }

    [Fact]
    public async Task Handle_QuizNotFound_ShouldThrowException()
    {
        var quizRepo = new Mock<IQuizRepository>();
        var resultRepo = new Mock<IQuizResultRepository>();
        var mapper = new Mock<IMapper>();
        var mediator = new Mock<IMediator>();

        var quizId = Guid.NewGuid();
        quizRepo.Setup(r => r.GetQuizWithQuestionsAsync(quizId)).ReturnsAsync((QuizEntity?)null);

        var command = new SubmitQuizResultCommand(quizId, "user", 15, new List<AnswerCommand>());

        var handler = new SubmitQuizResultCommandHandler(quizRepo.Object, resultRepo.Object, mapper.Object, mediator.Object);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }
}
