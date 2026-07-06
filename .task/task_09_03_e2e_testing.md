# Task 9.3: End-to-End Testing

## Description

Test the complete flow: Register → Login → Browse Vocabulary → Take Quiz → View Result → Check Leaderboard.

## Priority
🔴 Critical — Final verification

## Dependencies
- All Phase 6-8 tasks complete

## Steps

### Step 1: Start both servers
1. Backend: `dotnet run --project EnglishLearning.WebAPI`
2. Frontend: `npm run dev` (in EnglishLearning.Web)

### Step 2: Test flow
1. Register a new user
2. Login with the user
3. Browse vocabulary list
4. View vocabulary detail (flashcard)
5. Start a quiz
6. Answer questions and submit
7. View quiz result
8. Check leaderboard
9. Check learning history
10. View profile

### Step 3: Verify
- [ ] Registration works
- [ ] Login works, token stored
- [ ] Vocabulary list loads
- [ ] Quiz taking works with timer
- [ ] Quiz result shows score
- [ ] Leaderboard updates
- [ ] History records activity

## Acceptance Criteria

- [ ] Full user flow works end-to-end
- [ ] No console errors in browser
- [ ] No server errors in backend logs
- [ ] All pages load correctly
- [ ] Auth protection works (redirect to login)
