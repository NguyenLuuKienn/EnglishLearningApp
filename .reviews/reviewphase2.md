# 🔍 Code Review — Phase 2: EnglishLearning.Application

> **Ngày review:** 2026-07-06  
> **Reviewer:** GitHub Copilot  
> **Scope:** EnglishLearning.Application (Features, DTOs, Common, DependencyInjection)

---

## 📊 Tổng quan

| Tiêu chí | Đánh giá |
|---|---|
| Kiến trúc CQRS | ✅ Tốt — Tách rõ Commands / Queries theo MediatR |
| Folder structure | ✅ Tốt — `Features/Domain/Operation/Type/` rõ ràng |
| FluentValidation | ✅ Tốt — Validators tách riêng, đúng pattern |
| Result Pattern | ✅ Tốt — Dùng `Result<T>` thay vì throw exception |
| PagedResult | ✅ Tốt — Generic pagination wrapper tái sử dụng tốt |
| Record types | ✅ Tốt — Dùng `record` cho Commands/Queries |
| Using directives | ❌ Kém — Gọi namespace trực tiếp khắp nơi |
| AutoMapper | ❌ Kém — Đăng ký nhưng không dùng |
| Package versions | ⚠️ Cảnh báo — Dùng wildcard `*` |
| Logic business | ⚠️ Cảnh báo — `TotalQuestions` tính sai |

---

## 🔴 P0 — Nghiêm trọng (Phải sửa ngay)

### P0-1: Gọi namespace trực tiếp thay vì dùng `using`

**Mức độ:** Nghiêm trọng  
**Ảnh hưởng:** Tất cả files trong `Features/`

**Vấn đề:** Gần như tất cả files trong Features gọi namespace trực tiếp thay vì khai báo `using`, khiến code dài dòng, khó đọc, vi phạm C# convention.

**Ví dụ hiện tại:**
```csharp
// ❌ SAI — xuất hiện ở hầu hết files
IRequest<Common.Result<Guid>>
Common.Result<DTOs.VocabularyDto>.Failure(...)
Domain.Entities.Vocabulary
Domain.Enums.DifficultyLevel
Domain.Constants.VocabularyErrorMessages
DTOs.VocabularyDto
System.Linq.Expressions.Expression<Func<T, bool>>
```

**Cách sửa:** Thêm `using` vào đầu mỗi file:
```csharp
using EnglishLearning.Application.Common;
using EnglishLearning.Application.DTOs;
using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Constants;
using EnglishLearning.Domain.Interfaces;
using System.Linq.Expressions;
```

Rồi thay thế:

| Trước | Sau |
|---|---|
| `Common.Result<Guid>` | `Result<Guid>` |
| `DTOs.VocabularyDto` | `VocabularyDto` |
| `Domain.Entities.Vocabulary` | `Vocabulary` |
| `Domain.Enums.DifficultyLevel` | `DifficultyLevel` |
| `Domain.Constants.VocabularyErrorMessages` | `VocabularyErrorMessages` |
| `System.Linq.Expressions.Expression<Func<T, bool>>` | `Expression<Func<T, bool>>` |

