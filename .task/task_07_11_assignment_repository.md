# Task 7.11: QuizAssignmentRepository + Configuration

## Description

Create QuizAssignmentRepository implementation and EF Core configuration.

## Priority
🔴 Critical — Data access for assignments

## Dependencies
- Task 7.4 (IQuizAssignmentRepository)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Infrastructure/Repositories/QuizAssignmentRepository.cs` | Create |
| `EnglishLearning.Infrastructure/Persistence/Configurations/QuizAssignmentConfiguration.cs` | Create |

## Steps

### Step 1: Create QuizAssignmentRepository
1. Inherit from `Repository<QuizAssignment>`
2. Implement `IQuizAssignmentRepository`
3. Implement custom query methods

### Step 2: Create QuizAssignmentConfiguration
1. Table name: "QuizAssignments"
2. QuizId: required, FK to Quizzes
3. TargetRole: HasConversion<int>(), nullable
4. TargetUserId: max 200, nullable
5. StartTime, EndTime: required
6. Status: HasConversion<int>()
7. Indexes on QuizId, TargetRole, TargetUserId, StartTime, EndTime

## Expected Code

```csharp
// QuizAssignmentRepository.cs
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;

namespace EnglishLearning.Infrastructure.Repositories;

public class QuizAssignmentRepository(ApplicationDbContext context) 
    : Repository<QuizAssignment>(context), IQuizAssignmentRepository
{
    public async Task<List<QuizAssignment>> GetByUserIdAsync(string userId)
    {
        var all = await GetAllAsync();
        return all.Where(a => a.TargetUserId == userId).ToList();
    }

    public async Task<List<QuizAssignment>> GetByRoleAsync(UserRole role)
    {
        var all = await GetAllAsync();
        return all.Where(a => a.TargetRole == role).ToList();
    }

    public async Task<List<QuizAssignment>> GetActiveAssignmentsAsync()
    {
        var now = DateTime.UtcNow;
        var all = await GetAllAsync();
        return all
            .Where(a => a.Status != AssignmentStatus.Cancelled &&
                       a.StartTime <= now && a.EndTime >= now)
            .ToList();
    }

    public async Task<List<QuizAssignment>> GetExpiringSoonAsync(DateTime before)
    {
        var all = await GetAllAsync();
        return all
            .Where(a => a.Status != AssignmentStatus.Cancelled &&
                       a.EndTime <= before)
            .ToList();
    }
}

// QuizAssignmentConfiguration.cs
using EnglishLearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishLearning.Infrastructure.Persistence.Configurations;

public class QuizAssignmentConfiguration : IEntityTypeConfiguration<QuizAssignment>
{
    public void Configure(EntityTypeBuilder<QuizAssignment> builder)
    {
        builder.ToTable("QuizAssignments");

        builder.Property(a => a.QuizId).IsRequired();
        builder.HasOne(a => a.Quiz).WithMany().HasForeignKey(a => a.QuizId).OnDelete(DeleteBehavior.Restrict);

        builder.Property(a => a.TargetRole).HasConversion<int>().IsRequired(false);
        builder.Property(a => a.TargetUserId).HasMaxLength(200).IsRequired(false);
        builder.Property(a => a.StartTime).IsRequired();
        builder.Property(a => a.EndTime).IsRequired();
        builder.Property(a => a.Status).HasConversion<int>().IsRequired();

        builder.HasIndex(a => a.QuizId);
        builder.HasIndex(a => a.TargetRole);
        builder.HasIndex(a => a.TargetUserId);
        builder.HasIndex(a => a.StartTime);
        builder.HasIndex(a => a.EndTime);
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Infrastructure` — 0 errors ✅
- [x] Repository implements IQuizAssignmentRepository ✅
- [x] Configuration defines table and constraints ✅

## Acceptance Criteria

- [x] `QuizAssignmentRepository` implements `IQuizAssignmentRepository` ✅
- [ ] Custom methods: GetByUserIdAsync, GetByRoleAsync, GetActiveAssignmentsAsync, GetExpiringSoonAsync
- [ ] `QuizAssignmentConfiguration` sets table name "QuizAssignments"
- [ ] FK to Quizzes with Restrict delete
- [ ] Indexes on QuizId, TargetRole, TargetUserId, StartTime, EndTime
- [ ] Infrastructure project builds successfully
