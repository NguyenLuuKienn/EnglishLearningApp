# Task 6.8: Command — RefreshToken

## Description

Create RefreshTokenCommand and handler for token refresh.

## Priority
🟡 High — Token refresh flow

## Dependencies
- Task 6.5 (ITokenService), Task 6.7 (Login)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/Features/Auth/Commands/RefreshToken/RefreshTokenCommand.cs` | Create |
| `EnglishLearning.Application/Features/Auth/Commands/RefreshToken/RefreshTokenCommandHandler.cs` | Create |

## Steps

### Step 1: Create RefreshTokenCommand
1. Properties: AccessToken, RefreshToken
2. Inherits from `IRequest<Result<TokenDto>>`

### Step 2: Create RefreshTokenCommandHandler
1. Validate refresh token exists and not expired
2. Generate new token pair
3. Update user's refresh token

## Expected Code

```csharp
// RefreshTokenCommand.cs
using EnglishLearning.Application.DTOs;
using MediatR;

namespace EnglishLearning.Application.Features.Auth.Commands.RefreshToken;

public record RefreshTokenCommand(
    string AccessToken,
    string RefreshToken) : IRequest<Result<TokenDto>>;

// RefreshTokenCommandHandler.cs
using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Interfaces;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<TokenDto>>
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenCommandHandler(ITokenService tokenService, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TokenDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        if (principal == null)
            return Result<TokenDto>.Failure("Invalid access token.");

        var email = principal.FindFirst("email")?.Value;
        var user = await _userRepository.GetByEmailAsync(email!);
        if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiry <= DateTime.UtcNow)
            return Result<TokenDto>.Failure("Invalid or expired refresh token.");

        var newTokens = await _tokenService.GenerateTokensAsync(user);

        user.RefreshToken = newTokens.RefreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddHours(720);
        await _unitOfWork.SaveChangesAsync();

        return newTokens;
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Application` — 0 errors ✅
- [x] RefreshTokenCommand validates tokens ✅
- [x] Returns new TokenDto ✅

## Acceptance Criteria

- [x] `RefreshTokenCommand` with AccessToken, RefreshToken ✅
- [x] Handler validates expired access token ✅
- [x] Validates refresh token matches and not expired ✅
- [x] Generates new token pair ✅
- [x] Updates user's refresh token ✅
- [x] Returns `TokenDto` ✅
- [x] Application project builds successfully ✅

---

## ✅ Completed: 2026-07-07

- **RefreshTokenCommand** — AccessToken, RefreshToken → `IRequest<TokenDto>`
- **RefreshTokenCommandHandler** — Validates expired access token via ITokenService, checks refresh token match & expiry, generates new tokens, updates user
- Namespace: `EnglishLearning.Application.Features.Auth.Commands.RefreshToken`
- Primary constructor injection, throws `UnauthorizedAccessException` with `AuthErrorMessages`
- Build verified: 0 errors
