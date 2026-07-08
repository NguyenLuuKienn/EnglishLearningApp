using EnglishLearning.Application.Features.Vocabulary.Commands.DeleteVocabulary;
using EnglishLearning.Domain.Interfaces;
using EnglishLearnningApp.UnitTest.Helpers;
using Vocab = EnglishLearning.Domain.Entities.Vocabulary;

namespace EnglishLearnningApp.UnitTest.Application.Vocabulary;

public class DeleteVocabularyCommandHandlerTests
{
    [Fact]
    public async Task Handle_ExistingVocabulary_ShouldDelete()
    {
        var repo = new Mock<IVocabularyRepository>();
        var vocab = TestDataBuilder.CreateValidVocabulary();
        repo.Setup(r => r.GetByIdAsync(vocab.Id)).ReturnsAsync(vocab);

        var handler = new DeleteVocabularyCommandHandler(repo.Object);
        var command = new DeleteVocabularyCommand(vocab.Id);

        await handler.Handle(command, CancellationToken.None);

        repo.Verify(r => r.Delete(vocab), Times.Once);
    }

    [Fact]
    public async Task Handle_NonExistingVocabulary_ShouldThrowException()
    {
        var repo = new Mock<IVocabularyRepository>();
        var id = Guid.NewGuid();
        repo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Vocab?)null);

        var handler = new DeleteVocabularyCommandHandler(repo.Object);
        var command = new DeleteVocabularyCommand(id);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }
}
