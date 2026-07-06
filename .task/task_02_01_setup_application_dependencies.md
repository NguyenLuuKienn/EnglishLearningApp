# Task 2.1: Setup Application Layer Dependencies

## Description

Configure the Application layer project file with required project references and NuGet packages for CQRS, validation, and mapping.

## Priority
🔴 Critical — Foundation for Application layer

## Dependencies
- Task 1.1 - Task 1.8 (All Domain layer tasks complete)

## Files to Edit

| File | Action |
|------|--------|
| `EnglishLearning.Application/EnglishLearning.Application.csproj` | Edit |

## Steps

### Step 1: Add project reference to Domain
1. Open `.csproj` file
2. Add `<ProjectReference>` to `EnglishLearning.Domain`

### Step 2: Add NuGet packages
Add the following packages:
- `MediatR` — Mediator pattern for CQRS
- `FluentValidation` — Validation framework
- `FluentValidation.DependencyInjectionExtensions` — DI integration for FluentValidation
- `AutoMapper` — Object-to-object mapping

### Step 3: Verify build
1. Run `dotnet build EnglishLearning.Application`
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
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="MediatR" Version="*" />
    <PackageReference Include="FluentValidation" Version="*" />
    <PackageReference Include="FluentValidation.DependencyInjectionExtensions" Version="*" />
    <PackageReference Include="AutoMapper" Version="*" />
  </ItemGroup>
</Project>
```

## Verification

- [x] Run `dotnet build EnglishLearning.Application` — 0 errors ✅
- [x] All NuGet packages are restored ✅
- [x] Project reference to Domain is working ✅

## Acceptance Criteria

- [x] `EnglishLearning.Application` references `EnglishLearning.Domain` ✅
- [x] MediatR package is installed ✅
- [x] FluentValidation packages are installed ✅
- [x] AutoMapper package is installed ✅
- [x] Application project builds successfully ✅

---

## ✅ Completed: 2026-07-06

- Project reference to `EnglishLearning.Domain`
- NuGet packages: MediatR, FluentValidation, FluentValidation.DependencyInjectionExtensions, AutoMapper
- Build verified: 0 errors
