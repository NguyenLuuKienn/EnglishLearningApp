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

- [x] `DbSet<User> Users` added to ApplicationDbContext ✅
- [x] DataSeeder created with admin user ✅
- [ ] Migration created with Users table (user will run manually)
- [ ] Migration applied successfully (user will run manually)

## Acceptance Criteria

- [x] `DbSet<User> Users` added to ApplicationDbContext ✅
- [x] `DataSeeder.Seed()` called in `OnModelCreating` ✅
- [x] Admin user seeded (Username: "admin", Password: "Admin@123") ✅
- [ ] Migration created and applied (user will run manually)

---

## ✅ Completed: 2026-07-07

- **DataSeeder** — Seeds admin user: `admin` / `admin@englishlearning.com` / `Admin@123` (BCrypt hashed, Role: Admin)
- **ApplicationDbContext** — Added `DbSet<User> Users`, calls `DataSeeder.Seed(builder)` in `OnModelCreating`
- **Migration** — User will run manually:
  - `dotnet ef migrations add AddUsersTable --startup-project ..\EnglishLearning.WebAPI`
  - `dotnet ef database update --startup-project ..\EnglishLearning.WebAPI`
- Build verified: 0 errors
