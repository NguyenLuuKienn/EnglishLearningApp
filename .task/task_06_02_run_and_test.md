# Task 6.2: Run Application & Test API

## Description

Run the application and test all API endpoints via Swagger UI to verify everything works correctly.

## Priority
🔴 Critical — Final end-to-end verification

## Dependencies
- Task 6.1 (Solution builds successfully)
- Task 5.1 (Database migrated)
- Task 5.2 (Data seeded)

## Steps

### Step 1: Run the application
1. Run `dotnet run` from `EnglishLearning.WebAPI` directory
2. Verify application starts without errors
3. Note the URL (e.g., `https://localhost:5001`)

### Step 2: Open Swagger UI
1. Navigate to `/swagger` in browser
2. Verify Swagger UI loads with all endpoints listed

### Step 3: Test Vocabulary endpoints
1. `GET /api/vocabularies` — should return paged list of seeded vocabularies
2. `GET /api/vocabularies/{id}` — should return single vocabulary
3. `POST /api/vocabularies` — create new vocabulary
4. `PUT /api/vocabularies/{id}` — update existing vocabulary
5. `DELETE /api/vocabularies/{id}` — delete vocabulary

### Step 4: Test Quiz endpoints
1. `GET /api/quizzes` — should return paged list of quizzes
2. `GET /api/quizzes/{id}` — should return quiz with questions and choices
3. `POST /api/quizzes` — create new quiz with questions and choices
4. `PUT /api/quizzes/{id}` — update quiz
5. `DELETE /api/quizzes/{id}` — delete quiz

### Step 5: Test QuizResult endpoints
1. `POST /api/quizresults/submit` — submit quiz with answers, verify auto-grading
2. `GET /api/quizresults/{id}` — get result by Id
3. `GET /api/quizresults/user/{userId}` — get user's results

### Step 6: Verify auto-grading
1. Create a quiz with known correct answers
2. Submit answers with some correct and some incorrect
3. Verify score is calculated correctly (percentage)

## Verification Commands

```powershell
# From EnglishLearning.WebAPI directory
dotnet run
```

## Verification

- [ ] Application starts without errors
- [ ] Swagger UI loads at `/swagger`
- [ ] All endpoints are listed in Swagger
- [ ] GET /api/vocabularies returns seeded data
- [ ] POST /api/vocabularies creates new vocabulary
- [ ] GET /api/quizzes returns quizzes
- [ ] POST /api/quizzes creates quiz with questions and choices
- [ ] POST /api/quizresults/submit calculates correct score
- [ ] GET /api/quizresults/user/{userId} returns user results

## Acceptance Criteria

- [ ] Application runs successfully
- [ ] Swagger UI is accessible
- [ ] Vocabulary CRUD operations work (Create, Read, Update, Delete)
- [ ] Quiz CRUD operations work (Create with Questions/Choices, Read, Update, Delete)
- [ ] Quiz submission works with auto-grading
- [ ] Score calculation is correct
- [ ] Quiz results are stored and retrievable
- [ ] Paged responses return correct PageNumber, PageSize, TotalRecords
- [ ] Error responses return proper ApiResponse format
