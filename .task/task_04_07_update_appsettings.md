# Task 4.7: Update appsettings.json

## Description

Update `appsettings.json` with required configuration: connection strings, JWT settings, and logging configuration.

## Priority
🔴 Critical — Application configuration

## Dependencies
- Task 4.6 (Program.cs updated)

## Files to Edit

| File | Action |
|------|--------|
| `EnglishLearning.WebAPI/appsettings.json` | Edit |
| `EnglishLearning.WebAPI/appsettings.Development.json` | Edit |

## Steps

### Step 1: Update appsettings.json
1. Add `ConnectionStrings:DefaultConnection` — SQL Server connection string (localdb)
2. Add `Jwt:Key` — JWT signing key (use a strong key)
3. Add `Jwt:Issuer` — token issuer
4. Add `Jwt:Audience` — token audience
5. Add `Jwt:ExpirationInMinutes` — token expiration time

### Step 2: Update appsettings.Development.json
1. Add development-specific connection string if different

## Expected Code

```json
// appsettings.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EnglishLearningDb;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "Jwt": {
    "Key": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "EnglishLearningAPI",
    "Audience": "EnglishLearningClient",
    "ExpirationInMinutes": 60
  }
}
```

```json
// appsettings.Development.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EnglishLearningDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

## Verification

- [ ] `ConnectionStrings:DefaultConnection` exists
- [ ] `Jwt:Key`, `Jwt:Issuer`, `Jwt:Audience` exist
- [ ] Connection string uses localdb for development
- [ ] WebAPI project builds successfully

## Acceptance Criteria

- [ ] `appsettings.json` has `ConnectionStrings:DefaultConnection` with SQL Server localdb
- [ ] `appsettings.json` has `Jwt:Key` (at least 32 characters)
- [ ] `appsettings.json` has `Jwt:Issuer` set to "EnglishLearningAPI"
- [ ] `appsettings.json` has `Jwt:Audience` set to "EnglishLearningClient"
- [ ] `appsettings.json` has `Jwt:ExpirationInMinutes` set to 60
- [ ] `appsettings.Development.json` has development connection string
- [ ] JSON is valid (no syntax errors)