**Files cần sửa (21 files):**
- `Features/Vocabulary/Commands/CreateVocabulary/CreateVocabularyCommand.cs`
- `Features/Vocabulary/Commands/CreateVocabulary/CreateVocabularyCommandHandler.cs`
- `Features/Vocabulary/Commands/UpdateVocabulary/UpdateVocabularyCommand.cs`
- `Features/Vocabulary/Commands/UpdateVocabulary/UpdateVocabularyCommandHandler.cs`
- `Features/Vocabulary/Commands/DeleteVocabulary/DeleteVocabularyCommand.cs`
- `Features/Vocabulary/Commands/DeleteVocabulary/DeleteVocabularyCommandHandler.cs`
- `Features/Vocabulary/Queries/GetVocabulary/GetVocabularyQuery.cs`
- `Features/Vocabulary/Queries/GetVocabulary/GetVocabularyQueryHandler.cs`
- `Features/Vocabulary/Queries/GetVocabularies/GetVocabulariesQuery.cs`
- `Features/Vocabulary/Queries/GetVocabularies/GetVocabulariesQueryHandler.cs`
- `Features/Quizzes/Commands/CreateQuiz/CreateQuizCommand.cs`
- `Features/Quizzes/Commands/CreateQuiz/CreateQuizCommandHandler.cs`
- `Features/Quizzes/Commands/UpdateQuiz/UpdateQuizCommand.cs`
- `Features/Quizzes/Commands/UpdateQuiz/UpdateQuizCommandHandler.cs`
- `Features/Quizzes/Commands/DeleteQuiz/DeleteQuizCommand.cs`
- `Features/Quizzes/Commands/DeleteQuiz/DeleteQuizCommandHandler.cs`
- `Features/Quizzes/Queries/GetQuiz/GetQuizQuery.cs`
- `Features/Quizzes/Queries/GetQuiz/GetQuizQueryHandler.cs`
- `Features/Quizzes/Queries/GetQuizzes/GetQuizzesQuery.cs`
- `Features/Quizzes/Queries/GetQuizzes/GetQuizzesQueryHandler.cs`
- `Features/QuizResults/Commands/SubmitQuizResult/SubmitQuizResultCommand.cs`
- `Features/QuizResults/Commands/SubmitQuizResult/SubmitQuizResultCommandHandler.cs`
- `Features/QuizResults/Queries/GetQuizResult/GetQuizResultQuery.cs`
- `Features/QuizResults/Queries/GetQuizResult/GetQuizResultQueryHandler.cs`
- `Features/QuizResults/Queries/GetUserQuizResults/GetUserQuizResultsQuery.cs`
- `Features/QuizResults/Queries/GetUserQuizResults/GetUserQuizResultsQueryHandler.cs`

---

### P0-2: AutoMapper được đăng ký nhưng KHÔNG BAO GIỜ được sử dụng

**Mức độ:** Nghiêm trọng  
**Ảnh hưởng:** `DependencyInjection.cs`, `MappingsProfile.cs`, tất cả Query Handlers

**Vấn đề:**
- `MappingsProfile` được định nghĩa đầy đủ với các mapping Entity → DTO
- `AddAutoMapper` được gọi trong `DependencyInjection.cs`
- Nhưng **không có Handler nào inject `IMapper`** — tất cả đều map thủ công bằng `new DTOs.XxxDto { ... }`

**2 lựa chọn:**

**Option A (Khuyên dùng):** Inject `IMapper` vào các Handler:
```csharp
public class GetVocabularyQueryHandler : IRequestHandler<GetVocabularyQuery, Result<VocabularyDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetVocabularyQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<VocabularyDto>> Handle(GetVocabularyQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Vocabularies.GetByIdAsync(request.Id);
        if (entity == null)
            return Result<VocabularyDto>.Failure(VocabularyErrorMessages.NotFound);

        return Result<VocabularyDto>.Success(_mapper.Map<VocabularyDto>(entity));
    }
}
```

**Option B:** Xóa AutoMapper hoàn toàn nếu không dùng:
- Xóa `AddAutoMapper` trong `DependencyInjection.cs`
- Xóa `MappingsProfile.cs`
- Xóa 2 package references trong `.csproj`

---

### P0-3: Manual mapping lặp lại ở nhiều nơi (DRY Violation)

**Mức độ:** Nghiêm trọng  
**Ảnh hưởng:** 6 Query Handler files

**Vấn đề:** Các Query Handler đều có đoạn map thủ công giống hệt nhau. Nếu DTO thay đổi, phải sửa 6 chỗ.

**Các file map thủ công:**
| File | Mapping |
|---|---|
| `GetVocabularyQueryHandler.cs` | `Vocabulary` → `VocabularyDto` |
| `GetVocabulariesQueryHandler.cs` | `Vocabulary` → `VocabularyDto` (list) |
| `GetQuizQueryHandler.cs` | `Quiz` → `QuizDto` (nested Questions + Choices) |
| `GetQuizzesQueryHandler.cs` | `Quiz` → `QuizDto` (list) |
| `GetQuizResultQueryHandler.cs` | `QuizResult` → `QuizResultDto` |
| `GetUserQuizResultsQueryHandler.cs` | `QuizResult` → `QuizResultDto` (list) |

**Cách sửa:** Dùng AutoMapper (xem P0-2) hoặc tạo extension methods cho mapping.

---

## 🟡 P1 — Quan trọng (Nên sửa sớm)

