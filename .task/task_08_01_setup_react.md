# Task 8.1: Setup React + TypeScript Project with Vite

## Description

Create a new React + TypeScript frontend project using Vite as the build tool.

## Priority
🔴 Critical — Frontend foundation

## Dependencies
- None (independent frontend project)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Web/` (entire project) | Create |

## Steps

### Step 1: Create Vite React + TypeScript project
1. Run: `npm create vite@latest EnglishLearning.Web -- --template react-ts`
2. Navigate into folder: `cd EnglishLearning.Web`
3. Install dependencies: `npm install`

### Step 2: Verify project structure
1. Check `src/` folder with App.tsx, main.tsx, etc.
2. Run `npm run dev` to verify it works

### Step 3: Add to solution (optional)
1. Consider adding to .sln or keeping separate

## Verification

- [ ] Project created successfully
- [ ] `npm run dev` starts without errors
- [ ] App renders in browser at http://localhost:5173

## Acceptance Criteria

- [ ] React + TypeScript project created with Vite
- [ ] `npm run dev` works
- [ ] Basic app renders in browser
- [ ] TypeScript configured (tsconfig.json)
