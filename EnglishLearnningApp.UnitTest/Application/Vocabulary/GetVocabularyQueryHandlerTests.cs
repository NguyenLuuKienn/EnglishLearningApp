using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Vocabulary.Queries.GetVocabulary;
using EnglishLearning.Domain.Interfaces;
using EnglishLearnningApp.UnitTest.Helpers;
using AutoMapper;
using VocabularyEntity = EnglishLearning.Domain.Entities.Vocabulary;

namespace EnglishLearnningApp.UnitTest.Application.Vocabulary;

public class GetVocabularyQueryHandlerTests
{
    [Fact]
    public async Task Handle_ExistingVocabulary_ShouldReturnDto()
    {
        var repo = new Mock<IVocabularyRepository>();
        var mapper = new Mock<IMapper>();

        var vocab = TestDataBuilder.CreateValidVocabulary();
        var dto = new VocabularyDto { Id = vocab.Id, Word = "Hello" };

        repo.Setup(r => r.GetByIdAsync(vocab.Id)).ReturnsAsync(vocab);
        mapper.Setup(m => m.Map<VocabularyDto>(vocab)).Returns(dto);

        var handler = new GetVocabularyQueryHandler(repo.Object, mapper.Object);
        var query = new GetVocabularyQuery(vocab.Id);

        var result = await handler.Handle(query, CancellationToken.None);
        result.Should().NotBeNull();
        result.Word.Should().Be("Hello");
    }

    [Fact]
    public async Task Handle_NonExistingVocabulary_ShouldThrowException()
    {
        var repo = new Mock<IVocabularyRepository>();
        var mapper = new Mock<IMapper>();

        var id = Guid.NewGuid();
        repo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((VocabularyEntity?)null);

        var handler = new GetVocabularyQueryHandler(repo.Object, mapper.Object);
        var query = new GetVocabularyQuery(id);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(query, CancellationToken.None));
    }
}
