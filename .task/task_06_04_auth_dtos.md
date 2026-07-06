# Task 6.4: DTOs — Auth

## Description

Create DTOs for authentication: UserDto, TokenDto.

## Priority
🔴 Critical — Required for Auth CQRS

## Dependencies
- Task 6.1 (User entity)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/DTOs/UserDto.cs` | Create |
| `EnglishLearning.Application/DTOs/TokenDto.cs` | Create |

## Steps

### Step 1: Create UserDto
1. Properties: Id, Username, Email, Role, AvatarUrl, CreatedAt

### Step 2: Create TokenDto
1. Properties: AccessToken, RefreshToken, ExpiresIn

## Expected Code

```csharp
// UserDto.cs
using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Application.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public string? AvatarUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}

// TokenDto.cs
namespace EnglishLearning.Application.DTOs;

public class TokenDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Application` — 0 errors
- [ ] UserDto has all required properties
- [ ] TokenDto has AccessToken, RefreshToken, ExpiresIn

## Acceptance Criteria

- [ ] `UserDto` with Id, Username, Email, Role, AvatarUrl, CreatedAt
- [ ] `TokenDto` with AccessToken, RefreshToken, ExpiresIn
- [ ] Both in `EnglishLearning.Application.DTOs` namespace
- [ ] Application project builds successfully
