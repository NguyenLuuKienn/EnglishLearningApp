# Task 7.4: Interface — IQuizAssignmentRepository

## Description

Create the IQuizAssignmentRepository interface with methods for quiz assignment queries.

## Priority
🔴 Critical — Domain contract for assignments

## Dependencies
- Task 7.1 (QuizAssignment entity)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Domain/Interfaces/IQuizAssignmentRepository.cs` | Create |

## Steps

### Step 1: Create IQuizAssignmentRepository interface
1. Inherit from `IRepository<QuizAssignment>`
2. Additional methods:
   - `Task<List<QuizAssignment>> GetByUserIdAsync(string userId)`
   - `Task<List<QuizAssignment>> GetByRoleAsync(UserRole role)`
   - `Task<List<QuizAssignment>> GetActiveAssignmentsAsync()`
   - `Task<List<QuizAssignment>> GetExpiringSoonAsync(DateTime before)`

## Expected Code

```csharp
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Domain.Interfaces;

public interface IQuizAssignmentRepository : IRepository<QuizAssignment>
{
    Task<List<QuizAssignment>> GetByUserIdAsync(string userId);
    Task<List<QuizAssignment>> GetByRoleAsync(UserRole role);
    Task<List<QuizAssignment>> GetActiveAssignmentsAsync();
    Task<List<QuizAssignment>> GetExpiringSoonAsync(DateTime before);
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Domain` — 0 errors ✅
- [x] IQuizAssignmentRepository inherits from IRepository<QuizAssignment> ✅
- [x] Custom methods defined ✅

## Acceptance Criteria

- [x] `IQuizAssignmentRepository` interface in `EnglishLearning.Domain.Interfaces` namespace ✅
- [x] Inherits from `IRepository<QuizAssignment>` ✅
- [x] `GetByUserIdAsync(string userId)` returns assignments for specific user ✅
- [x] `GetByRoleAsync(UserRole role)` returns assignments for role ✅
- [x] `GetActiveAssignmentsAsync()` returns currently active assignments ✅
- [x] `GetExpiringSoonAsync(DateTime before)` returns assignments expiring before date ✅
- [x] Domain project builds successfully ✅

---

## ✅ Completed: 2026-07-07

- **IQuizAssignmentRepository** — `GetByUserIdAsync`, `GetByRoleAsync`, `GetActiveAssignmentsAsync`, `GetExpiringSoonAsync`
- Namespace: `EnglishLearning.Domain.Interfaces`
- Build verified: 0 errors
