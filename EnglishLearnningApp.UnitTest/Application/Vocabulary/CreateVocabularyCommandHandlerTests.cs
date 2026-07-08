using EnglishLearning.Application.Features.Vocabulary.Commands.CreateVocabulary;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using VocabularyEntity = EnglishLearning.Domain.Entities.Vocabulary;

namespace EnglishLearnningApp.UnitTest.Application.Vocabulary;

public class CreateVocabularyCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidCommand_ShouldCreateVocabulary()
    {
        var repo = new Mock<IVocabularyRepository>();
        var handler = new CreateVocabularyCommandHandler(repo.Object);

        var command = new CreateVocabularyCommand("Hello", "A greeting", "Hello world", "Interjection", DifficultyLevel.Beginner);
        await handler.Handle(command, CancellationToken.None);

        repo.Verify(r => r.AddAsync(It.IsAny<VocabularyEntity>()), Times.Once);
        repo.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}
