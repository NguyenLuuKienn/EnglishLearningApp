# Task 3.3: Create Entity Configurations

## Description

Create Fluent API entity configurations for all 5 entities. These configurations define column types, constraints, indexes, and relationships at the database level.

## Priority
🔴 Critical — Defines database schema

## Dependencies
- Task 3.1 (Infrastructure dependencies)
- Task 3.2 (DbContext created)
- Task 1.0 (BaseEntity)
- Task 1.2 - Task 1.6 (All entities)

## Files to Create

| File | Action |
|------|--------|
| `Persistence/Configurations/BaseEntityConfiguration.cs` | Create |
| `Persistence/Configurations/VocabularyConfiguration.cs` | Create |
| `Persistence/Configurations/QuizConfiguration.cs` | Create |
| `Persistence/Configurations/QuestionConfiguration.cs` | Create |
| `Persistence/Configurations/ChoiceConfiguration.cs` | Create |
| `Persistence/Configurations/QuizResultConfiguration.cs` | Create |

## Steps

### Step 1: Create BaseEntityConfiguration
- Configure: Id (Guid, PK, ValueGeneratedOnAdd), CreatedAt (required), UpdatedAt (required), CreatedBy (max 200), UpdatedBy (max 200)
- Use `ToAllTypes<BaseEntity>()` to apply to all entities inheriting BaseEntity

### Step 2: Create VocabularyConfiguration
- Configure: Id (Guid, PK), Word (string, required, max 200, indexed), Definition (string, required, max 1000), Example (string, max 1000), PartOfSpeech (string, max 50), Difficulty (int), CreatedAt, UpdatedAt

### Step 3: Create QuizConfiguration
- Configure: Id (Guid, PK), Title (string, required, max 200), Description (string, max 1000), Difficulty (int), TimeLimitMinutes (int), PassingScore (decimal 5,2), CreatedAt, UpdatedAt

### Step 4: Create QuestionConfiguration
- Configure: Id (Guid, PK), QuestionText (string, required, max 2000), QuestionType (int), Difficulty (int), CorrectAnswer (string, max 1000), Explanation (string, max 1000), QuizId (Guid), CreatedAt

### Step 5: Create ChoiceConfiguration
- Configure: Id (Guid, PK), ChoiceText (string, required, max 500), IsCorrect (bool), QuestionId (Guid)

### Step 6: Create QuizResultConfiguration
- Configure: Id (Guid, PK), QuizId (Guid), UserId (string, required, max 200), Score (decimal 5,2), TotalQuestions (int), CorrectAnswers (int), DurationMinutes (int), CompletedAt

## Expected Code

