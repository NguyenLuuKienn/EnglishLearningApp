using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Auth.Queries.GetProfile;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Domain.Enums;
using AutoMapper;

namespace EnglishLearnningApp.UnitTest.Application.Auth;

public class GetProfileQueryHandlerTests
{
    [Fact]
    public async Task Handle_UserExists_ShouldReturnUserDto()
    {
        var userRepo = new Mock<IUserRepository>();
        var mapper = new Mock<IMapper>();

        var userId = Guid.NewGuid();
        var user = new User
        {
            Username = "testuser",
            Email = "test@test.com",
            PasswordHash = "hash",
            Role = UserRole.Student,
            IsActive = true
        };
        var userDto = new UserDto { Id = userId, Username = "testuser", Email = "test@example.com" };

        userRepo.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);
        mapper.Setup(m => m.Map<UserDto>(user)).Returns(userDto);

        var handler = new GetProfileQueryHandler(userRepo.Object, mapper.Object);
        var query = new GetProfileQuery(userId);

        var result = await handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Username.Should().Be("testuser");
    }

    [Fact]
    public async Task Handle_UserNotFound_ShouldThrowException()
    {
        var userRepo = new Mock<IUserRepository>();
        var mapper = new Mock<IMapper>();

        var userId = Guid.NewGuid();
        userRepo.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync((User?)null);

        var handler = new GetProfileQueryHandler(userRepo.Object, mapper.Object);
        var query = new GetProfileQuery(userId);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(query, CancellationToken.None));
    }
}
