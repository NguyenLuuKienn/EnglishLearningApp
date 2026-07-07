# Task 7.3: Update Quiz Entity

## Description

Add StartTime and EndTime properties to the Quiz entity for scheduling.

## Priority
🔴 Critical — Quiz scheduling support

## Dependencies
- None (independent)

## Files to Edit

| File | Action |
|------|--------|
| `EnglishLearning.Domain/Entities/Quiz.cs` | Edit |

## Steps

### Step 1: Add properties to Quiz entity
1. `StartTime` (DateTime?, nullable)
2. `EndTime` (DateTime?, nullable)

## Expected Code Changes

```csharp
// Add to Quiz entity:
public DateTime? StartTime { get; set; }
public DateTime? EndTime { get; set; }
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Domain` — 0 errors
- [ ] Quiz entity has StartTime and EndTime properties

## Acceptance Criteria

- [ ] `Quiz` entity has `StartTime` (DateTime?) property
- [ ] `Quiz` entity has `EndTime` (DateTime?) property
- [ ] Domain project builds successfully