### P1-1: Package versions dùng wildcard `*`

**Mức độ:** Quan trọng  
**File:** `EnglishLearning.Application.csproj`

**Vấn đề:**
```xml
<!-- ❌ SAI -->
<PackageReference Include="MediatR" Version="*" />
<PackageReference Include="FluentValidation" Version="*" />
<PackageReference Include="FluentValidation.DependencyInjectionExtensions" Version="*" />
<PackageReference Include="AutoMapper" Version="*" />
<PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="*" />
```

Wildcard `*` lấy version mới nhất mỗi khi build → **không thể tái lập build**, gây lỗi production.

**Cách sửa:** Pin version cụ thể:
```xml
<PackageReference Include="MediatR" Version="12.4.1" />
<PackageReference Include="FluentValidation" Version="11.10.0" />
<PackageReference Include="FluentValidation.DependencyInjectionExtensions" Version="11.10.0" />
<PackageReference Include="AutoMapper" Version="13.0.1" />
<PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="12.0.1" />
```

---

### P1-2: `SubmitQuizResultCommandHandler` tính `TotalQuestions` sai

**Mức độ:** Quan trọng  
**File:** `Features/QuizResults/Commands/SubmitQuizResult/SubmitQuizResultCommandHandler.cs`

**Vấn đề:**
```csharp
// ❌ SAI — TotalQuestions = số câu user trả lời (có thể bỏ sót)
var result = Domain.Entities.QuizResult.Create(
    request.QuizId,
    request.UserId,
    request.Answers.Count,    // ← Sai: dùng số câu user gửi lên
    correctAnswers,
    request.DurationMinutes
);
```

Nếu user bỏ sót câu hỏi, `TotalQuestions` sẽ không đúng với số câu thực tế trong quiz → **tính score sai**.

**Cách sửa:**
```csharp
// ✅ ĐÚNG — Lấy từ quiz thực tế
var result = Domain.Entities.QuizResult.Create(
    request.QuizId,
    request.UserId,
    quiz.Questions.Count,     // ← Số câu thực sự trong quiz
    correctAnswers,
    request.DurationMinutes
);
```

---

## 🟢 P2 — Cải thiện (Nên làm)

### P2-1: `Expression<Func<T, bool>>` cast kiểu quá dài dòng

**Mức độ:** Cải thiện code quality  
**Ảnh hưởng:** `GetVocabulariesQueryHandler.cs`, `GetQuizzesQueryHandler.cs`, `GetUserQuizResultsQueryHandler.cs`

**Vấn đề:**
```csharp
// ❌ Khó đọc
var predicate = request.Difficulty.HasValue
    ? (System.Linq.Expressions.Expression<System.Func<Domain.Entities.Vocabulary, bool>>)(v => v.Difficulty == request.Difficulty.Value)
    : null;
```

**Cách sửa:** Dùng `using` + helper method:
```csharp
// ✅ Dễ đọc hơn
using System.Linq.Expressions;

private static Expression<Func<Vocabulary, bool>>? BuildPredicate(DifficultyLevel? difficulty)
{
    if (!difficulty.HasValue) return null;
    return v => v.Difficulty == difficulty.Value;
}
```

---

### P2-2: Thiếu Validator cho Delete commands

**Mức độ:** Best practice  
**Ảnh hưởng:** `DeleteVocabularyCommand`, `DeleteQuizCommand`

**Vấn đề:** Không có Validator cho Delete commands → không validate `Id != Guid.Empty`.

**Cách sửa:** Tạo validators:
```csharp
public class DeleteVocabularyCommandValidator : AbstractValidator<DeleteVocabularyCommand>
{
    public DeleteVocabularyCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}
```

---

### P2-3: `Result<T>` có thể cải thiện với implicit conversion

**Mức độ:** Cải thiện DX  
**File:** `Common/Result.cs`

**Gợi ý:**
```csharp
// Thêm implicit conversion
public static implicit operator Result<T>(T value) => Success(value);

// Thêm alias method
public static Result<T> Ok(T value) => Success(value);
```

---

### P2-4: `BaseEntity` không tự động update `UpdatedAt`

**Mức độ:** Cải thiện  
**File:** `Domain/Common/BaseEntity.cs`

