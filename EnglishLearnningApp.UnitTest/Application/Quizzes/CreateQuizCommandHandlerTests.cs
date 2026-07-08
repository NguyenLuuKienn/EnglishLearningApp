using EnglishLearning.Application.Features.Quizzes.Commands.CreateQuiz;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearnningApp.UnitTest.Application.Quizzes;

public class CreateQuizCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidCommand_ShouldCreateQuizWithQuestions()
    {
        var repo = new Mock<IQuizRepository>();
        var handler = new CreateQuizCommandHandler(repo.Object);

        var command = new CreateQuizCommand(
            "Test Quiz", "Description", DifficultyLevel.Beginner, 30, 50m,
            new List<QuestionCommand>
            {
                new("What is 1+1?", QuestionType.MultipleChoice, DifficultyLevel.Beginner, "2",
                    new List<ChoiceCommand> { new("2", true), new("3", false) })
            });

        await handler.Handle(command, CancellationToken.None);

        repo.Verify(r => r.AddAsync(It.IsAny<EnglishLearning.Domain.Entities.Quiz>()), Times.Once);
        repo.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}
