# Task 6.15: Configure JWT + DI + [Authorize]

## Description

Register Auth services (IUserRepository, ITokenService) in DI, update Program.cs, and add [Authorize] to existing controllers.

## Priority
🔴 Critical — Wire up authentication

## Dependencies
- Task 6.10 (UserRepository), Task 6.11 (TokenService), Task 6.14 (AuthController)

## Files to Edit

| File | Action |
|------|--------|
| `EnglishLearning.Infrastructure/DependencyInjection.cs` | Edit |
| `EnglishLearning.WebAPI/Program.cs` | Edit |
| `EnglishLearning.WebAPI/Controllers/VocabulariesController.cs` | Edit |
| `EnglishLearning.WebAPI/Controllers/QuizzesController.cs` | Edit |
| `EnglishLearning.WebAPI/Controllers/QuizResultsController.cs` | Edit |

## Steps

### Step 1: Update Infrastructure DI
1. Register `IUserRepository` → `UserRepository`
2. Register `ITokenService` → `TokenService`

### Step 2: Update Program.cs
1. Ensure JWT authentication is properly configured
2. Add `[Authorize]` as default filter (optional)

### Step 3: Add [Authorize] to existing controllers
1. Add `[Authorize]` attribute to VocabulariesController, QuizzesController, QuizResultsController
2. Keep GET endpoints accessible, protect POST/PUT/DELETE

## Expected Code

```csharp
// Infrastructure/DependencyInjection.cs — add:
using EnglishLearning.Application.Interfaces;
using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Infrastructure.Repositories;
using EnglishLearning.Infrastructure.Services;

// In AddInfrastructure method:
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<ITokenService, TokenService>();
```

## Verification

- [x] Run `dotnet build` — 0 errors ✅
- [x] All repositories registered in DI ✅
- [x] Existing controllers have [Authorize] attribute ✅
- [x] ExceptionMiddleware handles UnauthorizedAccessException ✅

## Acceptance Criteria

- [x] `IVocabularyRepository` registered as Scoped ✅
- [x] `IQuizRepository` registered as Scoped ✅
- [x] `IQuizResultRepository` registered as Scoped ✅
- [x] `IUserRepository` registered as Scoped ✅
- [x] `ITokenService` registered as Scoped ✅
- [x] Existing controllers protected with `[Authorize]` ✅
- [x] `UnauthorizedAccessException` → 401 in ExceptionMiddleware ✅
- [x] Full solution builds successfully ✅

---

## ✅ Completed: 2026-07-07

- **DependencyInjection.cs** — Registered all repositories:
  - `IVocabularyRepository → VocabularyRepository` (Scoped)
  - `IQuizRepository → QuizRepository` (Scoped)
  - `IQuizResultRepository → QuizResultRepository` (Scoped)
  - `IUserRepository → UserRepository` (Scoped)
  - `ITokenService → TokenService` (Scoped)
- **Existing controllers** — Added `[Authorize]` to `VocabulariesController`, `QuizzesController`, `QuizResultsController`
- **ExceptionMiddleware** — Added `UnauthorizedAccessException → 401 Unauthorized` handling
- **Program.cs** — JWT authentication already configured (no changes needed)
- Build verified: 0 errors
