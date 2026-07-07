# Task 8.10: Setup Hangfire

## Description

Setup Hangfire for background job scheduling in the WebAPI project.

## Priority
🔴 Critical — Background job infrastructure

## Dependencies
- Phase 4 complete (WebAPI)

## Files to Edit

| File | Action |
|------|--------|
| `EnglishLearning.WebAPI/EnglishLearning.WebAPI.csproj` | Edit |
| `EnglishLearning.WebAPI/Program.cs` | Edit |

## Steps

### Step 1: Add Hangfire packages
1. `Hangfire.Core`
2. `Hangfire.SqlServer`
3. `Hangfire.Dashboard.Basic.Authentication`

### Step 2: Configure Hangfire in Program.cs
1. Add `AddHangfire` with SQL Server storage
2. Add `UseHangfireServer`
3. Add `UseHangfireDashboard` with basic auth
4. Schedule recurring jobs

## Expected Code

```csharp
// EnglishLearning.WebAPI.csproj — add:
<PackageReference Include="Hangfire.Core" Version="1.8.14" />
<PackageReference Include="Hangfire.SqlServer" Version="1.8.14" />
<PackageReference Include="Hangfire.Dashboard.Basic.Authentication" Version="1.0.2" />

// Program.cs — add before var app = builder.Build():
using Hangfire;

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLock = true
    }));

builder.Services.AddHangfireServer();

// In middleware pipeline, after app.MapControllers():
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
    {
        SslRedirect = false,
        LoginCaseSensitive = false,
        Users = new[] { new BasicAuthAuthorizationFilterUser { Login = "admin", PasswordClear = "Admin@123" } }
    }) }
});

// Configure recurring jobs
RecurringJob.AddOrUpdate<ICheckQuizAssignmentsJob>(
    "check-quiz-assignments",
    job => job.CheckAssignments(),
    "*/5 * * * *"); // Every 5 minutes
```

## Verification

- [x] Run `dotnet build EnglishLearning.WebAPI` — 0 errors ✅
- [x] Hangfire dashboard accessible at /hangfire ✅
- [x] Recurring job configured ✅

## Acceptance Criteria

- [x] Hangfire packages installed (Hangfire.AspNetCore, Hangfire.SqlServer 1.8.14) ✅
- [x] Hangfire configured with SQL Server storage ✅
- [x] Hangfire dashboard protected with basic auth (custom HangfireBasicAuthAuthorizationFilter) ✅
- [x] Recurring job scheduled for checking quiz assignments (every 5 min) ✅
- [x] WebAPI project builds successfully ✅
