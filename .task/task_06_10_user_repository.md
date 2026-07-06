# Task 6.10: UserRepository + Configuration

## Description

Create UserRepository implementation and EF Core configuration for User entity.

## Priority
🔴 Critical — Data access for authentication

## Dependencies
- Task 6.3 (IUserRepository)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Infrastructure/Repositories/UserRepository.cs` | Create |
| `EnglishLearning.Infrastructure/Persistence/Configurations/UserConfiguration.cs` | Create |

## Steps

### Step 1: Create UserRepository
1. Inherit from `Repository<User>`
2. Implement `IUserRepository`
3. Implement `GetByUsernameAsync`, `GetByEmailAsync`

### Step 2: Create UserConfiguration
1. Table name: "Users"
2. Username: required, max 100, indexed
3. Email: required, max 200, indexed
4. PasswordHash: required
5. Role: HasConversion<int>()
6. AvatarUrl: max 500

## Expected Code

```csharp
// UserRepository.cs
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearning.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}

// UserConfiguration.cs
using EnglishLearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishLearning.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.Property(u => u.Username).IsRequired().HasMaxLength(100);
        builder.HasIndex(u => u.Username).IsUnique();

        builder.Property(u => u.Email).IsRequired().HasMaxLength(200);
        builder.HasIndex(u => u.Email).IsUnique();

        builder.Property(u => u.PasswordHash).IsRequired();
        builder.Property(u => u.Role).HasConversion<int>();
        builder.Property(u => u.AvatarUrl).HasMaxLength(500);
        builder.Property(u => u.IsActive).IsRequired();
    }
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Infrastructure` — 0 errors
- [ ] UserRepository implements IUserRepository
- [ ] UserConfiguration defines table and constraints

## Acceptance Criteria

- [ ] `UserRepository` inherits `Repository<User>` and implements `IUserRepository`
- [ ] `GetByUsernameAsync` and `GetByEmailAsync` implemented
- [ ] `UserConfiguration` sets table name "Users"
- [ ] Username and Email are unique indexed
- [ ] Role converted to int
- [ ] Infrastructure project builds successfully
