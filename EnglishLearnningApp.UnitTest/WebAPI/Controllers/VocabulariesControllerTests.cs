using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Vocabulary.Queries.GetVocabularies;
using EnglishLearning.Domain.Enums;
using EnglishLearning.WebAPI.Controllers;
using EnglishLearning.WebAPI.Models.Common;
using EnglishLearning.WebAPI.Models.Requests.Vocabulary;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearnningApp.UnitTest.WebAPI.Controllers;

public class VocabulariesControllerTests
{
    [Fact]
    public async Task Create_ValidRequest_ShouldReturnCreated()
    {
        var mediator = new Mock<IMediator>();
        var vocabId = Guid.NewGuid();
        mediator.Setup(m => m.Send(It.IsAny<EnglishLearning.Application.Features.Vocabulary.Commands.CreateVocabulary.CreateVocabularyCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(vocabId);

        var controller = new VocabulariesController(mediator.Object);
        var request = new CreateVocabularyRequest
        {
            Word = "Hello",
            Definition = "A greeting",
            Difficulty = DifficultyLevel.Beginner
        };

        var result = await controller.Create(request);

        var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
        createdResult.StatusCode.Should().Be(201);
        var returnedId = createdResult.Value.Should().BeOfType<Guid>().Subject;
        returnedId.Should().Be(vocabId);
    }

    [Fact]
    public async Task GetVocabularies_ShouldReturnPagedResponse()
    {
        var mediator = new Mock<IMediator>();
        var items = new List<VocabularyDto>
        {
            new() { Id = Guid.NewGuid(), Word = "Hello" },
            new() { Id = Guid.NewGuid(), Word = "World" }
        };
        var pagedResult = PagedResult<VocabularyDto>.Create(items, 1, 10, 2);
        mediator.Setup(m => m.Send(It.IsAny<GetVocabulariesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(pagedResult);

        var controller = new VocabulariesController(mediator.Object);

        var result = await controller.GetAll(1, 10, null);

        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var response = okResult.Value.Should().BeOfType<PagedResponse<VocabularyDto>>().Subject;
        response.Data.Should().HaveCount(2);
        response.PageNumber.Should().Be(1);
    }

    [Fact]
    public async Task GetById_ExistingVocabulary_ShouldReturnOk()
    {
        var mediator = new Mock<IMediator>();
        var vocabId = Guid.NewGuid();
        var vocabDto = new VocabularyDto { Id = vocabId, Word = "Hello" };
        mediator.Setup(m => m.Send(It.IsAny<EnglishLearning.Application.Features.Vocabulary.Queries.GetVocabulary.GetVocabularyQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(vocabDto);

        var controller = new VocabulariesController(mediator.Object);

        var result = await controller.GetById(vocabId);

        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var response = okResult.Value.Should().BeOfType<ApiResponse<VocabularyDto>>().Subject;
        response.Data!.Word.Should().Be("Hello");
    }

    [Fact]
    public async Task Delete_ExistingVocabulary_ShouldReturnNoContent()
    {
        var mediator = new Mock<IMediator>();
        var vocabId = Guid.NewGuid();
        mediator.Setup(m => m.Send(It.IsAny<EnglishLearning.Application.Features.Vocabulary.Commands.DeleteVocabulary.DeleteVocabularyCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var controller = new VocabulariesController(mediator.Object);

        var result = await controller.Delete(vocabId);

        result.Should().BeOfType<NoContentResult>();
    }
}