**Vấn đề:** `UpdatedAt` chỉ được set trong constructor. Các Handler phải set thủ công:
```csharp
entity.UpdatedAt = DateTime.UtcNow; // UpdateVocabularyCommandHandler, UpdateQuizCommandHandler
```

**Gợi ý:** Để EF Core handle qua `SaveChangesAsync` override trong `DbContext` (Phase 3 - Infrastructure).

---

## 📋 Checklist sửa lỗi

- [x] **P0-1:** Thêm `using` directives vào tất cả 26 files trong Features/
- [x] **P0-2:** Quyết định: dùng AutoMapper (inject IMapper) HOẶC xóa hoàn toàn
- [x] **P0-3:** Thay manual mapping bằng AutoMapper (6 files)
- [x] **P1-1:** Pin package versions trong `.csproj`
- [x] **P1-2:** Sửa `TotalQuestions` trong `SubmitQuizResultCommandHandler`
- [x] **P2-1:** Refactor Expression predicate thành helper methods
- [x] **P2-2:** Thêm Validator cho DeleteVocabulary và DeleteQuiz
- [x] **P2-3:** Cải thiện `Result<T>` với implicit conversion (tùy chọn)
- [ ] **P2-4:** Plan EF Core `SaveChangesAsync` override cho `UpdatedAt` (Phase 3)

---

## 💡 Ghi chú thêm

1. **Pattern CQRS:** Đã áp dụng đúng — Commands thay đổi state, Queries chỉ đọc dữ liệu
2. **MediatR Pipeline:** Chưa có behavior cho logging, caching, performance timing — có thể thêm sau
3. **Error handling:** Dùng `Result<T>` pattern tốt, nhưng nên có middleware ở WebAPI layer để map `Result<T>` → HTTP status codes
4. **CancellationToken:** Đã được pass qua tất cả Handler — ✅ tốt
5. **Nullable reference types:** Đã enable trong `.csproj` — ✅ tốt

---

## ✅ Round 2 — Các issue bổ sung đã fix

> **Ngày fix:** 2026-07-06

| # | File | Issue | Trạng thái |
|---|---|---|---|
| 1 | `GetVocabulariesQuery.cs` | Thiếu using, dùng namespace trực tiếp | ✅ Fixed |
| 2 | `GetVocabulariesQueryHandler.cs` | Chưa inject IMapper, manual mapping, Expression cast dài | ✅ Fixed |
| 3 | `VocabularyDto.cs` | Dùng `Domain.Enums.DifficultyLevel` | ✅ Fixed |
| 4 | `QuestionDto.cs` | Dùng `Domain.Enums.QuestionType` | ✅ Fixed |
| 5 | `QuizDto.cs` | Dùng `Domain.Enums.DifficultyLevel` | ✅ Fixed |
| 6 | `MappingsProfile.cs` | Dùng `Domain.Entities.Xxx`, `DTOs.XxxDto` | ✅ Fixed |
| 7 | `DependencyInjection.cs` | Dùng `Common.MappingsProfile` | ✅ Fixed |
| 8 | `CreateVocabularyCommandHandler.cs` | Inject IMapper không dùng | ✅ Removed |
| 9 | `UpdateVocabularyCommandHandler.cs` | Inject IMapper không dùng | ✅ Removed |
| 10 | `DeleteVocabularyCommandHandler.cs` | Inject IMapper không dùng | ✅ Removed |
| 11 | `CreateQuizCommandHandler.cs` | Inject IMapper không dùng | ✅ Removed |
| 12 | `UpdateQuizCommandHandler.cs` | Inject IMapper không dùng | ✅ Removed |
| 13 | `DeleteQuizCommandHandler.cs` | Inject IMapper không dùng | ✅ Removed |
| 14 | `CreateVocabularyCommandValidator.cs` | Dùng relative namespace | ✅ Fixed |
| 15 | `UpdateVocabularyCommandValidator.cs` | Dùng relative namespace | ✅ Fixed |
| 16 | `CreateQuizCommandValidator.cs` | Dùng relative namespace | ✅ Fixed |
| 17 | `UpdateQuizCommandValidator.cs` | Dùng relative namespace | ✅ Fixed |
| 18 | `SubmitQuizResultCommandValidator.cs` | Dùng relative namespace | ✅ Fixed |
