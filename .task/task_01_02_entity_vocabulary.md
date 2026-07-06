# Task 1.2: Implement Vocabulary Entity

## Description

Implement the Vocabulary entity with all properties, navigation properties, and basic validation. This entity represents a word in the English learning system.

## Priority
🔴 Critical — Core entity used across the system

## Dependencies
- Task 1.0 (BaseEntity)
- Task 1.1 (DifficultyLevel enum)

## Files to Edit

| File | Action |
|------|--------|
| `EnglishLearning.Domain/Entities/Vocabulary.cs` | Rewrite |

## Steps

### Step 1: Define entity properties
1. Change `internal class` to `public class`
2. Inherit from `BaseEntity` (auto gets: `Id`, `CreatedAt`, `UpdatedAt`, `CreatedBy`, `UpdatedBy`)
3. Add core properties:
   - `Word` (string, required, max 200 chars)
   - `Definition` (string, required, max 1000 chars)
   - `Example` (string, optional, max 1000 chars)
   - `PartOfSpeech` (string, optional, max 50 chars) — noun, verb, adjective, etc.
   - `Difficulty` (DifficultyLevel enum)

### Step 2: Add navigation properties
1. Add `ICollection<Question> Questions` — words can appear in multiple questions

## Expected Code

```csharp
namespace EnglishLearning.Domain.Entities;

public class Vocabulary : Common.BaseEntity
{
    public string Word { get; set; } = string.Empty;
    public string Definition { get; set; } = string.Empty;
    public string? Example { get; set; }
    public string? PartOfSpeech { get; set; }
    public Enums.DifficultyLevel Difficulty { get; set; }

    // Navigation
    public ICollection<Question> Questions { get; set; } = new List<Question>();
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Domain` — 0 errors ✅
- [x] Entity has all required properties ✅
- [x] Inherits from BaseEntity ✅

## Acceptance Criteria

- [x] `Vocabulary` is a `public class` inheriting from `BaseEntity` ✅
- [x] Has properties: Word, Definition, Example, PartOfSpeech, Difficulty ✅
- [x] Inherits from BaseEntity: Id, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy ✅
- [x] Has navigation property: Questions (ICollection<Question>) ✅
- [x] Domain project builds successfully ✅

---

## ✅ Completed: 2026-07-06

- Inherits from `BaseEntity` (Id, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy)
- Properties: Word, Definition, Example, PartOfSpeech, Difficulty
- Navigation: `ICollection<Question> Questions`
- Entity-only (no business logic — handled by Application layer services)
