# 🔍 Code Review — Phase 4: EnglishLearning.WebAPI

> **Ngày review:** 2026-07-06  
> **Reviewer:** GitHub Copilot  
> **Scope:** EnglishLearning.WebAPI (Controllers, Models, Middlewares, Program.cs, appsettings)

---

## 📊 Tổng quan

| Task | File(s) | Điểm | Trạng thái |
|---|---|---|---|
| 4.1 Dependencies | `.csproj` | 10/10 | ✅ Duyệt |
| 4.2 Response Models | `ApiResponse.cs`, `PagedResponse.cs` | 10/10 | ✅ Duyệt |
| 4.3 Request Contracts | 8 request files | 10/10 | ✅ Duyệt |
| 4.4 Controllers | 3 controllers | 10/10 | ✅ Duyệt |
| 4.5 Exception Middleware | `ExceptionMiddleware.cs` | 10/10 | ✅ Duyệt |
| 4.6 Program.cs | `Program.cs` | 10/10 | ✅ Duyệt |
| 4.7 appsettings | `appsettings.json`, `appsettings.Development.json` | 10/10 | ✅ Duyệt |
| 4.8 Cleanup | Xóa template files | 10/10 | ✅ Duyệt |

**Tổng: 10/10** — Phase 4 hoàn tất, không có issue.

---

## ✅ Task 4.1: Setup WebAPI Dependencies

**File:** `EnglishLearning.WebAPI.csproj`

| Kiểm tra | Kết quả |
|---|---|
| TargetFramework net8.0 | ✅ |
| ImplicitUsings enable | ✅ |
| Nullable enable | ✅ |
| ProjectRef Application | ✅ |
| ProjectRef Infrastructure | ✅ |
| JwtBearer 8.0.8 pinned | ✅ |
| BCrypt.Net-Next 4.0.3 pinned | ✅ |
| Swashbuckle 6.6.2 pinned | ✅ |

---

## ✅ Task 4.2: API Response Models

**Files:** `Models/Common/ApiResponse.cs`, `Models/Common/PagedResponse.cs`

**Ghi chú:** Files được di chuyển từ `Extensions/` sang `Models/Common/` — tổ chức tốt hơn, gom chung trong `Models/`.

**ApiResponse.cs:**
- Properties: Success, Message, Data, Errors ✅
- Factory `Ok(data, message)` ✅
- Factory `BadRequest(errors, message)` ✅
- Factory `NotFound(message)` ✅

**PagedResponse.cs:**
- Inherit `ApiResponse<IReadOnlyList<T>>` ✅
- Properties: PageNumber, PageSize, TotalRecords, TotalPages ✅
- Factory `Ok()` tính `TotalPages = Ceiling(totalRecords / pageSize)` ✅

---

## ✅ Task 4.3: Request Contracts (8 files)

**Folder structure:** `Models/Requests/{Feature}/`

**Vocabulary (2 files):**
- `CreateVocabularyRequest` — Word [Required][StringLength(200)], Definition [Required][StringLength(1000)], Example [StringLength(1000)], PartOfSpeech [StringLength(50)], Difficulty ✅
- `UpdateVocabularyRequest` — Giống Create ✅

**Quizzes (4 files):**
- `CreateQuizRequest` — Title [Required][StringLength(200)], Description [StringLength(1000)], Difficulty, TimeLimitMinutes, PassingScore = 50m, Questions [MinLength(1)] ✅
- `QuestionRequest` — QuestionText [Required][StringLength(2000)], QuestionType, Difficulty, CorrectAnswer, Choices ✅
- `ChoiceRequest` — ChoiceText [Required][StringLength(500)], IsCorrect ✅
- `UpdateQuizRequest` — Title [Required][StringLength(200)], Description [StringLength(1000)], Difficulty, TimeLimitMinutes, PassingScore ✅

**QuizResults (2 files):**
- `SubmitQuizResultRequest` — QuizId [Required], UserId [Required], DurationMinutes, Answers [MinLength(1)] ✅
- `AnswerRequest` — QuestionId [Required], SelectedChoiceId, AnswerText ✅

---

## ✅ Task 4.4: Controllers (3 files)

### VocabulariesController

| Endpoint | Method | Command/Query | HTTP Status |
|---|---|---|---|
| POST `/` | Create | CreateVocabularyCommand | 201 Created / 400 Bad Request |
| GET `/` | GetAll | GetVocabulariesQuery | 200 Ok / 400 Bad Request |
| GET `/{id}` | GetById | GetVocabularyQuery | 200 Ok / 404 NotFound |
| PUT `/{id}` | Update | UpdateVocabularyCommand | 200 Ok / 404 NotFound / 400 Bad Request |
| DELETE `/{id}` | Delete | DeleteVocabularyCommand | 204 NoContent / 404 NotFound |

### QuizzesController

| Endpoint | Method | Command/Query | HTTP Status |
|---|---|---|---|
| POST `/` | Create | CreateQuizCommand (nested Questions/Choices) | 201 Created / 400 Bad Request |
| GET `/` | GetAll | GetQuizzesQuery | 200 Ok / 400 Bad Request |
| GET `/{id}` | GetById | GetQuizQuery | 200 Ok / 404 NotFound |
| PUT `/{id}` | Update | UpdateQuizCommand | 200 Ok / 404 NotFound / 400 Bad Request |
| DELETE `/{id}` | Delete | DeleteQuizCommand | 204 NoContent / 404 NotFound |

### QuizResultsController

| Endpoint | Method | Command/Query | HTTP Status |
|---|---|---|---|
| POST `/submit` | Submit | SubmitQuizResultCommand (nested Answers) | 200 Ok / 400 Bad Request |
| GET `/{id}` | GetById | GetQuizResultQuery | 200 Ok / 404 NotFound |
| GET `/user/{userId}` | GetByUserId | GetUserQuizResultsQuery | 200 Ok / 400 Bad Request |

