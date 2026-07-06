# Task 6.9: Query — GetProfile

## Description

Create GetProfileQuery and handler to get current user's profile information.

## Priority
🟡 High — User profile retrieval

## Dependencies
- Task 6.4 (Auth DTOs)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/Features/Auth/Queries/GetProfile/GetProfileQuery.cs` | Create |
| `EnglishLearning.Application/Features/Auth/Queries/GetProfile/GetProfileQueryHandler.cs` | Create |

## Steps

### Step 1: Create GetProfileQuery
1. Property: UserId (Guid)
2. Inherits from `IRequest<Result<UserDto>>`

### Step 2: Create GetProfileQueryHandler
1. Inject `IUserRepository`
2. Find user by Id
3. Map to UserDto

## Expected Code

```csharp
// GetProfileQuery.cs
using EnglishLearning.Application.DTOs;
using MediatR;

namespace EnglishLearning.Application.Features.Auth.Queries.GetProfile;

public record GetProfileQuery(Guid UserId) : IRequest<Result<UserDto>>;

// GetProfileQueryHandler.cs
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Application.Features.Auth.Queries.GetProfile;

public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, Result<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetProfileQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserDto>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
            return Result<UserDto>.Failure("User not found.");

        var dto = new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role,
            AvatarUrl = user.AvatarUrl,
            CreatedAt = user.CreatedAt
        };

        return dto;
    }
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Application` — 0 errors
- [ ] GetProfileQuery returns UserDto

## Acceptance Criteria

- [ ] `GetProfileQuery` with UserId
- [ ] `GetProfileQueryHandler` finds user by Id
- [ ] Returns `Result<UserDto>` with user info (no password)
- [ ] Application project builds successfully
