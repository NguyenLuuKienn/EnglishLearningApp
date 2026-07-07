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

- [ ] Migration created successfully
- [ ] QuizAssignments table exists in database
- [ ] Quizzes table has StartTime and EndTime columns

## Acceptance Criteria

- [ ] `DbSet<QuizAssignment> QuizAssignments` added to ApplicationDbContext
- [ ] Migration created with QuizAssignments table
- [ ] Quizzes table updated with StartTime and EndTime columns
- [ ] Migration applied successfully
- [ ] Foreign key to Quizzes with Restrict delete
