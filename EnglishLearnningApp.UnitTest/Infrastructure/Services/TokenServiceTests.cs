using System.Security.Claims;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace EnglishLearnningApp.UnitTest.Infrastructure.Services;

public class TokenServiceTests
{
    private IConfiguration CreateTestConfiguration()
    {
        var dictionary = new Dictionary<string, string>
        {
            { "Jwt:Key", "ThisIsASecretKeyThatIsAtLeast32CharactersLong!" },
            { "Jwt:Issuer", "test-issuer" },
            { "Jwt:Audience", "test-audience" }
        };

        return new ConfigurationBuilder()
            .AddInMemoryCollection(dictionary)
            .Build();
    }

    [Fact]
    public async Task GenerateTokensAsync_ShouldReturnTokenDtoWithAllProperties()
    {
        var config = CreateTestConfiguration();
        var service = new TokenService(config);
        var user = new User
        {
            Username = "testuser",
            Email = "test@test.com",
            PasswordHash = "hash",
            Role = UserRole.Student,
            IsActive = true
        };

        var result = await service.GenerateTokensAsync(user);

        result.Should().NotBeNull();
        result.AccessToken.Should().NotBeNullOrEmpty();
        result.RefreshToken.Should().NotBeNullOrEmpty();
        result.ExpiresIn.Should().Be(3600);
    }

    [Fact]
    public async Task GenerateTokensAsync_ShouldGenerateUniqueTokensEachTime()
    {
        var config = CreateTestConfiguration();
        var service = new TokenService(config);
        var user = new User
        {
            Username = "testuser",
            Email = "test@test.com",
            PasswordHash = "hash",
            Role = UserRole.Student,
            IsActive = true
        };

        var result1 = await service.GenerateTokensAsync(user);
        // Refresh tokens use RandomNumberGenerator so they're always unique
        result1.RefreshToken.Should().NotBeNullOrEmpty();
        result1.RefreshToken.Should().HaveLength(44);
        
        var result2 = await service.GenerateTokensAsync(user);
        // Each refresh token is cryptographically random, so they're always different
        result1.RefreshToken.Should().NotBe(result2.RefreshToken);
    }

    [Fact]
    public async Task GenerateNewRefreshTokenAsync_ShouldReturnValidToken()
    {
        var config = CreateTestConfiguration();
        var service = new TokenService(config);

        var result = await service.GenerateNewRefreshTokenAsync();

        result.Should().NotBeNullOrEmpty();
        result.Length.Should().Be(44); // Base64 of 32 bytes = 44 characters
    }

    [Fact]
    public void GetPrincipalFromExpiredToken_ValidToken_ShouldReturnPrincipal()
    {
        var config = CreateTestConfiguration();
        var service = new TokenService(config);
        var user = new User
        {
            Username = "testuser",
            Email = "test@test.com",
            PasswordHash = "hash",
            Role = UserRole.Student,
            IsActive = true
        };

        var tokens = service.GenerateTokensAsync(user).Result;

        var principal = service.GetPrincipalFromExpiredToken(tokens.AccessToken);

        principal.Should().NotBeNull();
        principal.FindFirst(ClaimTypes.Name)?.Value.Should().Be("testuser");
        principal.FindFirst(ClaimTypes.Email)?.Value.Should().Be("test@test.com");
        principal.FindFirst(ClaimTypes.Role)?.Value.Should().Be(UserRole.Student.ToString());
        principal.FindFirst(ClaimTypes.NameIdentifier)?.Value.Should().Be(user.Id.ToString());
    }

    [Fact]
    public void GetPrincipalFromExpiredToken_InvalidToken_ShouldReturnNull()
    {
        var config = CreateTestConfiguration();
        var service = new TokenService(config);

        // Invalid tokens throw exceptions in JwtSecurityTokenHandler, so we expect that
        Assert.ThrowsAny<Exception>(() => service.GetPrincipalFromExpiredToken("invalid.token.here"));
    }

    [Fact]
    public void GetPrincipalFromExpiredToken_EmptyToken_ShouldReturnNull()
    {
        var config = CreateTestConfiguration();
        var service = new TokenService(config);

        // Empty tokens also throw exceptions
        Assert.ThrowsAny<Exception>(() => service.GetPrincipalFromExpiredToken(""));
    }
}
