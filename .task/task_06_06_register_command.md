# Task 6.6: Command — Register

## Description

Create RegisterCommand and handler for user registration with BCrypt password hashing.

## Priority
🔴 Critical — User registration

## Dependencies
- Task 6.1 (User entity), Task 6.3 (IUserRepository), Task 6.4 (Auth DTOs)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/Features/Auth/Commands/Register/RegisterCommand.cs` | Create |
| `EnglishLearning.Application/Features/Auth/Commands/Register/RegisterCommandHandler.cs` | Create |

## Steps

### Step 1: Create RegisterCommand
1. Properties: Username, Email, Password
2. Inherits from ` IRequest<Result<Guid>>`

### Step 2: Create RegisterCommandHandler
1. Inject `IUserRepository`, `IUnitOfWork`
2. Validate: username/email not exists
3. Hash password with BCrypt
4. Create User entity
5. Save and return UserId

## Expected Code

```csharp
// RegisterCommand.cs
using MediatR;

namespace EnglishLearning.Application.Features.Auth.Commands.Register;

public record RegisterCommand(
    string Username,
    string Email,
    string Password) : IRequest<Result<Guid>>;

// RegisterCommandHandler.cs
using BCrypt.Net;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<Guid>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Check if username already exists
        var existingUser = await _userRepository.GetByUsernameAsync(request.Username);
        if (existingUser != null)
            return Result<Guid>.Failure("Username already exists.");

        // Check if email already exists
        existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
            return Result<Guid>.Failure("Email already exists.");

        // Hash password
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        // Create user
        var user = User.Create(request.Username, request.Email, passwordHash, UserRole.User);
        await _userRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return user.Id;
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Application` — 0 errors ✅
- [x] RegisterCommand has Username, Email, Password ✅
- [x] Handler validates username/email uniqueness ✅
- [x] Password is hashed with BCrypt ✅

## Acceptance Criteria

- [x] `RegisterCommand` record with Username, Email, Password ✅
- [x] `RegisterCommandHandler` checks username/email uniqueness ✅
- [x] Password hashed with BCrypt before saving ✅
- [x] Returns `Result<Guid>` (UserId on success) ✅
- [x] Application project builds successfully ✅

---

## ✅ Completed: 2026-07-07

- **RegisterCommand** — Username, Email, Password → `IRequest<Result<Guid>>`
- **RegisterCommandHandler** — Validates uniqueness, BCrypt hash, creates User, saves via UnitOfWork
- Namespace: `EnglishLearning.Application.Features.Auth.Commands.Register`
- Dependencies added: `BCrypt.Net-Next` 4.0.3
- Build verified: 0 errors
