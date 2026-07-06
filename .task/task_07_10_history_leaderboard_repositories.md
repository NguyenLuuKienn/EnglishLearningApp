# Task 7.10: Repositories — History & Leaderboard

## Description

Create repository implementations and EF Core configurations for LearningHistory and Leaderboard.

## Priority
🔴 Critical — Data access for history & leaderboard

## Dependencies
- Task 7.4 (Interfaces)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Infrastructure/Repositories/LearningHistoryRepository.cs` | Create |
| `EnglishLearning.Infrastructure/Repositories/LeaderboardRepository.cs` | Create |
| `EnglishLearning.Infrastructure/Persistence/Configurations/LearningHistoryConfiguration.cs` | Create |
| `EnglishLearning.Infrastructure/Persistence/Configurations/LeaderboardConfiguration.cs` | Create |

## Steps

### Step 1: Create repositories
1. LearningHistoryRepository — implement ILearningHistoryRepository
2. LeaderboardRepository — implement ILeaderboardRepository

### Step 2: Create configurations
1. LearningHistory: table "LearningHistories", indexes on UserId, CreatedAt
2. Leaderboard: table "Leaderboards", unique index on UserId

## Expected Code

```csharp
// LearningHistoryRepository.cs
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Infrastructure.Repositories;

public class LearningHistoryRepository : Repository<LearningHistory>, ILearningHistoryRepository
{
    public LearningHistoryRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<LearningHistory>> GetByUserIdAsync(string userId, int pageNumber, int pageSize)
    {
        var all = await GetAllAsync();
        return all
            .Where(h => h.UserId == userId)
            .OrderByDescending(h => h.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public async Task<List<LearningHistory>> GetRecentByUserIdAsync(string userId, int count)
    {
        var all = await GetAllAsync();
        return all
            .Where(h => h.UserId == userId)
            .OrderByDescending(h => h.CreatedAt)
            .Take(count)
            .ToList();
    }
}

// LeaderboardRepository.cs
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Infrastructure.Repositories;

public class LeaderboardRepository : Repository<Leaderboard>, ILeaderboardRepository
{
    public LeaderboardRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Leaderboard?> GetByUserIdAsync(string userId)
    {
        var all = await GetAllAsync();
        return all.FirstOrDefault(l => l.UserId == userId);
    }

    public async Task<List<Leaderboard>> GetTopUsersAsync(int count)
    {
        var all = await GetAllAsync();
        return all.OrderByDescending(l => l.TotalScore).Take(count).ToList();
    }

    public async Task<int> GetRankByUserIdAsync(string userId)
    {
        var all = await GetAllAsync();
        var sorted = all.OrderByDescending(l => l.TotalScore).ToList();
        var index = sorted.FindIndex(l => l.UserId == userId);
        return index >= 0 ? index + 1 : -1;
    }
}

// LearningHistoryConfiguration.cs
using EnglishLearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishLearning.Infrastructure.Persistence.Configurations;

public class LearningHistoryConfiguration : IEntityTypeConfiguration<LearningHistory>
{
    public void Configure(EntityTypeBuilder<LearningHistory> builder)
    {
        builder.ToTable("LearningHistories");
        builder.Property(h => h.UserId).IsRequired().HasMaxLength(200);
        builder.Property(h => h.ActionType).HasConversion<int>();
        builder.Property(h => h.Details).HasMaxLength(1000);
        builder.Property(h => h.Score).HasPrecision(5, 2);
        builder.HasIndex(h => h.UserId);
        builder.HasIndex(h => h.CreatedAt);
    }
}

// LeaderboardConfiguration.cs
using EnglishLearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishLearning.Infrastructure.Persistence.Configurations;

public class LeaderboardConfiguration : IEntityTypeConfiguration<Leaderboard>
{
    public void Configure(EntityTypeBuilder<Leaderboard> builder)
    {
        builder.ToTable("Leaderboards");
        builder.Property(l => l.UserId).IsRequired().HasMaxLength(200);
        builder.HasIndex(l => l.UserId).IsUnique();
        builder.Property(l => l.TotalScore).HasPrecision(10, 2);
        builder.Property(l => l.AverageScore).HasPrecision(5, 2);
    }
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Infrastructure` — 0 errors
- [ ] Both repositories implement their interfaces
- [ ] Configurations define tables and indexes

## Acceptance Criteria

- [ ] `LearningHistoryRepository` implements `ILearningHistoryRepository`
- [ ] `LeaderboardRepository` implements `ILeaderboardRepository`
- [ ] Configurations set correct table names and indexes
- [ ] Infrastructure project builds successfully
