using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.History.Queries.GetUserHistory;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using AutoMapper;
using LearningHistoryEntity = EnglishLearning.Domain.Entities.LearningHistory;

namespace EnglishLearnningApp.UnitTest.Application.History;

public class GetUserHistoryQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnPagedHistory()
    {
        var repo = new Mock<ILearningHistoryRepository>();
        var mapper = new Mock<IMapper>();

        var histories = new List<LearningHistoryEntity>
        {
            new LearningHistoryEntity
            {
                UserId = "user",
                ActionType = ActionType.CompleteQuiz,
                TargetId = Guid.NewGuid(),
                Details = "test",
                Score = 80m
            }
        };
        var dtos = new List<LearningHistoryDto> { new() };
        var paged = (Items: histories, TotalRecords: 1);

        repo.Setup(r => r.GetByUserIdAsync("user", 1, 10)).ReturnsAsync(paged);
        mapper.Setup(m => m.Map<List<LearningHistoryDto>>(histories)).Returns(dtos);

        var handler = new GetUserHistoryQueryHandler(repo.Object, mapper.Object);
        var query = new GetUserHistoryQuery("user", 1, 10);

        var result = await handler.Handle(query, CancellationToken.None);
        result.Should().NotBeNull();
        result.Items.Should().HaveCount(1);
    }
}
