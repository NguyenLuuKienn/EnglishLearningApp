# Task 1.0: Create BaseEntity

## Description

Create a `BaseEntity` abstract class that contains common properties for all entities: `Id`, `CreatedAt`, `UpdatedAt`, `CreatedBy`, `UpdatedBy`. This eliminates duplication across all entity classes.

## Priority
🔴 Critical — Foundation for all entities

## Dependencies
None (first task)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Domain/Common/BaseEntity.cs` | Create |

## Steps

### Step 1: Create BaseEntity abstract class
1. Create `Common/` folder in Domain layer
2. Create `public abstract class BaseEntity`
3. Add common properties:
   - `Id` (Guid, primary key, auto-generated)
   - `CreatedAt` (DateTime, set on creation)
   - `UpdatedAt` (DateTime, set on update)
   - `CreatedBy` (string?, user who created)
   - `UpdatedBy` (string?, user who updated)

### Step 2: Add constructor
1. Parameterless constructor that initializes `Id`, `CreatedAt`, `UpdatedAt`

## Expected Code

```csharp
namespace EnglishLearning.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Domain` — 0 errors ✅
- [x] `BaseEntity` is `public abstract class` ✅
- [x] Constructor auto-generates Id and timestamps ✅
- [x] Domain project builds successfully ✅

## Acceptance Criteria

- [x] `BaseEntity` is a `public abstract class` ✅
- [x] Has properties: Id, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy ✅
- [x] Constructor initializes Id, CreatedAt, UpdatedAt ✅
- [x] Domain project builds successfully ✅

---

## ✅ Completed: 2026-07-06

- `Common/BaseEntity.cs` — created with Id, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy
- Constructor auto-initializes Id (Guid.NewGuid()) and timestamps (DateTime.UtcNow)
- Build verified: 0 errors
