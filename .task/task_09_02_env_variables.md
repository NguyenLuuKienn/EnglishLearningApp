# Task 9.2: Frontend Environment Variables

## Description

Setup environment variables for the frontend project.

## Priority
🔴 Critical — API URL configuration

## Dependencies
- Task 8.1 (React project)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Web/.env` | Create |
| `EnglishLearning.Web/.env.development` | Create |
| `EnglishLearning.Web/.env.production` | Create |

## Expected Code

```bash
# .env.development
VITE_API_URL=http://localhost:5055

# .env.production
VITE_API_URL=https://your-api-domain.com
```

## Verification

- [ ] Environment files created
- [ ] `import.meta.env.VITE_API_URL` works in code

## Acceptance Criteria

- [ ] `.env.development` with VITE_API_URL pointing to localhost
- [ ] `.env.production` with placeholder URL
- [ ] API service uses `import.meta.env.VITE_API_URL`
