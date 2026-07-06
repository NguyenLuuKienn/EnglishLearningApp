# Task 9.1: Configure CORS in WebAPI

## Description

Configure CORS in WebAPI to allow requests from the frontend dev server.

## Priority
🔴 Critical — Frontend-backend communication

## Dependencies
- Phase 4 complete (WebAPI)

## Files to Edit

| File | Action |
|------|--------|
| `EnglishLearning.WebAPI/Program.cs` | Edit |

## Steps

### Step 1: Add CORS policy in Program.cs
1. Add `builder.Services.AddCors()` before `var app = builder.Build()`
2. Add `app.UseCors()` in middleware pipeline (before `MapControllers()`)

## Expected Code

```csharp
// In Program.cs, before var app = builder.Build():
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Vite dev server
              .AllowAnyHeader()
              .AllowAnyMethod()
              .WithHeaders("Authorization");
    });
});

// In middleware pipeline, before MapControllers():
app.UseCors("AllowFrontend");
```

## Verification

- [ ] CORS policy configured
- [ ] Frontend can make requests to backend
- [ ] Build succeeds

## Acceptance Criteria

- [ ] CORS policy allows localhost:5173
- [ ] Authorization header allowed
- [ ] `UseCors` in middleware pipeline
- [ ] Build succeeds
