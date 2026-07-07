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

- [ ] Run `dotnet build EnglishLearning.Domain` — 0 errors
- [ ] IQuizAssignmentRepository inherits from IRepository<QuizAssignment>
- [ ] Custom methods defined

## Acceptance Criteria

- [ ] `IQuizAssignmentRepository` interface in `EnglishLearning.Domain.Interfaces` namespace
- [ ] Inherits from `IRepository<QuizAssignment>`
- [ ] `GetByUserIdAsync(string userId)` returns assignments for specific user
- [ ] `GetByRoleAsync(UserRole role)` returns assignments for role
- [ ] `GetActiveAssignmentsAsync()` returns currently active assignments
- [ ] `GetExpiringSoonAsync(DateTime before)` returns assignments expiring before date
- [ ] Domain project builds successfully
