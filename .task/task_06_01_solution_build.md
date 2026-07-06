# Task 6.1: Solution-Wide Build

## Description

Perform a complete solution-wide build to verify all projects compile without errors.

## Priority
🔴 Critical — Final verification before running

## Dependencies
ALL previous tasks (Phase 1-5 complete)

## Steps

### Step 1: Clean solution
1. Run `dotnet clean` from solution root

### Step 2: Restore packages
1. Run `dotnet restore` from solution root

### Step 3: Build solution
1. Run `dotnet build` from solution root
2. Verify 0 errors, 0 warnings (or acceptable warnings only)

### Step 4: Verify each project
1. Check `EnglishLearning.Domain` — builds
2. Check `EnglishLearning.Application` — builds
3. Check `EnglishLearning.Infrastructure` — builds
4. Check `EnglishLearning.WebAPI` — builds

## Verification Commands

```powershell
# From solution root
dotnet clean
dotnet restore
dotnet build
```

## Verification

- [ ] `dotnet clean` succeeds
- [ ] `dotnet restore` succeeds — all packages restored
- [ ] `dotnet build` succeeds — 0 errors
- [ ] All 4 projects build successfully
- [ ] No circular dependencies

## Acceptance Criteria

- [ ] Solution builds with 0 errors
- [ ] EnglishLearning.Domain builds
- [ ] EnglishLearning.Application builds
- [ ] EnglishLearning.Infrastructure builds
- [ ] EnglishLearning.WebAPI builds
- [ ] All project references are resolved
- [ ] All NuGet packages are restored
