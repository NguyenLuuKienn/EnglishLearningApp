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

- [ ] Run `dotnet build` — 0 errors
- [ ] IUserRepository and ITokenService registered
- [ ] Existing controllers have [Authorize] attribute

## Acceptance Criteria

- [ ] `IUserRepository` registered as Scoped
- [ ] `ITokenService` registered as Scoped
- [ ] Existing controllers protected with `[Authorize]`
- [ ] GET endpoints remain accessible (or protected as needed)
- [ ] Full solution builds successfully
