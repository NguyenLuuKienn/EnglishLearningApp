# Task 2.8: Create Application DependencyInjection Extension

## Description

Create the `AddApplication()` extension method for `IServiceCollection` that registers all Application layer services: MediatR, FluentValidation, and AutoMapper.

## Priority
🔴 Critical — Entry point for registering Application services in WebAPI

## Dependencies
- Task 2.1 (Application dependencies)
- Task 2.4 (Vocabulary CQRS — MediatR commands/queries)
- Task 2.5 (Quiz CQRS)
- Task 2.6 (QuizResult CQRS)
- Task 2.7 (AutoMapper profiles)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/DependencyInjection.cs` | Create |

## Steps

### Step 1: Create static class with extension method
1. Create `public static class DependencyInjection`
2. Add `AddApplication(this IServiceCollection services)` extension method

### Step 2: Register services
1. Register MediatR — scan the Application assembly for handlers
2. Register FluentValidation — scan the Application assembly for validators
3. Register AutoMapper — add the MappingsProfile

## Expected Code

```csharp
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register MediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        // Register FluentValidation
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        // Register AutoMapper
        services.AddAutoMapper(typeof(MappingsProfile).Assembly);

        return services;
    }
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.Application` — 0 errors
- [ ] `AddApplication()` method is discoverable from WebAPI
- [ ] MediatR is configured to scan the Application assembly
- [ ] FluentValidation is configured to scan the Application assembly
- [ ] AutoMapper is configured with the MappingsProfile assembly

## Acceptance Criteria

- [ ] `DependencyInjection` is a `public static class`
- [ ] `AddApplication()` extension method on `IServiceCollection`
- [ ] MediatR registered with assembly scanning
- [ ] FluentValidation registered with assembly scanning
- [ ] AutoMapper registered with MappingsProfile
- [ ] Application project builds successfully
