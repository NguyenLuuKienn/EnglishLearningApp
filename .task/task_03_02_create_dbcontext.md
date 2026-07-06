# Task 3.2: Create DbContext

## Description

Create the `ApplicationDbContext` class that serves as the main EF Core context for the application. Configures all entity sets and relationships.

## Priority
🔴 Critical — Core of data access layer

## Dependencies
- Task 3.1 (Infrastructure dependencies)
- Task 1.2 - Task 1.6 (All entities)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Infrastructure/Persistence/ApplicationDbContext.cs` | Create |

## Steps

### Step 1: Create ApplicationDbContext class
1. Inherit from `DbContext`
2. Add constructor accepting `DbContextOptions<ApplicationDbContext>`
3. Add `DbSet<T>` properties for all entities:
   - `DbSet<Vocabulary> Vocabularies`
   - `DbSet<Quiz> Quizzes`
   - `DbSet<Question> Questions`
   - `DbSet<Choice> Choices`
   - `DbSet<QuizResult> QuizResults`

### Step 2: Override OnModelCreating
1. Call `base.OnModelCreating(builder)`
2. Apply entity configurations using `ApplyConfigurationsFromAssembly`
3. Configure relationships explicitly if not handled by configurations

### Step 3: Configure relationships in OnModelCreating
1. Quiz → Questions (one-to-many, cascade delete)
2. Question → Choices (one-to-many, cascade delete)
3. Quiz → QuizResults (one-to-many)

## Expected Code

```csharp
using Microsoft.EntityFrameworkCore;
using EnglishLearning.Domain.Entities;

namespace EnglishLearning.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Vocabulary> Vocabularies => Set<Vocabulary>();
    public DbSet<Quiz> Quizzes => Set<Quiz>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Choice> Choices => Set<Choice>();
    public DbSet<QuizResult> QuizResults => Set<QuizResult>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Apply entity configurations from this assembly
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Configure relationships
        builder.Entity<Quiz>()
            .HasMany(q => q.Questions)
            .WithOne(q => q.Quiz)
            .HasForeignKey(q => q.QuizId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Question>()
            .HasMany(q => q.Choices)
            .WithOne(c => c.Question)
            .HasForeignKey(c => c.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Quiz>()
            .HasMany(q => q.Results)
            .WithOne(r => r.Quiz)
            .HasForeignKey(r => r.QuizId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Infrastructure` — 0 errors ✅
- [x] DbContext has DbSet for all 5 entities ✅
- [x] Relationships are configured with cascade delete ✅
- [x] Configurations are loaded from assembly ✅

## Acceptance Criteria

- [x] `ApplicationDbContext` inherits from `DbContext` ✅
- [x] Has DbSet properties: Vocabularies, Quizzes, Questions, Choices, QuizResults ✅
- [x] Constructor accepts `DbContextOptions<ApplicationDbContext>` ✅
- [x] `OnModelCreating` applies configurations from assembly ✅
- [x] Quiz → Questions relationship configured (cascade delete) ✅
- [x] Question → Choices relationship configured (cascade delete) ✅
- [x] Quiz → QuizResults relationship configured (cascade delete) ✅
- [x] Infrastructure project builds successfully ✅

---

## ✅ Completed: 2026-07-06

- `ApplicationDbContext` inherits from `DbContext`
- 5 DbSet properties: Vocabularies, Quizzes, Questions, Choices, QuizResults
- `OnModelCreating` configures:
  - `ApplyConfigurationsFromAssembly` for entity configurations
  - Quiz → Questions (one-to-many, cascade delete)
  - Question → Choices (one-to-many, cascade delete)
  - Quiz → QuizResults (one-to-many, cascade delete)
- Build verified: 0 errors
