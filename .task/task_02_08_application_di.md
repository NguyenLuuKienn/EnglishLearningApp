# Task 2.8: Create Application DependencyInjection Extension

## Description

Create the `AddApplication()` extension method for `IServiceCollection` that registers all Application layer services: MediatR, FluentValidation, and AutoMapper.

## Priority
🔴 Critical — Entry point for registering Application services in WebAPI

## Status
✅ Completed

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
using EnglishLearning.Application.Common;
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
        services.AddAutoMapper(typeof(MappingsProfile));

        return services;
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Application` — 0 errors
- [x] `AddApplication()` method is discoverable from WebAPI
- [x] MediatR is configured to scan the Application assembly
- [x] FluentValidation is configured to scan the Application assembly
- [x] AutoMapper is configured with the MappingsProfile assembly

## Acceptance Criteria

- [x] `DependencyInjection` is a `public static class`
- [x] `AddApplication()` extension method on `IServiceCollection`
- [x] MediatR registered with assembly scanning
- [x] FluentValidation registered with assembly scanning
- [x] AutoMapper registered with MappingsProfile
- [x] Application project builds successfully

## ✅ Completed: 2026-07-06