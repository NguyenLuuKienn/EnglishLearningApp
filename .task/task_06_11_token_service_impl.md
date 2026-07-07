# Task 6.11: TokenService Implementation

## Description

Create TokenService implementation in Infrastructure layer for JWT token generation and validation.

## Priority
🔴 Critical — JWT token management

## Dependencies
- Task 6.5 (ITokenService interface)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Infrastructure/Services/TokenService.cs` | Create |

## Steps

### Step 1: Create TokenService
1. Implement `ITokenService`
2. Inject `IConfiguration`
3. Generate JWT with claims (email, role, name)
4. Generate random refresh token
5. Extract claims from expired token

## Expected Code

```csharp
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Interfaces;
using EnglishLearning.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace EnglishLearning.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<TokenDto> GenerateTokensAsync(User user)
    {
        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();

        return new TokenDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = 3600 // 1 hour
        };
    }

    public async Task<string> GenerateNewRefreshTokenAsync()
    {
        return GenerateRefreshToken();
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "default-key")),
            ValidateLifetime = false, // Expired token
            ValidIssuer = _configuration["Jwt:Issuer"],
            ValidAudience = _configuration["Jwt:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            return null;

        return principal;
    }

    private string GenerateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role.ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "default-key"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Infrastructure` — 0 errors ✅
- [x] TokenService implements ITokenService ✅
- [x] JWT token generated with claims ✅

## Acceptance Criteria

- [x] `TokenService` implements `ITokenService` ✅
- [x] `GenerateTokensAsync` creates JWT with Name, Email, Role, NameIdentifier claims ✅
- [x] AccessToken expires in 1 hour ✅
- [x] RefreshToken is random 32-byte Base64 string ✅
- [x] `GetPrincipalFromExpiredToken` validates and extracts claims ✅
- [x] Infrastructure project builds successfully ✅

---

## ✅ Completed: 2026-07-07

- **TokenService** — Primary constructor with `IConfiguration`, generates JWT (Name, Email, Role, NameIdentifier claims), 1-hour expiry, 32-byte random refresh token
- **NuGet packages added** — `System.IdentityModel.Tokens.Jwt` 7.6.2, `Microsoft.IdentityModel.Tokens` 7.6.2
- **DependencyInjection** — Registered `ITokenService → TokenService` (Scoped)
- Build verified: 0 errors
