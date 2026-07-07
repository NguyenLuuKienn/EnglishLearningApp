# Task 7.12: Assignment Migration

## Description

Create EF Core migration for QuizAssignments table and update Quizzes table with StartTime/EndTime.

## Priority
🔴 Critical — Database schema for assignments

## Dependencies
- Task 7.11 (QuizAssignmentRepository + Configuration)

## Files to Edit

| File | Action |
|------|--------|
| `EnglishLearning.Infrastructure/Persistence/ApplicationDbContext.cs` | Edit |

## Steps

### Step 1: Update ApplicationDbContext
1. Add `DbSet<QuizAssignment> QuizAssignments`

### Step 2: Create migration
1. `dotnet ef migrations add AddQuizAssignments --startup-project ..\EnglishLearning.WebAPI`

### Step 3: Apply migration
1. `dotnet ef database update --startup-project ..\EnglishLearning.WebAPI`

## Expected Code

```csharp
// ApplicationDbContext.cs — add:
public DbSet<QuizAssignment> QuizAssignments => Set<QuizAssignment>();
```

## Verification

- [x] `DbSet<QuizAssignment> QuizAssignments` added to ApplicationDbContext ✅
- [x] `DbSet<LearningHistory> LearningHistories` added to ApplicationDbContext ✅
- [x] `DbSet<Leaderboard> Leaderboards` added to ApplicationDbContext ✅
- [ ] Migration created (user will run manually)
- [ ] Migration applied (user will run manually)

## Acceptance Criteria

- [x] All new DbSets added to ApplicationDbContext ✅
- [x] All configurations registered via `ApplyConfigurationsFromAssembly` ✅
- [ ] Migration created and applied (user will run manually)

---

## ✅ Completed: 2026-07-07

- **ApplicationDbContext** — Added `QuizAssignments`, `LearningHistories`, `Leaderboards` DbSets
- **Migration** — User will run manually:
  - `Add-Migration AddQuizAssignmentsHistoryLeaderboard`
  - `Update-Database`
- Build verified: 0 errors
