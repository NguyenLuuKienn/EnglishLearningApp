# Task 6.14: AuthController

## Description

Create AuthController with Register, Login, RefreshToken, and GetProfile endpoints.

## Priority
🔴 Critical — HTTP entry points for authentication

## Dependencies
- Task 6.6 (Register), Task 6.7 (Login), Task 6.8 (RefreshToken), Task 6.9 (GetProfile), Task 6.13 (Auth Requests)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.WebAPI/Controllers/AuthController.cs` | Create |

## Steps

### Step 1: Create AuthController
1. `[ApiController]`, `[Route("api/[controller]")]`
2. Inject `IMediator`
3. Endpoints:
   - `POST /register` — RegisterCommand
   - `POST /login` — LoginCommand
   - `POST /refresh-token` — RefreshTokenCommand
   - `GET /profile` — GetProfileQuery (requires auth)

## Expected Code

```csharp
using EnglishLearning.Application.DTOs;
using EnglishLearning.Application.Features.Auth.Commands.Login;
using EnglishLearning.Application.Features.Auth.Commands.RefreshToken;
using EnglishLearning.Application.Features.Auth.Commands.Register;
using EnglishLearning.Application.Features.Auth.Queries.GetProfile;
using EnglishLearning.WebAPI.Models.Common;
using EnglishLearning.WebAPI.Models.Requests.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var command = new RegisterCommand(request.Username, request.Email, request.Password);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(ApiResponse<Guid>.BadRequest(
                result.Errors?.ToList() ?? [result.Error ?? string.Empty]));

        return Ok(ApiResponse<Guid>.Ok(result.Value!, "Registration successful"));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var command = new LoginCommand(request.Username, request.Password);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return Unauthorized(ApiResponse<TokenDto>.BadRequest(
                [result.Error ?? "Invalid credentials"]));

        return Ok(ApiResponse<TokenDto>.Ok(result.Value!));
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var command = new RefreshTokenCommand(request.AccessToken, request.RefreshToken);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return Unauthorized(ApiResponse<TokenDto>.BadRequest(
                [result.Error ?? "Invalid tokens"]));

        return Ok(ApiResponse<TokenDto>.Ok(result.Value!));
    }

    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!);
        var query = new GetProfileQuery(userId);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return NotFound(ApiResponse<UserDto>.NotFound(result.Error ?? string.Empty));

        return Ok(ApiResponse<UserDto>.Ok(result.Value!));
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.WebAPI` — 0 errors ✅
- [x] All endpoints return proper ApiResponse ✅
- [x] GetProfile requires [Authorize] ✅

## Acceptance Criteria

- [x] `AuthController` with `[ApiController]`, `[Route("api/[controller]")]` ✅
- [x] `POST /register` — returns 200 Ok or 400 BadRequest ✅
- [x] `POST /login` — returns 200 Ok with tokens or 401 Unauthorized ✅
- [x] `POST /refresh-token` — returns 200 Ok with new tokens or 401 Unauthorized ✅
- [x] `GET /profile` — requires [Authorize], returns 200 Ok or 404 NotFound ✅
- [x] All responses wrapped in `ApiResponse<T>` ✅
- [x] WebAPI project builds successfully ✅

---

## ✅ Completed: 2026-07-07

- **AuthController** — Primary constructor, 4 endpoints:
  - `POST /register` — RegisterCommand → `ApiResponse<Guid>` (200 Ok)
  - `POST /login` — LoginCommand → `ApiResponse<TokenDto>` (200 Ok)
  - `POST /refresh-token` — RefreshTokenCommand → `ApiResponse<TokenDto>` (200 Ok)
  - `GET /profile` — `[Authorize]`, extracts UserId from JWT claim → `ApiResponse<UserDto>` (200 Ok)
- Exceptions caught by `ExceptionMiddleware` → proper HTTP status codes (401, 409, 404)
- Namespace: `EnglishLearning.WebAPI.Controllers`
- Build verified: 0 errors
