# Task 4.5: Create Exception Middleware

## Description

Create a global exception handling middleware that catches unhandled exceptions and returns standardized `ApiResponse` error format.

## Priority
🟡 High — Ensures consistent error handling

## Dependencies
- Task 4.2 (API Response models)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.WebAPI/Middlewares/ExceptionMiddleware.cs` | Create |

## Steps

### Step 1: Create ExceptionMiddleware class
1. Constructor accepts `RequestDelegate`, `ILogger<ExceptionMiddleware>`, `IHostEnvironment`
2. `InvokeAsync(HttpContext context)` method:
   - Try/catch block around `_next(context)`
   - On exception: log error details
   - Set response status code based on exception type
   - Return JSON response using `ApiResponse<object>`

### Step 2: Handle exception types
1. `ArgumentException` → 400 Bad Request
2. `KeyNotFoundException` → 404 Not Found
3. Default → 500 Internal Server Error

### Step 3: Add extension method
1. Create `UseGlobalExceptionHandling(this IApplicationBuilder app)` extension method

## Expected Code

```csharp
using System.Net;
using System.Text.Json;
using EnglishLearning.WebAPI.Extensions;

namespace EnglishLearning.WebAPI.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _environment;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger,
        IHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);

            var response = context.Response;
            response.ContentType = "application/json";

            (HttpStatusCode code, string message) = ex switch
            {
                ArgumentException => (HttpStatusCode.BadRequest, ex.Message),
                KeyNotFoundException => (HttpStatusCode.NotFound, ex.Message),
                _ => (HttpStatusCode.InternalServerError, "An internal server error occurred")
            };

            response.StatusCode = (int)code;

            var result = ApiResponse<object>.BadRequest(
                new List<string> { _environment.IsDevelopment() ? ex.ToString() : message },
                message);

            var json = JsonSerializer.Serialize(result);
            await response.WriteAsync(json);
        }
    }
}

// Extension method
public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionMiddleware>();
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.WebAPI` — 0 errors ✅
- [x] Middleware catches exceptions and returns JSON ✅
- [x] Different exception types map to correct HTTP status codes ✅
- [x] Development environment shows full exception details ✅
- [x] Production environment shows generic error message ✅

## Acceptance Criteria

- [x] `ExceptionMiddleware` implements middleware pattern with `InvokeAsync` ✅
- [x] Constructor accepts RequestDelegate, ILogger, IHostEnvironment ✅
- [x] ArgumentException → 400 Bad Request ✅
- [x] KeyNotFoundException → 404 Not Found ✅
- [x] Default → 500 Internal Server Error ✅
- [x] Response is JSON formatted using `ApiResponse<object>` ✅
- [x] Development mode shows full exception details ✅
- [x] `UseGlobalExceptionHandling()` extension method exists ✅
- [x] WebAPI project builds successfully ✅

---

## ✅ Completed: 2026-07-06

- **ExceptionMiddleware** — global middleware với `InvokeAsync(HttpContext context)`
  - Constructor inject: `RequestDelegate`, `ILogger<ExceptionMiddleware>`, `IHostEnvironment`
  - Try/catch quanh `_next(context)`, log error bằng `_logger.LogError()`
  - Exception mapping qua pattern matching:
    - `ArgumentException` → 400 Bad Request
    - `KeyNotFoundException` → 404 Not Found
    - Default → 500 Internal Server Error
  - Response: `ApiResponse<object>.BadRequest()` với JSON serialization
  - Development mode: hiển thị `ex.ToString()` (full stack trace)
  - Production mode: hiển thị generic message
- **ExceptionMiddlewareExtensions** — extension method `UseGlobalExceptionHandling(this IApplicationBuilder app)`
  - Namespace: `EnglishLearning.WebAPI.Middlewares`
- Build verified: 0 errors
