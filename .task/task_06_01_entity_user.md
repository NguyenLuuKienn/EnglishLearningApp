# Task 6.1: Entity — User

## Description

Create the User entity in the Domain layer for authentication and user management.

## Priority
🔴 Critical — Foundation for authentication

## Dependencies
- Phase 1 complete (BaseEntity, Enums)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Domain/Entities/User.cs` | Create |

## Steps

### Step 1: Create User entity
1. Inherit from `BaseEntity`
2. Properties:
   - `Username` (string, required, max 100)
   - `Email` (string, required, max 200)
   - `PasswordHash` (string, required)
   - `RefreshToken` (string?, nullable)
   - `RefreshTokenExpiry` (DateTime?, nullable)
   - `Role` (UserRole enum)
   - `AvatarUrl` (string?, nullable, max 500)
   - `IsActive` (bool, default true)
3. Factory method `Create(username, email, passwordHash, role)`

## Expected Code

```csharp
namespace EnglishLearning.Domain.Entities;

public class User : Common.BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
    public Enums.UserRole Role { get; set; }
    public string? AvatarUrl { get; set; }
    public bool IsActive { get; set; } = true;

    public static User Create(string username, string email, string passwordHash, Enums.UserRole role = Enums.UserRole.User)
    {
        return new User
        {
            Username = username,
            Email = email,
            PasswordHash = passwordHash,
            Role = role,
            IsActive = true
        };
    }
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Domain` — 0 errors
- [ ] User entity inherits from BaseEntity
- [ ] All properties are defined correctly
- [ ] Factory method `Create()` exists

## Acceptance Criteria

- [ ] `User` entity inherits from `BaseEntity`
- [ ] Properties: Username, Email, PasswordHash, RefreshToken, RefreshTokenExpiry, Role, AvatarUrl, IsActive
- [ ] `Username` max 100 chars, `Email` max 200 chars
- [ ] `Role` uses `UserRole` enum
- [ ] Factory method `Create()` with username, email, passwordHash, role
- [ ] Domain project builds successfully
