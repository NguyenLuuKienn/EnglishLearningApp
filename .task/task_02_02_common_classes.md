# Task 2.2: Create Common Infrastructure Classes

## Description

Create common utility classes in the Application layer: `Result<T>` for operation results and `PagedResult<T>` for paginated responses.

## Priority
🔴 Critical — Used by all CQRS handlers

## Dependencies
- Task 2.1 (Application dependencies)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Application/Common/Result.cs` | Create |
| `EnglishLearning.Application/Common/PagedResult.cs` | Create |

## Steps

### Step 1: Create Result<T> class
1. Create `Result<T>` generic class
2. Properties:
   - `Value` (T?) — success data
   - `IsSuccess` (bool) — operation status
   - `Error` (string?) — error message
   - `Errors` (IEnumerable<string>?) — detailed errors
3. Factory methods:
   - `Success(value)` — returns successful result
   - `Failure(error)` — returns failed result
   - `Failure(errors)` — returns failed result with multiple errors
4. Static properties:
   - `Ok` — empty success result (non-generic)

### Step 2: Create PagedResult<T> class
1. Create `PagedResult<T>` class
2. Properties:
   - `Items` (IReadOnlyList<T>)
   - `PageNumber` (int)
   - `PageSize` (int)
   - `TotalRecords` (int)
   - `TotalPages` (int) — computed from TotalRecords / PageSize
3. Factory method:
   - `Create(items, pageNumber, pageSize, totalRecords)`

## Expected Code

```csharp
// Result.cs
namespace EnglishLearning.Application.Common;

public class Result<T>
{
    public T? Value { get; set; }
    public bool IsSuccess { get; set; }
    public string? Error { get; set; }
    public IEnumerable<string>? Errors { get; set; }

    public static Result<T> Success(T value) => new() { Value = value, IsSuccess = true };
    public static Result<T> Failure(string error) => new() { IsSuccess = false, Error = error };
    public static Result<T> Failure(IEnumerable<string> errors) => new() { IsSuccess = false, Errors = errors };
}

public class Result
{
    public bool IsSuccess { get; set; }
    public string? Error { get; set; }
    public IEnumerable<string>? Errors { get; set; }

    public static Result Success() => new() { IsSuccess = true };
    public static Result Failure(string error) => new() { IsSuccess = false, Error = error };
    public static Result Failure(IEnumerable<string> errors) => new() { IsSuccess = false, Errors = errors };
}
```

```csharp
// PagedResult.cs
namespace EnglishLearning.Application.Common;

public class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; set; } = new List<T>();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }

    public static PagedResult<T> Create(IReadOnlyList<T> items, int pageNumber, int pageSize, int totalRecords)
    {
        return new PagedResult<T>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
        };
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Application` — 0 errors ✅
- [x] `Result<T>` has factory methods for Success and Failure ✅
- [x] `PagedResult<T>` correctly calculates TotalPages ✅

## Acceptance Criteria

- [x] `Result<T>` generic class with Value, IsSuccess, Error, Errors properties ✅
- [x] `Result` non-generic class for void operations ✅
- [x] Both have static factory methods: Success() and Failure() ✅
- [x] `PagedResult<T>` with Items, PageNumber, PageSize, TotalRecords, TotalPages ✅
- [x] `PagedResult<T>.Create()` factory method calculates TotalPages ✅
- [x] Application project builds successfully ✅

---

## ✅ Completed: 2026-07-06

- `Result<T>` — generic with Value, IsSuccess, Error, Errors + factory methods
- `Result` — non-generic for void operations + factory methods
- `PagedResult<T>` — Items, PageNumber, PageSize, TotalRecords, TotalPages + Create() factory
- Build verified: 0 errors
