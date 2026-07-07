using System.Security.Claims;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Entities;

namespace EnglishLearning.Application.Interfaces;

public interface ITokenService
{
    Task<TokenDto> GenerateTokensAsync(User user);
    Task<string> GenerateNewRefreshTokenAsync();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}
