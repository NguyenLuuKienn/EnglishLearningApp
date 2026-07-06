# Task 6.7: Command — Login

## Description

Create LoginCommand and handler for user authentication with JWT token generation.

## Priority
🔴 Critical — User login

## Dependencies
- Task 6.5 (ITokenService), Task 6.6 (Register)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/Features/Auth/Commands/Login/LoginCommand.cs` | Create |
| `EnglishLearning.Application/Features/Auth/Commands/Login/LoginCommandHandler.cs` | Create |

## Steps

### Step 1: Create LoginCommand
1. Properties: Username, Password
2. Inherits from `IRequest<Result<TokenDto>>`

### Step 2: Create LoginCommandHandler
1. Inject `IUserRepository`, `ITokenService`
2. Find user by username
3. Verify password with BCrypt
4. Generate JWT tokens
5. Update user's RefreshToken
6. Return TokenDto

## Expected Code

```csharp
// LoginCommand.cs
using EnglishLearning.Application.DTOs;
using MediatR;

namespace EnglishLearning.Application.Features.Auth.Commands.Login;

public record LoginCommand(
    string Username,
    string Password) : IRequest<Result<TokenDto>>;

// LoginCommandHandler.cs
using BCrypt.Net;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Interfaces;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<TokenDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public LoginCommandHandler(IUserRepository userRepository, ITokenService tokenService, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TokenDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return Result<TokenDto>.Failure("Invalid username or password.");

        if (!user.IsActive)
            return Result<TokenDto>.Failure("Your account is deactivated. Please contact support.");

        // Generate tokens
        var tokens = await _tokenService.GenerateTokensAsync(user);

        // Update refresh token
        user.RefreshToken = tokens.RefreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddHours(720); // 30 days
        await _unitOfWork.SaveChangesAsync();

        return tokens;
    }
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Application` — 0 errors
- [ ] LoginCommand has Username, Password
- [ ] Handler verifies password with BCrypt
- [ ] Returns TokenDto with AccessToken and RefreshToken

## Acceptance Criteria

- [ ] `LoginCommand` record with Username, Password
- [ ] `LoginCommandHandler` finds user by username
- [ ] Password verified with BCrypt
- [ ] Checks if user is active
- [ ] Generates JWT tokens via ITokenService
- [ ] Updates user's RefreshToken in database
- [ ] Returns `Result<TokenDto>`
- [ ] Application project builds successfully
