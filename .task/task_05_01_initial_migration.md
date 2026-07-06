# Task 5.1: Create Initial Migration

## Description

Create and apply the initial EF Core migration to create the database schema.

## Priority
🔴 Critical — Creates the database

## Dependencies
- Task 3.2 (DbContext)
- Task 3.3 (Entity configurations)
- Task 3.7 (Infrastructure DI)
- Task 4.7 (appsettings.json with connection string)

## Files to Create (by EF Core)

| File | Action |
|------|--------|
| `Infrastructure/Persistence/Migrations/{Timestamp}_InitialCreate.cs` | Auto-generated |
| `Infrastructure/Persistence/Migrations/{Timestamp}_InitialCreate.Designer.cs` | Auto-generated |
| `Infrastructure/Persistence/Migrations/ApplicationDbContextModelSnapshot.cs` | Auto-generated |

## Steps

### Step 1: Create Migrations folder
1. Create `Persistence/Migrations/` folder in Infrastructure project

### Step 2: Run migration command
1. Open terminal in `EnglishLearning.Infrastructure` directory
2. Run: `dotnet ef migrations add InitialCreate --startup-project ..\EnglishLearning.WebAPI`
3. Verify migration files are created

### Step 3: Apply migration to database
1. Run: `dotnet ef database update --startup-project ..\EnglishLearning.WebAPI`
2. Verify database is created with all tables

### Step 4: Verify tables
1. Check that tables exist: Vocabularies, Quizzes, Questions, Choices, QuizResults
2. Check that columns match entity configurations

## Verification

- [ ] Migration files are created in `Persistence/Migrations/`
- [ ] `dotnet ef database update` succeeds
- [ ] Database `EnglishLearningDb` is created
- [ ] All 5 tables exist: Vocabularies, Quizzes, Questions, Choices, QuizResults
- [ ] Column types match configurations (string lengths, decimal precision, etc.)

## Acceptance Criteria

- [ ] Initial migration created successfully
- [ ] Migration applied to database without errors
- [ ] Vocabularies table exists with correct columns
- [ ] Quizzes table exists with correct columns
- [ ] Questions table exists with correct columns and foreign key to Quizzes
- [ ] Choices table exists with correct columns and foreign key to Questions
- [ ] QuizResults table exists with correct columns and foreign key to Quizzes
- [ ] Cascade delete relationships are configured
