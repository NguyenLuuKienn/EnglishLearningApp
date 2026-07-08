using System.Security.Claims;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Auth.Commands.RefreshToken;
using EnglishLearning.Application.Interfaces;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Domain.Enums;
using EnglishLearnningApp.UnitTest.Helpers;

namespace EnglishLearnningApp.UnitTest.Application.Auth;

public class RefreshTokenCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidTokens_ShouldReturnNewTokens()
    {
        var tokenService = new Mock<ITokenService>();
        var userRepo = new Mock<IUserRepository>();
        var uow = new Mock<IUnitOfWork>();

        var user = TestDataBuilder.CreateValidUser();
        user.RefreshToken = "old-refresh-token";
        user.RefreshTokenExpiry = DateTime.UtcNow.AddHours(1);

        var newTokens = new TokenDto
        {
            AccessToken = "new-access",
            RefreshToken = "new-refresh",
            ExpiresIn = 3600
        };

        var principal = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim("email", user.Email)
        }));

        tokenService.Setup(t => t.GetPrincipalFromExpiredToken("expired-access")).Returns(principal);
        userRepo.Setup(r => r.GetByEmailAsync(user.Email)).ReturnsAsync(user);
        tokenService.Setup(t => t.GenerateTokensAsync(user)).ReturnsAsync(newTokens);

        var handler = new RefreshTokenCommandHandler(tokenService.Object, userRepo.Object, uow.Object);
        var command = new RefreshTokenCommand("expired-access", "old-refresh-token");

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.AccessToken.Should().Be("new-access");
        result.RefreshToken.Should().Be("new-refresh");
        uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidAccessToken_ShouldThrowUnauthorizedAccessException()
    {
        var tokenService = new Mock<ITokenService>();
        var userRepo = new Mock<IUserRepository>();
        var uow = new Mock<IUnitOfWork>();

        tokenService.Setup(t => t.GetPrincipalFromExpiredToken("invalid")).Returns((ClaimsPrincipal?)null);

        var handler = new RefreshTokenCommandHandler(tokenService.Object, userRepo.Object, uow.Object);
        var command = new RefreshTokenCommand("invalid", "refresh");

        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_UserNotFound_ShouldThrowUnauthorizedAccessException()
    {
        var tokenService = new Mock<ITokenService>();
        var userRepo = new Mock<IUserRepository>();
        var uow = new Mock<IUnitOfWork>();

        var principal = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim("email", "missing@test.com")
        }));

        tokenService.Setup(t => t.GetPrincipalFromExpiredToken("access")).Returns(principal);
        userRepo.Setup(r => r.GetByEmailAsync("missing@test.com")).ReturnsAsync((User?)null);

        var handler = new RefreshTokenCommandHandler(tokenService.Object, userRepo.Object, uow.Object);
        var command = new RefreshTokenCommand("access", "refresh");

        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_InvalidRefreshToken_ShouldThrowUnauthorizedAccessException()
    {
        var tokenService = new Mock<ITokenService>();
        var userRepo = new Mock<IUserRepository>();
        var uow = new Mock<IUnitOfWork>();

        var user = TestDataBuilder.CreateValidUser();
        user.RefreshToken = "different-token";
        user.RefreshTokenExpiry = DateTime.UtcNow.AddHours(1);

        var principal = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim("email", user.Email)
        }));

        tokenService.Setup(t => t.GetPrincipalFromExpiredToken("access")).Returns(principal);
        userRepo.Setup(r => r.GetByEmailAsync(user.Email)).ReturnsAsync(user);

        var handler = new RefreshTokenCommandHandler(tokenService.Object, userRepo.Object, uow.Object);
        var command = new RefreshTokenCommand("access", "wrong-refresh");

        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ExpiredRefreshToken_ShouldThrowUnauthorizedAccessException()
    {
        var tokenService = new Mock<ITokenService>();
        var userRepo = new Mock<IUserRepository>();
        var uow = new Mock<IUnitOfWork>();

        var user = TestDataBuilder.CreateValidUser();
        user.RefreshToken = "refresh";
        user.RefreshTokenExpiry = DateTime.UtcNow.AddHours(-1);

        var principal = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim("email", user.Email)
        }));

        tokenService.Setup(t => t.GetPrincipalFromExpiredToken("access")).Returns(principal);
        userRepo.Setup(r => r.GetByEmailAsync(user.Email)).ReturnsAsync(user);

        var handler = new RefreshTokenCommandHandler(tokenService.Object, userRepo.Object, uow.Object);
        var command = new RefreshTokenCommand("access", "refresh");

        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => handler.Handle(command, CancellationToken.None));
    }
}
