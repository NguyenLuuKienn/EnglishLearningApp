using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Auth.Commands.Login;
using EnglishLearning.Application.Interfaces;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using EnglishLearnningApp.UnitTest.Helpers;

namespace EnglishLearnningApp.UnitTest.Application.Auth;

public class LoginCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidCredentials_ShouldReturnTokens()
    {
        var userRepo = new Mock<IUserRepository>();
        var tokenService = new Mock<ITokenService>();
        var uow = new Mock<IUnitOfWork>();

        var user = TestDataBuilder.CreateValidUser();
        var tokenDto = new TokenDto { AccessToken = "access", RefreshToken = "refresh", ExpiresIn = 3600 };

        userRepo.Setup(r => r.GetByUsernameAsync("testuser")).ReturnsAsync(user);
        tokenService.Setup(t => t.GenerateTokensAsync(It.IsAny<User>())).ReturnsAsync(tokenDto);

        var handler = new LoginCommandHandler(userRepo.Object, tokenService.Object, uow.Object);
        var command = new LoginCommand("testuser", "Password123!");

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.AccessToken.Should().Be("access");
    }

    [Fact]
    public async Task Handle_UserNotFound_ShouldThrowException()
    {
        var userRepo = new Mock<IUserRepository>();
        var tokenService = new Mock<ITokenService>();
        var uow = new Mock<IUnitOfWork>();

        userRepo.Setup(r => r.GetByUsernameAsync("nonexistent")).ReturnsAsync((User?)null);

        var handler = new LoginCommandHandler(userRepo.Object, tokenService.Object, uow.Object);
        var command = new LoginCommand("nonexistent", "password");

        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_DeactivatedUser_ShouldThrowException()
    {
        var userRepo = new Mock<IUserRepository>();
        var tokenService = new Mock<ITokenService>();
        var uow = new Mock<IUnitOfWork>();

        var user = TestDataBuilder.CreateValidUser();
        user.IsActive = false;
        userRepo.Setup(r => r.GetByUsernameAsync("testuser")).ReturnsAsync(user);

        var handler = new LoginCommandHandler(userRepo.Object, tokenService.Object, uow.Object);
        var command = new LoginCommand("testuser", "Password123!");

        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));
    }
}
