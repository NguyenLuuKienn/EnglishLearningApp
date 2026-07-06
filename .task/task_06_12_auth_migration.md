# Task 6.12: Auth Migration + Seed Admin

## Description

Create EF Core migration for Users table and seed an admin user.

## Priority
🔴 Critical — Database schema for authentication

## Dependencies
- Task 6.10 (UserRepository + Configuration)

## Files to Edit

| File | Action |
|------|--------|
| `EnglishLearning.Infrastructure/Persistence/ApplicationDbContext.cs` | Edit |
| `EnglishLearning.Infrastructure/Persistence/DataSeeder.cs` | Edit (if exists) |

## Steps

### Step 1: Update ApplicationDbContext
1. Add `DbSet<User> Users`

### Step 2: Update DataSeeder (if exists)
1. Seed admin user (Username: "admin", Email: "admin@englishlearning.com", Password: "Admin@123")

### Step 3: Create migration
1. `dotnet ef migrations add AddUsersTable --startup-project ..\EnglishLearning.WebAPI`

### Step 4: Apply migration
1. `dotnet ef database update --startup-project ..\EnglishLearning.WebAPI`

## Expected Code

```csharp
// ApplicationDbContext.cs — add:
public DbSet<User> Users => Set<User>();
```

## Verification

- [ ] Migration created successfully
- [ ] Users table exists in database
- [ ] Admin user seeded (if DataSeeder exists)

## Acceptance Criteria

- [ ] `DbSet<User> Users` added to ApplicationDbContext
- [ ] Migration created with Users table
- [ ] Username and Email columns are unique
- [ ] Migration applied successfully
- [ ] Admin user seeded (Username: "admin", Password: "Admin@123")
