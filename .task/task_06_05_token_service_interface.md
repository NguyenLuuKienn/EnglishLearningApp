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

- [x] Run `dotnet build EnglishLearning.Application` — 0 errors ✅
- [x] ITokenService interface has required methods ✅

## Acceptance Criteria

- [x] `ITokenService` interface in `EnglishLearning.Application.Interfaces` namespace ✅
- [x] `GenerateTokensAsync(User)` returns `Task<TokenDto>` ✅
- [x] `GenerateNewRefreshTokenAsync()` returns `Task<string>` ✅
- [x] `GetPrincipalFromExpiredToken(string)` returns `ClaimsPrincipal?` ✅
- [x] Application project builds successfully ✅

---

## ✅ Completed: 2026-07-07

- **ITokenService** — GenerateTokensAsync, GenerateNewRefreshTokenAsync, GetPrincipalFromExpiredToken
- Namespace: `EnglishLearning.Application.Interfaces`
- Build verified: 0 errors
