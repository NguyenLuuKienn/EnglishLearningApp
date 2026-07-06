# Task 5.2: Add Data Seeding

## Description

Add sample data seeding for Vocabulary, Quiz, Questions, and Choices. This provides initial data for testing the API.

## Priority
🟡 High — Provides test data

## Dependencies
- Task 5.1 (Initial migration applied)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Infrastructure/Persistence/DataSeeder.cs` | Create |
| `EnglishLearning.Infrastructure/Persistence/Configurations/QuizConfiguration.cs` | Edit (add seed call) |

## Steps

### Step 1: Create DataSeeder class
1. Create `static class DataSeeder`
2. Add static method `Seed(ModelBuilder builder)`
3. Seed Vocabulary data (20+ words with different difficulties)
4. Seed Quiz data (at least 2 quizzes)
5. Seed Questions with Choices for each quiz

### Step 2: Seed Vocabulary data
Create at least 20 vocabulary words:
- 7 Beginner words (e.g., "apple", "house", "happy")
- 7 Intermediate words (e.g., "accomplish", "demonstrate", "environment")
- 6 Advanced words (e.g., "ubiquitous", "ephemeral", "pragmatic")

### Step 3: Seed Quiz data
Create at least 2 quizzes:
- "Beginner English Quiz" — Difficulty: Beginner, TimeLimit: 10 min
- "Intermediate English Quiz" — Difficulty: Intermediate, TimeLimit: 15 min

### Step 4: Seed Questions and Choices
For each quiz, create 5 questions with 4 choices each (MultipleChoice type)

### Step 5: Call DataSeeder in OnModelCreating
1. In `ApplicationDbContext.OnModelCreating`, add `DataSeeder.Seed(builder)` at the end

## Expected Code

```csharp
// DataSeeder.cs
using Microsoft.EntityFrameworkCore;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Infrastructure.Persistence;

