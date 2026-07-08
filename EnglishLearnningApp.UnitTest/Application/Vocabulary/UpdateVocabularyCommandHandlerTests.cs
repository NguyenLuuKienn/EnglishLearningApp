using EnglishLearning.Application.Features.Vocabulary.Commands.UpdateVocabulary;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using EnglishLearnningApp.UnitTest.Helpers;
using VocabularyEntity = EnglishLearning.Domain.Entities.Vocabulary;

namespace EnglishLearnningApp.UnitTest.Application.Vocabulary;

public class UpdateVocabularyCommandHandlerTests
{
    [Fact]
    public async Task Handle_ExistingVocabulary_ShouldUpdate()
    {
        var repo = new Mock<IVocabularyRepository>();
        var vocab = TestDataBuilder.CreateValidVocabulary();
        repo.Setup(r => r.GetByIdAsync(vocab.Id)).ReturnsAsync(vocab);

        var handler = new UpdateVocabularyCommandHandler(repo.Object);
        var command = new UpdateVocabularyCommand(vocab.Id, "NewWord", "New def", null, null, DifficultyLevel.Intermediate);

        await handler.Handle(command, CancellationToken.None);

        vocab.Word.Should().Be("NewWord");
        vocab.Difficulty.Should().Be(DifficultyLevel.Intermediate);
    }

    [Fact]
    public async Task Handle_NonExistingVocabulary_ShouldThrowException()
    {
        var repo = new Mock<IVocabularyRepository>();
        var id = Guid.NewGuid();
        repo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((VocabularyEntity?)null);

        var handler = new UpdateVocabularyCommandHandler(repo.Object);
        var command = new UpdateVocabularyCommand(id, "Word", "Def", null, null, DifficultyLevel.Beginner);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }
}
