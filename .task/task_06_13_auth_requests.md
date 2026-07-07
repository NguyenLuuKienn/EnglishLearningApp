# Task 6.13: Auth Request Contracts

## Description

Create request DTOs for authentication endpoints.

## Priority
🔴 Critical — API input contracts for auth

## Dependencies
- None (independent)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.WebAPI/Models/Requests/Auth/RegisterRequest.cs` | Create |
| `EnglishLearning.WebAPI/Models/Requests/Auth/LoginRequest.cs` | Create |
| `EnglishLearning.WebAPI/Models/Requests/Auth/RefreshTokenRequest.cs` | Create |

## Steps

### Step 1: Create RegisterRequest
1. Username (required, max 100), Email (required, max 200), Password (required, min 6)

### Step 2: Create LoginRequest
1. Username (required), Password (required)

### Step 3: Create RefreshTokenRequest
1. AccessToken (required), RefreshToken (required)

## Expected Code

```csharp
// RegisterRequest.cs
using System.ComponentModel.DataAnnotations;

namespace EnglishLearning.WebAPI.Models.Requests.Auth;

public class RegisterRequest
{
    [Required]
    [StringLength(100)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;
}

// LoginRequest.cs
using System.ComponentModel.DataAnnotations;

namespace EnglishLearning.WebAPI.Models.Requests.Auth;

public class LoginRequest
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}

// RefreshTokenRequest.cs
using System.ComponentModel.DataAnnotations;

namespace EnglishLearning.WebAPI.Models.Requests.Auth;

public class RefreshTokenRequest
{
    [Required]
    public string AccessToken { get; set; } = string.Empty;

    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.WebAPI` — 0 errors ✅
- [x] All request classes have data annotations ✅

## Acceptance Criteria

- [x] `RegisterRequest` with Username, Email, Password (validated) ✅
- [x] `LoginRequest` with Username, Password ✅
- [x] `RefreshTokenRequest` with AccessToken, RefreshToken ✅
- [x] All in `EnglishLearning.WebAPI.Models.Requests.Auth` namespace ✅
- [x] WebAPI project builds successfully ✅

---

## ✅ Completed: 2026-07-07

- **RegisterRequest** — Username (Required, max 100), Email (Required, EmailAddress, max 200), Password (Required, min 6)
- **LoginRequest** — Username (Required), Password (Required)
- **RefreshTokenRequest** — AccessToken (Required), RefreshToken (Required)
- Namespace: `EnglishLearning.WebAPI.Models.Requests.Auth`
- Build verified: 0 errors
