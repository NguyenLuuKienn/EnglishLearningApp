# Task 4.1: Setup WebAPI Dependencies

## Description

Configure the WebAPI project file with required project references and NuGet packages for JWT authentication and password hashing.

## Priority
🔴 Critical — Foundation for WebAPI layer

## Dependencies
- Task 3.1 - Task 3.7 (All Infrastructure layer tasks complete)
- Task 2.1 - Task 2.8 (All Application layer tasks complete)

## Files to Edit

| File | Action |
|------|--------|
| `EnglishLearning.WebAPI/EnglishLearning.WebAPI.csproj` | Edit |

## Steps

### Step 1: Add project references
1. Add `<ProjectReference>` to `EnglishLearning.Application`
2. Add `<ProjectReference>` to `EnglishLearning.Infrastructure`

### Step 2: Add NuGet packages
- `Microsoft.AspNetCore.Authentication.JwtBearer` — JWT authentication middleware
- `BCrypt.Net-Next` — Password hashing (for future auth feature)

### Step 3: Verify build
1. Run `dotnet build EnglishLearning.WebAPI`
2. Verify no errors

## Expected Code

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\EnglishLearning.Application\EnglishLearning.Application.csproj" />
    <ProjectReference Include="..\EnglishLearning.Infrastructure\EnglishLearning.Infrastructure.csproj" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="*" />
    <PackageReference Include="BCrypt.Net-Next" Version="*" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.6.2" />
  </ItemGroup>
</Project>
```

## Verification

- [ ] Run `dotnet build EnglishLearning.WebAPI` — 0 errors
- [ ] All NuGet packages are restored
- [ ] Project references to Application and Infrastructure are working

## Acceptance Criteria

- [ ] `EnglishLearning.WebAPI` references `EnglishLearning.Application`
- [ ] `EnglishLearning.WebAPI` references `EnglishLearning.Infrastructure`
- [ ] JWT Bearer authentication package is installed
- [ ] BCrypt.Net-Next package is installed
- [ ] Swashbuckle (Swagger) package is still present
- [ ] WebAPI project builds successfully
