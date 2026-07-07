# Task 8.13: Notification Migration

## Description

Create EF Core migration for Notifications table and Hangfire tables.

## Priority
🔴 Critical — Database schema for notifications

## Dependencies
- Task 8.8 (NotificationRepository + Configuration), Task 8.10 (Hangfire setup)

## Files to Edit

| File | Action |
|------|--------|
| `EnglishLearning.Infrastructure/Persistence/ApplicationDbContext.cs` | Edit |

## Steps

### Step 1: Update ApplicationDbContext
1. Add `DbSet<Notification> Notifications`

### Step 2: Create migration
1. `dotnet ef migrations add AddNotificationsAndHangfire --startup-project ..\EnglishLearning.WebAPI`

### Step 3: Apply migration
1. `dotnet ef database update --startup-project ..\EnglishLearning.WebAPI`

## Expected Code

```csharp
// ApplicationDbContext.cs — add:
public DbSet<Notification> Notifications => Set<Notification>();
```

## Verification

- [ ] Migration created successfully
- [ ] Notifications table exists in database
- [ ] Hangfire tables exist (Hangfire.Scheduled, Hangfire.JobQueue, etc.)

## Acceptance Criteria

- [ ] `DbSet<Notification> Notifications` added to ApplicationDbContext
- [ ] Migration created with Notifications table
- [ ] Hangfire tables created
- [ ] Migration applied successfully