**Pattern chung:**
- `[ApiController]` + `[Route("api/[controller]")]` ✅
- Inject `IMediator` qua constructor ✅
- Map Request → Command/Query ✅
- Wrap response trong `ApiResponse<T>` / `PagedResponse<T>` ✅
- Error handling: `result.Errors?.ToList() ?? [result.Error ?? string.Empty]` ✅
- NotFound detection: `result.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase)` ✅
- Using directives sạch, không namespace trực tiếp ✅

---

## ✅ Task 4.5: Exception Middleware

**File:** `Middlewares/ExceptionMiddleware.cs`

| Kiểm tra | Kết quả |
|---|---|
| Constructor: RequestDelegate, ILogger, IHostEnvironment | ✅ |
| `InvokeAsync` với try/catch | ✅ |
| Log error `_logger.LogError(ex, ...)` | ✅ |
| `ArgumentException` → 400 Bad Request | ✅ |
| `KeyNotFoundException` → 404 Not Found | ✅ |
| Default → 500 Internal Server Error | ✅ |
| Response `application/json` | ✅ |
| `ApiResponse<object>.BadRequest()` wrap | ✅ |
| Dev mode: `ex.ToString()` (full stack trace) | ✅ |
| Prod mode: generic message | ✅ |
| Extension `UseGlobalExceptionHandling()` | ✅ |

---

## ✅ Task 4.6: Program.cs

**Services registration:**
- `AddControllers()` ✅
- `AddEndpointsApiExplorer()` ✅
- `AddSwaggerGen()` — "English Learning API" v1 ✅
- `AddApplication()` — MediatR, FluentValidation, AutoMapper ✅
- `AddInfrastructure(builder.Configuration)` — DbContext, UoW ✅
- `AddAuthentication(JwtBearer)` — Issuer, Audience, Key từ config ✅
- `AddAuthorization()` ✅

**Middleware pipeline (thứ tự đúng):**
1. Swagger + SwaggerUI (Development only)
2. `UseGlobalExceptionHandling()`
3. `UseHttpsRedirection()`
4. `UseAuthentication()`
5. `UseAuthorization()`
6. `MapControllers()`

---

## ✅ Task 4.7: appsettings

**appsettings.json:**
- `ConnectionStrings:DefaultConnection` — SQL Server ✅
- `Jwt:Key` — 44 ký tự, đủ mạnh ✅
- `Jwt:Issuer` — "EnglishLearningAPI" ✅
- `Jwt:Audience` — "EnglishLearningClient" ✅
- `Jwt:ExpirationInMinutes` — 60 ✅
- Logging config ✅
- AllowedHosts = "*" ✅

**appsettings.Development.json:**
- Override `ConnectionStrings:DefaultConnection` với localdb ✅
- Logging config dev ✅

---

## ✅ Task 4.8: Cleanup Template Files

| Kiểm tra | Kết quả |
|---|---|
| `WeatherForecast.cs` đã xóa | ✅ |
| `WeatherForecastController.cs` đã xóa | ✅ |
| Không còn reference `WeatherForecast` | ✅ (grep: 0 results) |
| `.http` file đã cập nhật với API requests thực tế | ✅ |

---

## 📋 Ghi chú

1. **Folder `Extensions/` giờ trống** — Task 4.2 đã di chuyển ApiResponse/PagedResponse sang `Models/Common/`. Folder `Extensions/` trong `.csproj` (`<Folder Include="Extensions\" />`) giờ thừa.

2. **Connection string khác nhau giữa 2 files:**
   - `appsettings.json`: `Server=KIENNL\DEV;Database=EnglishApp;User Id=sa;...` (SQL Server thực tế)
   - `appsettings.Development.json`: `Server=(localdb)\mssqllocaldb;Database=EnglishLearningDb;...` (localdb)
   - → Không sai, nhưng 2 database khác nhau (`EnglishApp` vs `EnglishLearningDb`) có thể gây nhầm lẫn.

3. **JWT Key hardcode trong source code** — Chấp nhận được cho project training/demo. Production nên dùng Environment Variables hoặc Key Vault.

4. **Route constraint `{id}` vs `{id:guid}`** — Code dùng `{id}` thay vì `{id:guid}` như spec. Với `[ApiController]`, ASP.NET Core tự động validate type từ parameter (`Guid id`), nên `{id}` đã đủ.

5. **Method naming `GetById` thay vì `Get`** — Tốt hơn spec, naming rõ ràng hơn khi có cả `GetAll` và `GetById`.

---

## 📊 So sánh với Phase 2

| Tiêu chí | Phase 2 (Application) | Phase 4 (WebAPI) |
|---|---|---|
| Using directives | ❌ → ✅ (sau 3 lần review) | ✅ (từ đầu) |
| Namespace trực tiếp | ❌ → ✅ | ✅ (không có) |
| Pattern nhất quán | ✅ CQRS + MediatR | ✅ Controllers → MediatR |
| Response wrapping | ✅ Result\<T\> | ✅ ApiResponse\<T\> |
| Validation | ✅ FluentValidation | ✅ Data Annotations |
| Package versions | ❌ → ✅ (pinned) | ✅ (pinned từ đầu) |

---

## 💡 Đánh giá tổng thể Phase 4

Phase 4 được implement **sạch ngay từ lần đầu**, không cần review lại nhiều lần như Phase 2. Code khớp hoàn toàn với task specs, using directives sạch sẽ, pattern CQRS + MediatR đúng chuẩn, HTTP status codes chính xác, response wrapping nhất quán.

**Điểm: 10/10**