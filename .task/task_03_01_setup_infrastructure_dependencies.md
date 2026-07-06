# Task 3.1: Setup Infrastructure Dependencies

## Description

Configure the Infrastructure layer project file with required project references and NuGet packages for Entity Framework Core and database provider.

## Priority
🔴 Critical — Foundation for Infrastructure layer

## Dependencies
- Task 2.1 - Task 2.8 (All Application layer tasks complete)
- Task 1.1 - Task 1.8 (All Domain layer tasks complete)

## Files to Edit

| File | Action |
|------|--------|
| `EnglishLearning.Infrastructure/EnglishLearning.Infrastructure.csproj` | Edit |

## Steps

### Step 1: Add project references
1. Add `<ProjectReference>` to `EnglishLearning.Domain`
2. Add `<ProjectReference>` to `EnglishLearning.Application`

### Step 2: Add NuGet packages
- `Microsoft.EntityFrameworkCore` — EF Core runtime
- `Microsoft.EntityFrameworkCore.SqlServer` — SQL Server provider
- `Microsoft.EntityFrameworkCore.Tools` — EF Core tools (design-time)

### Step 3: Verify build
1. Run `dotnet build EnglishLearning.Infrastructure`
2. Verify no errors

## Expected Code

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\EnglishLearning.Domain\EnglishLearning.Domain.csproj" />
    <ProjectReference Include="..\EnglishLearning.Application\EnglishLearning.Application.csproj" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.8" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.8" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.8">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>
</Project>
```

## Verification

- [x] Run `dotnet build EnglishLearning.Infrastructure` — 0 errors ✅
- [x] All NuGet packages are restored ✅
- [x] Project references to Domain and Application are working ✅

## Acceptance Criteria

- [x] `EnglishLearning.Infrastructure` references `EnglishLearning.Domain` ✅
- [x] `EnglishLearning.Infrastructure` references `EnglishLearning.Application` ✅
- [x] EF Core packages are installed ✅
- [x] SQL Server provider is installed ✅
- [x] EF Core Tools are installed ✅
- [x] Infrastructure project builds successfully ✅

---

## ✅ Completed: 2026-07-06

- Project references: `EnglishLearning.Domain` + `EnglishLearning.Application`
- NuGet packages installed:
  - `Microsoft.EntityFrameworkCore` 8.0.8
  - `Microsoft.EntityFrameworkCore.SqlServer` 8.0.8
  - `Microsoft.EntityFrameworkCore.Tools` 8.0.8 (design-time)
- Build verified: 0 errors
