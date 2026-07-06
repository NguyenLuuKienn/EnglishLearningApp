# Task 2.7: Create AutoMapper Profiles

## Description

Create AutoMapper configuration to map Domain entities to Application DTOs.

## Priority
🟡 High — Used by query handlers for entity-to-DTO mapping

## Dependencies
- Task 2.1 (AutoMapper package installed)
- Task 2.3 (DTOs created)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/Common/Mappings.cs` | Create |

## Steps

### Step 1: Create MappingProfile
1. Create `MappingsProfile : Profile` class
2. Configure CreateMap for each entity → DTO pair:
   - `Vocabulary` → `VocabularyDto`
   - `Choice` → `ChoiceDto`
   - `Question` → `QuestionDto` (include nested ChoiceDto mapping)
   - `Quiz` → `QuizDto` (include nested QuestionDto mapping)
   - `QuizResult` → `QuizResultDto`

### Step 2: Register in profile constructor
1. All CreateMap calls in the constructor

## Expected Code

```csharp
using AutoMapper;

namespace EnglishLearning.Application.Common;

public class MappingsProfile : Profile
{
    public MappingsProfile()
    {
        // Vocabulary
        CreateMap<Domain.Entities.Vocabulary, DTOs.VocabularyDto>();

        // Choice
        CreateMap<Domain.Entities.Choice, DTOs.ChoiceDto>();

        // Question → QuestionDto (includes nested Choices)
        CreateMap<Domain.Entities.Question, DTOs.QuestionDto>()
            .ForMember(dest => dest.Choices, opt => opt.MapFrom(src => src.Choices));

        // Quiz → QuizDto (includes nested Questions)
        CreateMap<Domain.Entities.Quiz, DTOs.QuizDto>()
            .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));

        // QuizResult
        CreateMap<Domain.Entities.QuizResult, DTOs.QuizResultDto>();
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Application` — 0 errors ✅
- [x] All entity-to-DTO mappings are configured ✅
- [x] Nested mappings (Quiz → Questions → Choices) are properly configured ✅

## Acceptance Criteria

- [x] `MappingsProfile` class inherits from `Profile` ✅
- [x] Vocabulary → VocabularyDto mapping configured ✅
- [x] Choice → ChoiceDto mapping configured ✅
- [x] Question → QuestionDto mapping configured (with nested Choices) ✅
- [x] Quiz → QuizDto mapping configured (with nested Questions) ✅
- [x] QuizResult → QuizResultDto mapping configured ✅
- [x] Application project builds successfully ✅

---

## ✅ Completed: 2026-07-06

- `MappingsProfile` inherits from AutoMapper `Profile`
- 5 mappings configured:
  - `Vocabulary` → `VocabularyDto`
  - `Choice` → `ChoiceDto`
  - `Question` → `QuestionDto` (nested Choices)
  - `Quiz` → `QuizDto` (nested Questions)
  - `QuizResult` → `QuizResultDto`
- Build verified: 0 errors
