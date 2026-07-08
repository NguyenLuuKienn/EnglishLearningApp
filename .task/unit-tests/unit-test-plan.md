# Unit Test Plan — EnglishLearningApp

> **Target:** 95% code coverage across all 4 layers
> **Framework:** xUnit + Moq + FluentAssertions + coverlet
> **Strategy:** No hardcode — all tests use proper mocking, test data builders, and assertions

---

## Test Project Structure

```
EnglishLearnningApp.UnitTest/
├── Domain/
│   ├── Entities/
│   │   ├── BaseEntityTests.cs
│   │   ├── UserTests.cs
│   │   ├── QuizResultTests.cs
│   │   ├── QuizAssignmentTests.cs
│   │   ├── LearningHistoryTests.cs
│   │   ├── NotificationTests.cs
│   │   └── LeaderboardTests.cs
│   └── Enums/
│       └── EnumTests.cs
│
├── Application/
│   ├── Common/
│   │   ├── ResultTests.cs
│   │   └── PagedResultTests.cs
│   ├── Auth/
│   │   ├── RegisterCommandHandlerTests.cs
│   │   ├── LoginCommandHandlerTests.cs
│   │   ├── RefreshTokenCommandHandlerTests.cs
│   │   └── GetProfileQueryHandlerTests.cs
│   ├── Vocabulary/
│   │   ├── CreateVocabularyCommandHandlerTests.cs
│   │   ├── UpdateVocabularyCommandHandlerTests.cs
│   │   ├── DeleteVocabularyCommandHandlerTests.cs
│   │   ├── GetVocabularyQueryHandlerTests.cs
│   │   ├── GetVocabulariesQueryHandlerTests.cs
│   │   ├── CreateVocabularyCommandValidatorTests.cs
│   │   ├── UpdateVocabularyCommandValidatorTests.cs
│   │   └── DeleteVocabularyCommandValidatorTests.cs
│   ├── Quizzes/
│   │   ├── CreateQuizCommandHandlerTests.cs
│   │   ├── UpdateQuizCommandHandlerTests.cs
│   │   ├── DeleteQuizCommandHandlerTests.cs
│   │   ├── GetQuizQueryHandlerTests.cs
│   │   ├── GetQuizzesQueryHandlerTests.cs
│   │   ├── GetQuizForTakeQueryHandlerTests.cs
│   │   ├── GetQuestionQueryHandlerTests.cs
│   │   ├── CreateQuizCommandValidatorTests.cs
│   │   ├── UpdateQuizCommandValidatorTests.cs
│   │   └── DeleteQuizCommandValidatorTests.cs
│   ├── Assignments/
│   │   ├── AssignQuizCommandHandlerTests.cs
│   │   ├── CancelAssignmentCommandHandlerTests.cs
│   │   ├── GetUserAssignmentsQueryHandlerTests.cs
│   │   ├── GetAssignmentByIdQueryHandlerTests.cs
│   │   └── GetActiveAssignmentsQueryHandlerTests.cs
│   ├── QuizResults/
│   │   ├── SubmitQuizResultCommandHandlerTests.cs
│   │   ├── GetQuizResultQueryHandlerTests.cs
│   │   ├── GetUserQuizResultsQueryHandlerTests.cs
│   │   └── SubmitQuizResultCommandValidatorTests.cs
│   ├── History/
│   │   ├── RecordHistoryCommandHandlerTests.cs
│   │   └── GetUserHistoryQueryHandlerTests.cs
│   ├── Leaderboard/
│   │   ├── UpdateLeaderboardCommandHandlerTests.cs
│   │   ├── GetLeaderboardQueryHandlerTests.cs
│   │   └── GetUserRankQueryHandlerTests.cs
│   ├── Notifications/
│   │   ├── MarkNotificationReadCommandHandlerTests.cs
│   │   └── GetUserNotificationsQueryHandlerTests.cs
│   └── Mapping/
│       └── MappingsProfileTests.cs
│
├── Infrastructure/
│   ├── Services/
│   │   ├── TokenServiceTests.cs
│   │   ├── NotificationServiceTests.cs
│   │   ├── CheckQuizAssignmentsJobTests.cs
│   │   └── SendAssignmentNotificationsJobTests.cs
│   └── Repositories/
│       ├── RepositoryTests.cs
│       ├── UserRepositoryTests.cs
│       ├── QuizRepositoryTests.cs
│       ├── QuizResultRepositoryTests.cs
│       ├── QuizAssignmentRepositoryTests.cs
│       ├── LearningHistoryRepositoryTests.cs
│       ├── LeaderboardRepositoryTests.cs
│       ├── NotificationRepositoryTests.cs
│       └── VocabularyRepositoryTests.cs
│
├── WebAPI/
│   ├── Controllers/
│   │   ├── AuthControllerTests.cs
│   │   ├── VocabulariesControllerTests.cs
│   │   ├── QuizzesControllerTests.cs
│   │   ├── AssignmentsControllerTests.cs
│   │   ├── QuizResultsControllerTests.cs
│   │   ├── HistoryControllerTests.cs
│   │   ├── LeaderboardControllerTests.cs
│   │   └── NotificationsControllerTests.cs
│   └── Middleware/
│       └── ExceptionMiddlewareTests.cs
│
├── Helpers/
│   ├── TestDataBuilder.cs
│   ├── MockHelper.cs
│   └── InMemoryDbContextFactory.cs
│
└── EnglishLearnning.UnitTest.csproj
```

