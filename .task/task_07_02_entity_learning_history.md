# Task 7.2: Entity — LearningHistory

## Description

Create the LearningHistory entity to track user learning activities.

## Priority
🔴 Critical — History tracking foundation

## Dependencies
- Task 7.1 (ActionType enum)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Domain/Entities/LearningHistory.cs` | Create |

## Steps

### Step 1: Create LearningHistory entity
1. Inherit from `BaseEntity`
2. Properties:
   - `UserId` (string, required, max 200)
   - `ActionType` (ActionType enum)
   - `TargetId` (Guid, the entity being acted upon)
   - `Details` (string?, nullable, max 1000)
   - `Score` (decimal?, nullable — for quiz results)
3. Factory method `Create(userId, actionType, targetId, details, score)`

## Expected Code

```csharp
namespace EnglishLearning.Domain.Entities;

public class LearningHistory : Common.BaseEntity
{
    public string UserId { get; set; } = string.Empty;
    public Enums.ActionType ActionType { get; set; }
    public Guid TargetId { get; set; }
    public string? Details { get; set; }
    public decimal? Score { get; set; }

    public static LearningHistory Create(string userId, Enums.ActionType actionType, Guid targetId, string? details = null, decimal? score = null)
    {
        return new LearningHistory
        {
            UserId = userId,
            ActionType = actionType,
            TargetId = targetId,
            Details = details,
            Score = score
        };
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Domain` — 0 errors ✅
- [x] LearningHistory entity has all properties ✅
- [x] Factory method exists ✅

## Acceptance Criteria

- [x] `LearningHistory` inherits from `BaseEntity` ✅
- [x] Properties: UserId, ActionType, TargetId, Details, Score ✅
- [x] Factory method `Create()` exists ✅
- [x] Domain project builds successfully ✅

---

## ✅ Completed: 2026-07-07

- **LearningHistory** — UserId, ActionType, TargetId, Details (nullable), Score (nullable)
- Factory method `Create(userId, actionType, targetId, details, score)`
- Build verified: 0 errors
