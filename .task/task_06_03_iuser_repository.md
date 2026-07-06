# Task 6.3: Interface — IUserRepository

## Description

Create the IUserRepository interface in the Domain layer with methods specific to User queries.

## Priority
🔴 Critical — Required for authentication

## Dependencies
- Task 6.1 (User entity)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Domain/Interfaces/IUserRepository.cs` | Create |

## Steps

### Step 1: Create IUserRepository interface
1. Inherit from `IRepository<User>`
2. Additional methods:
   - `Task<User?> GetByUsernameAsync(string username)`
   - `Task<User?> GetByEmailAsync(string email)`

## Expected Code

```csharp
using EnglishLearning.Domain.Entities;

namespace EnglishLearning.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByEmailAsync(string email);
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Domain` — 0 errors
- [ ] IUserRepository inherits from IRepository<User>
- [ ] Custom methods: GetByUsernameAsync, GetByEmailAsync

## Acceptance Criteria

- [ ] `IUserRepository` interface in `EnglishLearning.Domain.Interfaces` namespace
- [ ] Inherits from `IRepository<User>`
- [ ] `GetByUsernameAsync(string username)` returns `Task<User?>`
- [ ] `GetByEmailAsync(string email)` returns `Task<User?>`
- [ ] Domain project builds successfully
