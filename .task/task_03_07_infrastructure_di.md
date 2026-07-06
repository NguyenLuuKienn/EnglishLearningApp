# Task 3.7: Update Infrastructure DependencyInjection

## Description

Update the `DependencyInjection.cs` file to provide the `AddInfrastructure()` extension method that registers DbContext, repositories, and UnitOfWork with the DI container.

## Priority
🔴 Critical — Entry point for registering Infrastructure services in WebAPI

## Dependencies
- Task 3.2 (DbContext)
- Task 3.4 (Base Repository)
- Task 3.5 (Specific Repositories)
- Task 3.6 (Unit of Work)

## Files to Edit

| File | Action |
|------|--------|
| `EnglishLearning.Infrastructure/DependencyInjection.cs` | Rewrite |

## Steps

### Step 1: Rewrite DependencyInjection.cs
1. Change from `internal class` to `public static class`
2. Add `AddInfrastructure(this IServiceCollection services, IConfiguration configuration)` extension method

### Step 2: Register services
1. Register `ApplicationDbContext` with SQL Server using connection string from configuration
2. Register `IUnitOfWork` → `UnitOfWork` as Scoped

## Expected Code

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Infrastructure.Persistence;
using EnglishLearning.Infrastructure.UnitOfWork;

namespace EnglishLearning.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Register DbContext with SQL Server
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Register Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Infrastructure` — 0 errors ✅
- [x] `AddInfrastructure()` accepts `IConfiguration` parameter ✅
- [x] DbContext is registered with SQL Server provider ✅
- [x] IUnitOfWork is registered as Scoped ✅

## Acceptance Criteria

- [x] `DependencyInjection` is a `public static class` ✅
- [x] `AddInfrastructure()` extension method on `IServiceCollection` ✅
- [x] Method accepts `IConfiguration` parameter ✅
- [x] `ApplicationDbContext` registered with `UseSqlServer` ✅
- [x] Connection string read from `DefaultConnection` key ✅
- [x] `IUnitOfWork` registered as Scoped with `UnitOfWork` implementation ✅
- [x] Infrastructure project builds successfully ✅

---

## ✅ Completed: 2026-07-06

- `DependencyInjection` là `public static class`
- `AddInfrastructure(IServiceCollection, IConfiguration)` extension method:
  - Đọc connection string `DefaultConnection` từ `IConfiguration`
  - Register `ApplicationDbContext` với `UseSqlServer`
  - Register `IUnitOfWork` → `UnitOfWork` (Scoped)
- Build verified: 0 errors
