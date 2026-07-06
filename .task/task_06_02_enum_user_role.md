# Task 6.2: Enum — UserRole

## Description

Create the UserRole enum to define user roles (Admin, User) for authorization.

## Priority
🔴 Critical — Required for User entity

## Dependencies
- None (can be created in parallel with Task 6.1)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Domain/Enums/UserRole.cs` | Create |

## Steps

### Step 1: Create UserRole enum
1. Values: `User = 0`, `Admin = 1`

## Expected Code

```csharp
namespace EnglishLearning.Domain.Enums;

public enum UserRole
{
    User = 0,
    Admin = 1
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Domain` — 0 errors
- [ ] UserRole enum has User and Admin values

## Acceptance Criteria

- [ ] `UserRole` enum defined in `EnglishLearning.Domain.Enums` namespace
- [ ] Values: `User = 0`, `Admin = 1`
- [ ] Domain project builds successfully
