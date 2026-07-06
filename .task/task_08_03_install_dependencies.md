# Task 8.3: Install Frontend Dependencies

## Description

Install required npm packages for routing, data fetching, HTTP client, and icons.

## Priority
🔴 Critical — Core dependencies

## Dependencies
- Task 8.1 (React project)

## Files to Edit

| File | Action |
|------|--------|
| `EnglishLearning.Web/package.json` | Auto-updated |

## Steps

### Step 1: Install dependencies
1. `npm install react-router-dom` — Routing
2. `npm install @tanstack/react-query` — Data fetching & caching
3. `npm install axios` — HTTP client
4. `npm install lucide-react` — Icons
5. `npm install -D @types/node` — Node types for env vars

## Verification

- [ ] All packages installed successfully
- [ ] `npm run dev` still works
- [ ] No TypeScript errors

## Acceptance Criteria

- [ ] `react-router-dom` installed
- [ ] `@tanstack/react-query` installed
- [ ] `axios` installed
- [ ] `lucide-react` installed
- [ ] `@types/node` installed (dev)
- [ ] Project builds without errors