---

## Test Strategy

### Domain Layer (No mocking needed)
- **Factory methods**: Verify default values, score calculations, edge cases
- **Entity constructors**: Verify collections initialized, timestamps set
- **Enums**: Verify all values exist and are correct

### Application Layer (Moq for dependencies)
- **Command Handlers**: Mock repositories/UnitOfWork, test happy path + error paths
- **Query Handlers**: Mock repositories + IMapper, test data transformation
- **Validators**: Test valid/invalid inputs, boundary values
- **Result/PagedResult**: Test factory methods, TotalPages calculation
- **MappingsProfile**: Test AutoMapper mappings (Entity → DTO)

### Infrastructure Layer (InMemory/SQLite DbContext)
- **Repositories**: Use EF Core InMemory database, test CRUD + custom queries
- **TokenService**: Test JWT generation, validation, claims
- **NotificationService**: Mock repositories, test send logic
- **Jobs**: Mock dependencies, test scheduling logic

### WebAPI Layer (Moq for IMediator)
- **Controllers**: Mock IMediator, test HTTP status codes, request/response mapping
- **ExceptionMiddleware**: Test exception handling, error responses

---

## Coverage Target Breakdown

| Layer | Target Tests | Target Coverage |
|-------|-------------|-----------------|
| Domain | ~30 tests | 100% |
| Application | ~120 tests | 95% |
| Infrastructure | ~60 tests | 90% |
| WebAPI | ~50 tests | 95% |
| **Total** | **~260 tests** | **95%** |

---

## Task Tracking

| # | Task | File | Status |
|---|------|------|--------|
| UT-00 | Setup test project (dependencies, structure) | `.csproj`, `Helpers/` | ✅ Done |
| UT-01 | Domain — BaseEntity, Entity factory methods | `Domain/Entities/` | ✅ Done |
| UT-02 | Domain — Enums | `Domain/Enums/` | ✅ Done |
| UT-03 | Application — Result, PagedResult | `Application/Common/` | ✅ Done |
| UT-04 | Application — Auth handlers | `Application/Auth/` | ✅ Done |
| UT-05 | Application — Vocabulary handlers + validators | `Application/Vocabulary/` | ✅ Done |
| UT-06 | Application — Quiz handlers + validators | `Application/Quizzes/` | ✅ Done |
| UT-07 | Application — Assignment handlers | `Application/Assignments/` | ✅ Done |
| UT-08 | Application — QuizResult handlers + validators | `Application/QuizResults/` | ✅ Done |
| UT-09 | Application — History handlers | `Application/History/` | ✅ Done |
| UT-10 | Application — Leaderboard handlers | `Application/Leaderboard/` | ✅ Done |
| UT-11 | Application — Notification handlers | `Application/Notifications/` | ✅ Done |
| UT-12 | Application — AutoMapper mappings | `Application/Mapping/` | ✅ Done |
| UT-13 | Infrastructure — TokenService | `Infrastructure/Services/` | ✅ Done |
| UT-14 | Infrastructure — NotificationService | `Infrastructure/Services/` | ✅ Done |
| UT-15 | Infrastructure — Background Jobs | `Infrastructure/Services/` | ✅ Done |
| UT-16 | Infrastructure — Repositories (InMemory DB) | `Infrastructure/Repositories/` | ⬜ Pending |
| UT-17 | WebAPI — AuthController | `WebAPI/Controllers/` | ✅ Done |
| UT-18 | WebAPI — VocabulariesController | `WebAPI/Controllers/` | ✅ Done |
| UT-19 | WebAPI — QuizzesController | `WebAPI/Controllers/` | ✅ Done |
| UT-20 | WebAPI — AssignmentsController | `WebAPI/Controllers/` | ✅ Done |
| UT-21 | WebAPI — QuizResultsController | `WebAPI/Controllers/` | ✅ Done |
| UT-22 | WebAPI — History, Leaderboard, Notifications | `WebAPI/Controllers/` | ✅ Done |
| UT-23 | WebAPI — ExceptionMiddleware | `WebAPI/Middleware/` | ⬜ Pending |
| UT-24 | Final — Run coverage report, fix gaps | N/A | ⬜ Pending |
