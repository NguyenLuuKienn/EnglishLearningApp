using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Vocabulary.Queries.GetVocabularies;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using EnglishLearnningApp.UnitTest.Helpers;
using AutoMapper;
using System.Linq.Expressions;
using Vocab = EnglishLearning.Domain.Entities.Vocabulary;

namespace EnglishLearnningApp.UnitTest.Application.Vocabulary;

public class GetVocabulariesQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnPagedResults()
    {
        var repo = new Mock<IVocabularyRepository>();
        var mapper = new Mock<IMapper>();

        var vocabs = new List<Vocab>
        {
            TestDataBuilder.CreateValidVocabulary("Hello"),
            TestDataBuilder.CreateValidVocabulary("World")
        };
        var dtos = new List<VocabularyDto> { new() { Word = "Hello" }, new() { Word = "World" } };
        var paged = (Items: (IReadOnlyList<Vocab>)vocabs, TotalRecords: 2);

        repo.Setup(r => r.GetPagedAsync(1, 10, null, It.IsAny<Expression<Func<Vocab, object>>>(), false)).ReturnsAsync(paged);
        mapper.Setup(m => m.Map<List<VocabularyDto>>(vocabs)).Returns(dtos);

        var handler = new GetVocabulariesQueryHandler(repo.Object, mapper.Object);
        var query = new GetVocabulariesQuery(1, 10, null);

        var result = await handler.Handle(query, CancellationToken.None);
        result.Should().NotBeNull();
        result.Items.Should().HaveCount(2);
    }

    [Fact]
    public async Task Handle_WithDifficultyFilter_ShouldPassPredicate()
    {
        var repo = new Mock<IVocabularyRepository>();
        var mapper = new Mock<IMapper>();

        var vocabs = new List<Vocab> { TestDataBuilder.CreateValidVocabulary() };
        var dtos = new List<VocabularyDto> { new() { Word = "Hello" } };
        var paged = (Items: (IReadOnlyList<Vocab>)vocabs, TotalRecords: 1);

        repo.Setup(r => r.GetPagedAsync(1, 10, It.IsAny<Expression<Func<Vocab, bool>>>(), It.IsAny<Expression<Func<Vocab, object>>>(), false))
            .ReturnsAsync(paged);
        mapper.Setup(m => m.Map<List<VocabularyDto>>(vocabs)).Returns(dtos);

        var handler = new GetVocabulariesQueryHandler(repo.Object, mapper.Object);
        var query = new GetVocabulariesQuery(1, 10, DifficultyLevel.Beginner);

        await handler.Handle(query, CancellationToken.None);

        repo.Verify(r => r.GetPagedAsync(1, 10, It.IsAny<Expression<Func<Vocab, bool>>>(), It.IsAny<Expression<Func<Vocab, object>>>(), false), Times.Once);
    }
}