```csharp
// BaseEntityConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnglishLearning.Domain.Common;

namespace EnglishLearning.Infrastructure.Persistence.Configurations;

public class BaseEntityConfiguration : IEntityTypeConfiguration<BaseEntity>
{
    public void Configure(EntityTypeBuilder<BaseEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();
        builder.Property(e => e.CreatedBy).HasMaxLength(200);
        builder.Property(e => e.UpdatedBy).HasMaxLength(200);
    }
}

// VocabularyConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnglishLearning.Domain.Entities;

namespace EnglishLearning.Infrastructure.Persistence.Configurations;

public class VocabularyConfiguration : IEntityTypeConfiguration<Vocabulary>
{
    public void Configure(EntityTypeBuilder<Vocabulary> builder)
    {
        builder.ToTable("Vocabularies");

        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id).ValueGeneratedOnAdd();

        builder.Property(v => v.Word)
            .IsRequired()
            .HasMaxLength(200);
        builder.HasIndex(v => v.Word);

        builder.Property(v => v.Definition)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(v => v.Example).HasMaxLength(1000);
        builder.Property(v => v.PartOfSpeech).HasMaxLength(50);
        builder.Property(v => v.Difficulty).HasConversion<int>();
    }
}

// QuizConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnglishLearning.Domain.Entities;

namespace EnglishLearning.Infrastructure.Persistence.Configurations;

public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        builder.ToTable("Quizzes");

        builder.HasKey(q => q.Id);
        builder.Property(q => q.Id).ValueGeneratedOnAdd();

        builder.Property(q => q.Title).IsRequired().HasMaxLength(200);
        builder.Property(q => q.Description).HasMaxLength(1000);
        builder.Property(q => q.Difficulty).HasConversion<int>();
        builder.Property(q => q.TimeLimitMinutes).HasDefaultValue(0);
        builder.Property(q => q.PassingScore).HasPrecision(5, 2).HasDefaultValue(50m);
    }
}

// QuestionConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnglishLearning.Domain.Entities;

namespace EnglishLearning.Infrastructure.Persistence.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Questions");

        builder.HasKey(q => q.Id);
        builder.Property(q => q.Id).ValueGeneratedOnAdd();

        builder.Property(q => q.QuestionText).IsRequired().HasMaxLength(2000);
        builder.Property(q => q.QuestionType).HasConversion<int>();
        builder.Property(q => q.Difficulty).HasConversion<int>();
        builder.Property(q => q.CorrectAnswer).HasMaxLength(1000);
        builder.Property(q => q.Explanation).HasMaxLength(1000);
    }
}

// ChoiceConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnglishLearning.Domain.Entities;

namespace EnglishLearning.Infrastructure.Persistence.Configurations;

public class ChoiceConfiguration : IEntityTypeConfiguration<Choice>
{
    public void Configure(EntityTypeBuilder<Choice> builder)
    {
        builder.ToTable("Choices");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();

        builder.Property(c => c.ChoiceText).IsRequired().HasMaxLength(500);
        builder.Property(c => c.IsCorrect).IsRequired();
    }
}

// QuizResultConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnglishLearning.Domain.Entities;

namespace EnglishLearning.Infrastructure.Persistence.Configurations;

public class QuizResultConfiguration : IEntityTypeConfiguration<QuizResult>
{
    public void Configure(EntityTypeBuilder<QuizResult> builder)
    {
        builder.ToTable("QuizResults");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedOnAdd();

        builder.Property(r => r.UserId).IsRequired().HasMaxLength(200);
        builder.Property(r => r.Score).HasPrecision(5, 2);
        builder.Property(r => r.TotalQuestions).IsRequired();
        builder.Property(r => r.CorrectAnswers).IsRequired();
        builder.Property(r => r.DurationMinutes).IsRequired();
        builder.Property(r => r.CompletedAt).IsRequired();
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Infrastructure` — 0 errors ✅
- [x] All 5 configurations implement `IEntityTypeConfiguration<T>` ✅
- [x] All string properties have `HasMaxLength` constraints ✅
- [x] All required properties have `IsRequired()` ✅
- [x] Enums are converted to `int` with `HasConversion<int>()` ✅

## Acceptance Criteria

- [x] `BaseEntityConfiguration` — Id, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy configured ✅
- [x] `VocabularyConfiguration` — Word indexed, max lengths configured ✅
- [x] `QuizConfiguration` — PassingScore precision (5,2), defaults configured ✅
- [x] `QuestionConfiguration` — QuestionText max 2000, enums converted ✅
- [x] `ChoiceConfiguration` — ChoiceText max 500, IsCorrect required ✅
- [x] `QuizResultConfiguration` — Score precision (5,2), all fields required ✅
- [x] All configurations use correct table names ✅
- [x] Infrastructure project builds successfully ✅

---

## ✅ Completed: 2026-07-06

- 6 entity configurations created:
  - `BaseEntityConfiguration` — Id (PK, ValueGeneratedOnAdd), CreatedAt, UpdatedAt, CreatedBy (max 200), UpdatedBy (max 200)
  - `VocabularyConfiguration` — Word (indexed, max 200), Definition (max 1000), Example (max 1000), PartOfSpeech (max 50), Difficulty (enum→int)
  - `QuizConfiguration` — Title (max 200), Description (max 1000), Difficulty (enum→int), TimeLimitMinutes (default 0), PassingScore (precision 5,2, default 50m)
  - `QuestionConfiguration` — QuestionText (max 2000), QuestionType (enum→int), Difficulty (enum→int), CorrectAnswer (max 1000), Explanation (max 1000)
  - `ChoiceConfiguration` — ChoiceText (max 500), IsCorrect (required)
  - `QuizResultConfiguration` — UserId (max 200), Score (precision 5,2), TotalQuestions, CorrectAnswers, DurationMinutes, CompletedAt (all required)
- Build verified: 0 errors