public static class DataSeeder
{
    public static void Seed(ModelBuilder builder)
    {
        // Seed Vocabularies
        builder.Entity<Vocabulary>().HasData(
            new Vocabulary { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Word = "Apple", Definition = "A round fruit with red or green skin and crisp flesh.", Example = "I eat an apple every day.", PartOfSpeech = "Noun", Difficulty = DifficultyLevel.Beginner },
            new Vocabulary { Id = Guid.Parse("11111111-1111-1111-1111-111111111112"), Word = "House", Definition = "A building for human habitation.", Example = "They live in a big house.", PartOfSpeech = "Noun", Difficulty = DifficultyLevel.Beginner },
            new Vocabulary { Id = Guid.Parse("11111111-1111-1111-1111-111111111113"), Word = "Happy", Definition = "Feeling or showing pleasure or contentment.", Example = "She felt happy after the exam.", PartOfSpeech = "Adjective", Difficulty = DifficultyLevel.Beginner },
            new Vocabulary { Id = Guid.Parse("11111111-1111-1111-1111-111111111114"), Word = "Run", Definition = "To move at a speed faster than a walk.", Example = "I run every morning.", PartOfSpeech = "Verb", Difficulty = DifficultyLevel.Beginner },
            new Vocabulary { Id = Guid.Parse("11111111-1111-1111-1111-111111111115"), Word = "Book", Definition = "A written or printed work consisting of pages.", Example = "I'm reading a good book.", PartOfSpeech = "Noun", Difficulty = DifficultyLevel.Beginner },
            new Vocabulary { Id = Guid.Parse("11111111-1111-1111-1111-111111111116"), Word = "Water", Definition = "A colorless, transparent, odorless liquid essential for life.", Example = "Please give me some water.", PartOfSpeech = "Noun", Difficulty = DifficultyLevel.Beginner },
            new Vocabulary { Id = Guid.Parse("11111111-1111-1111-1111-111111111117"), Word = "Friend", Definition = "A person whom one knows and with whom one has a bond of mutual affection.", Example = "She is my best friend.", PartOfSpeech = "Noun", Difficulty = DifficultyLevel.Beginner },

            new Vocabulary { Id = Guid.Parse("22222222-2222-2222-2222-222222222221"), Word = "Accomplish", Definition = "To achieve or complete successfully.", Example = "She accomplished her goal.", PartOfSpeech = "Verb", Difficulty = DifficultyLevel.Intermediate },
            new Vocabulary { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Word = "Demonstrate", Definition = "To clearly show the existence or truth of something.", Example = "The experiment demonstrates the theory.", PartOfSpeech = "Verb", Difficulty = DifficultyLevel.Intermediate },
            new Vocabulary { Id = Guid.Parse("22222222-2222-2222-2222-222222222223"), Word = "Environment", Definition = "The surroundings or conditions in which a person, animal, or plant lives.", Example = "We should protect the environment.", PartOfSpeech = "Noun", Difficulty = DifficultyLevel.Intermediate },
            new Vocabulary { Id = Guid.Parse("22222222-2222-2222-2222-222222222224"), Word = "Consequence", Definition = "A result or effect of an action or condition.", Example = "You must face the consequences.", PartOfSpeech = "Noun", Difficulty = DifficultyLevel.Intermediate },
            new Vocabulary { Id = Guid.Parse("22222222-2222-2222-2222-222222222225"), Word = "Opportunity", Definition = "A set of circumstances that makes it possible to do something.", Example = "This is a great opportunity.", PartOfSpeech = "Noun", Difficulty = DifficultyLevel.Intermediate },
            new Vocabulary { Id = Guid.Parse("22222222-2222-2222-2222-222222222226"), Word = "Challenge", Definition = "A task or situation that tests someone's abilities.", Example = "Learning a new language is a challenge.", PartOfSpeech = "Noun", Difficulty = DifficultyLevel.Intermediate },

            new Vocabulary { Id = Guid.Parse("33333333-3333-3333-3333-333333333331"), Word = "Ubiquitous", Definition = "Present, appearing, or found everywhere.", Example = "Smartphones have become ubiquitous.", PartOfSpeech = "Adjective", Difficulty = DifficultyLevel.Advanced },
            new Vocabulary { Id = Guid.Parse("33333333-3333-3333-3333-333333333332"), Word = "Ephemeral", Definition = "Lasting for a very short time.", Example = "Fame is often ephemeral.", PartOfSpeech = "Adjective", Difficulty = DifficultyLevel.Advanced },
            new Vocabulary { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Word = "Pragmatic", Definition = "Dealing with things sensibly and realistically.", Example = "We need a pragmatic approach.", PartOfSpeech = "Adjective", Difficulty = DifficultyLevel.Advanced },
            new Vocabulary { Id = Guid.Parse("33333333-3333-3333-3333-333333333334"), Word = "Ambiguous", Definition = "Open to more than one interpretation; not clear.", Example = "The statement was ambiguous.", PartOfSpeech = "Adjective", Difficulty = DifficultyLevel.Advanced },
            new Vocabulary { Id = Guid.Parse("33333333-3333-3333-3333-333333333335"), Word = "Resilient", Definition = "Able to withstand or recover quickly from difficult conditions.", Example = "Children are often resilient.", PartOfSpeech = "Adjective", Difficulty = DifficultyLevel.Advanced },
            new Vocabulary { Id = Guid.Parse("33333333-3333-3333-3333-333333333336"), Word = "Comprehensive", Definition = "Dealing with all or nearly all elements or aspects of something.", Example = "We need a comprehensive plan.", PartOfSpeech = "Adjective", Difficulty = DifficultyLevel.Advanced }
        );
    }
}
```

## Verification

- [ ] Run `dotnet ef migrations add SeedData --startup-project ..\EnglishLearning.WebAPI`
- [ ] Run `dotnet ef database update --startup-project ..\EnglishLearning.WebAPI`
- [ ] Vocabularies table has 20+ rows
- [ ] Words cover all 3 difficulty levels

## Acceptance Criteria

- [ ] `DataSeeder` class with `Seed(ModelBuilder builder)` method
- [ ] At least 20 Vocabulary entries seeded
- [ ] 7 Beginner, 7 Intermediate, 6 Advanced words
- [ ] Each word has Word, Definition, Example, PartOfSpeech, Difficulty
- [ ] DataSeeder.Seed() called in ApplicationDbContext.OnModelCreating
- [ ] New migration created and applied successfully
- [ ] Database contains seeded vocabulary data
