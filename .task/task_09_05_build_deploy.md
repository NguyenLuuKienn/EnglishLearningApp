# Task 9.5: Build & Deploy Preparation

## Description

Prepare the project for production build and deployment.

## Priority
🟢 Medium — Deployment readiness

## Dependencies
- All Phase 9 tasks complete

## Steps

### Step 1: Backend build
1. `dotnet publish EnglishLearning.WebAPI -c Release -o ./publish`
2. Verify output

### Step 2: Frontend build
1. `npm run build` (in EnglishLearning.Web)
2. Verify `dist/` folder

### Step 3: Create deployment docs
1. `DEPLOYMENT.md` with instructions

## Expected DEPLOYMENT.md

```markdown
# Deployment Guide

## Backend
1. `dotnet publish EnglishLearning.WebAPI -c Release -o ./publish`
2. Configure connection string in appsettings.Production.json
3. Run: `dotnet EnglishLearning.WebAPI.dll`

## Frontend
1. `npm run build`
2. Serve `dist/` folder with Nginx or any static server
3. Configure reverse proxy for API calls

## Database
1. Run migrations: `dotnet ef database update`
```

## Acceptance Criteria

- [ ] Backend publishes successfully
- [ ] Frontend builds successfully
- [ ] DEPLOYMENT.md created
- [ ] Production-ready configuration
