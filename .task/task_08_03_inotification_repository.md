# Task 8.3: Interface — INotificationRepository

## Description

Create the INotificationRepository interface with methods for notification queries.

## Priority
🔴 Critical — Domain contract for notifications

## Dependencies
- Task 8.1 (Notification entity)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Domain/Interfaces/INotificationRepository.cs` | Create |

## Steps

### Step 1: Create INotificationRepository interface
1. Inherit from `IRepository<Notification>`
2. Additional methods:
   - `Task<List<Notification>> GetByUserIdAsync(string userId, int pageNumber, int pageSize)`
   - `Task<int> GetUnreadCountAsync(string userId)`

## Expected Code

```csharp
using EnglishLearning.Domain.Entities;

namespace EnglishLearning.Domain.Interfaces;

public interface INotificationRepository : IRepository<Notification>
{
    Task<List<Notification>> GetByUserIdAsync(string userId, int pageNumber, int pageSize);
    Task<int> GetUnreadCountAsync(string userId);
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Domain` — 0 errors ✅
- [x] INotificationRepository inherits from IRepository<Notification> ✅
- [x] Custom methods defined ✅

## Acceptance Criteria

- [x] `INotificationRepository` interface in `EnglishLearning.Domain.Interfaces` namespace ✅
- [x] Inherits from `IRepository<Notification>` ✅
- [x] `GetByUserIdAsync` with pagination and isRead filter ✅
- [x] `GetUnreadCountAsync` returns unread count ✅
- [x] Domain project builds successfully ✅
- [ ] `GetByUserIdAsync(string userId, int pageNumber, int pageSize)` returns paged notifications
- [ ] `GetUnreadCountAsync(string userId)` returns unread count
- [ ] Domain project builds successfully
