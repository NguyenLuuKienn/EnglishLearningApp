# Task 8.8: NotificationRepository + Configuration

## Description

Create NotificationRepository implementation and EF Core configuration.

## Priority
🔴 Critical — Data access for notifications

## Dependencies
- Task 8.3 (INotificationRepository)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Infrastructure/Repositories/NotificationRepository.cs` | Create |
| `EnglishLearning.Infrastructure/Persistence/Configurations/NotificationConfiguration.cs` | Create |

## Steps

### Step 1: Create NotificationRepository
1. Inherit from `Repository<Notification>`
2. Implement `INotificationRepository`
3. Implement custom query methods

### Step 2: Create NotificationConfiguration
1. Table name: "Notifications"
2. UserId: required, max 200, indexed
3. Type: HasConversion<int>()
4. Title: required, max 200
5. Message: required, max 1000
6. Data: max 2000, nullable
7. Indexes on UserId, IsRead, CreatedAt

## Expected Code

```csharp
// NotificationRepository.cs
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Infrastructure.Repositories;

public class NotificationRepository(ApplicationDbContext context) 
    : Repository<Notification>(context), INotificationRepository
{
    public async Task<List<Notification>> GetByUserIdAsync(string userId, int pageNumber, int pageSize)
    {
        var all = await GetAllAsync();
        return all
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public async Task<int> GetUnreadCountAsync(string userId)
    {
        var all = await GetAllAsync();
        return all.Count(n => n.UserId == userId && !n.IsRead);
    }
}

// NotificationConfiguration.cs
using EnglishLearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishLearning.Infrastructure.Persistence.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("Notifications");

        builder.Property(n => n.UserId).IsRequired().HasMaxLength(200);
        builder.Property(n => n.Type).HasConversion<int>().IsRequired();
        builder.Property(n => n.Title).IsRequired().HasMaxLength(200);
        builder.Property(n => n.Message).IsRequired().HasMaxLength(1000);
        builder.Property(n => n.Data).HasMaxLength(2000).IsRequired(false);
        builder.Property(n => n.IsRead).IsRequired();

        builder.HasIndex(n => n.UserId);
        builder.HasIndex(n => n.IsRead);
        builder.HasIndex(n => n.CreatedAt);
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Infrastructure` — 0 errors ✅
- [x] Repository implements INotificationRepository ✅
- [x] Configuration defines table and constraints ✅

## Acceptance Criteria

- [x] `NotificationRepository` implements `INotificationRepository` ✅
- [x] Custom methods: GetByUserIdAsync (with isRead filter), GetUnreadCountAsync ✅
- [x] `NotificationConfiguration` sets table name "Notifications" ✅
- [x] Indexes on UserId, IsRead, CreatedAt ✅
- [x] Infrastructure project builds successfully ✅
