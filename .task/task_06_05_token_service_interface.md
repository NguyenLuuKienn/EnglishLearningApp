# Task 6.5: JWT Token Service Interface

## Description

Create the ITokenService interface in the Application layer for JWT token generation and validation.

## Priority
🔴 Critical — Required for authentication

## Dependencies
- Task 6.4 (Auth DTOs)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/Interfaces/ITokenService.cs` | Create |

## Steps

### Step 1: Create ITokenService interface
1. Methods:
   - `Task<TokenDto> GenerateTokensAsync(User user)`
   - `Task<string> GenerateNewRefreshTokenAsync()`
   - `ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)`

## Expected Code

```csharp
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
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Application` — 0 errors
- [ ] ITokenService interface has required methods

## Acceptance Criteria

- [ ] `ITokenService` interface in `EnglishLearning.Application.Interfaces` namespace
- [ ] `GenerateTokensAsync(User)` returns `Task<TokenDto>`
- [ ] `GenerateNewRefreshTokenAsync()` returns `Task<string>`
- [ ] `GetPrincipalFromExpiredToken(string)` returns `ClaimsPrincipal?`
- [ ] Application project builds successfully
