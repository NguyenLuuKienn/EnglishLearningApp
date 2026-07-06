# Task 4.2: Create API Response Models

## Description

Create standardized API response models (`ApiResponse<T>` and `PagedResponse<T>`) that wrap all API responses in a consistent format.

## Priority
🔴 Critical — Standardizes all API responses

## Dependencies
- Task 4.1 (WebAPI dependencies)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.WebAPI/Extensions/ApiResponse.cs` | Create |
| `EnglishLearning.WebAPI/Extensions/PagedResponse.cs` | Create |

## Steps

### Step 1: Create ApiResponse<T>
1. Properties:
   - `Success` (bool)
   - `Message` (string)
   - `Data` (T?)
   - `Errors` (List<string>?)
2. Static factory methods:
   - `Ok(data, message?)` — success response
   - `BadRequest(errors, message?)` — error response
   - `NotFound(message)` — not found response

### Step 2: Create PagedResponse<T>
1. Inherit from `ApiResponse<IReadOnlyList<T>>` or contain paged data
2. Additional properties:
   - `PageNumber` (int)
   - `PageSize` (int)
   - `TotalRecords` (int)
   - `TotalPages` (int)
3. Static factory method:
   - `Ok(items, pageNumber, pageSize, totalRecords, message?)`

## Expected Code

```csharp
// ApiResponse.cs
namespace EnglishLearning.WebAPI.Extensions;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }

    public static ApiResponse<T> Ok(T data, string message = "Success")
    {
        return new ApiResponse<T> { Success = true, Message = message, Data = data };
    }

    public static ApiResponse<T> BadRequest(List<string> errors, string message = "Validation failed")
    {
        return new ApiResponse<T> { Success = false, Message = message, Errors = errors };
    }

    public static ApiResponse<T> NotFound(string message = "Resource not found")
    {
        return new ApiResponse<T> { Success = false, Message = message };
    }
}

// PagedResponse.cs
namespace EnglishLearning.WebAPI.Extensions;

public class PagedResponse<T> : ApiResponse<IReadOnlyList<T>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }

    public static PagedResponse<T> Ok(
        IReadOnlyList<T> items,
        int pageNumber,
        int pageSize,
        int totalRecords,
        string message = "Success")
    {
        return new PagedResponse<T>
        {
            Success = true,
            Message = message,
            Data = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
        };
    }
}
```

## Verification

- [ ] Run `dotnet build EnglishLearning.WebAPI` — 0 errors
- [ ] `ApiResponse<T>` has factory methods: Ok, BadRequest, NotFound
- [ ] `PagedResponse<T>` extends `ApiResponse<IReadOnlyList<T>>`
- [ ] `PagedResponse<T>` calculates TotalPages correctly

## Acceptance Criteria

- [ ] `ApiResponse<T>` with Success, Message, Data, Errors properties
- [ ] `ApiResponse<T>.Ok()` factory method for success
- [ ] `ApiResponse<T>.BadRequest()` factory method for errors
- [ ] `ApiResponse<T>.NotFound()` factory method for 404
- [ ] `PagedResponse<T>` inherits from `ApiResponse<IReadOnlyList<T>>`
- [ ] `PagedResponse<T>` has PageNumber, PageSize, TotalRecords, TotalPages
- [ ] `PagedResponse<T>.Ok()` factory method calculates TotalPages
- [ ] WebAPI project builds successfully
