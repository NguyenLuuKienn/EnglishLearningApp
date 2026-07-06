# Task 4.6: Update Program.cs

## Description

Update `Program.cs` to wire up all layers: Application, Infrastructure, JWT authentication, exception middleware, and Swagger configuration.

## Priority
🔴 Critical — Application entry point

## Dependencies
- Task 4.1 (WebAPI dependencies)
- Task 4.5 (Exception middleware)
- Task 2.8 (Application DI)
- Task 3.7 (Infrastructure DI)

## Files to Edit

| File | Action |
|------|--------|
| `EnglishLearning.WebAPI/Program.cs` | Rewrite |

## Steps

### Step 1: Add using statements
1. Add `using EnglishLearning.Application.DependencyInjection;`
2. Add `using EnglishLearning.Infrastructure.DependencyInjection;`
3. Add `using EnglishLearning.WebAPI.Middlewares;`

### Step 2: Register services
1. Add `builder.Services.AddApplication()` — registers MediatR, FluentValidation, AutoMapper
2. Add `builder.Services.AddInfrastructure(builder.Configuration)` — registers DbContext, UnitOfWork
3. Add `builder.Services.AddControllers()`
4. Add `builder.Services.AddEndpointsApiExplorer()`
5. Configure Swagger with description

### Step 3: Configure JWT authentication
1. Add `AddAuthentication(JwtBearerDefaults.AuthenticationScheme)`
2. Add `AddJwtBearer` with token validation parameters from configuration
3. Add `AddAuthorization()`

### Step 4: Configure middleware pipeline
1. `UseGlobalExceptionHandling()` — first, before other middleware
2. `UseHttpsRedirection()`
3. `UseAuthorization()`
4. `UseSwagger()` and `UseSwaggerUI()` (development only)
5. `MapControllers()`

## Expected Code

```csharp
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EnglishLearning.Application.DependencyInjection;
using EnglishLearning.Infrastructure.DependencyInjection;
using EnglishLearning.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "English Learning API",
        Version = "v1",
        Description = "API for the English Learning System"
    });
});

// Application layer (MediatR, FluentValidation, AutoMapper)
builder.Services.AddApplication();

// Infrastructure layer (DbContext, UnitOfWork)
builder.Services.AddInfrastructure(builder.Configuration);

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "default-key"))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalExceptionHandling();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

## Verification

- [ ] Run `dotnet build EnglishLearning.WebAPI` — 0 errors
- [ ] `AddApplication()` is called
- [ ] `AddInfrastructure(builder.Configuration)` is called
- [ ] JWT authentication is configured
- [ ] Exception middleware is registered
- [ ] Swagger is configured for development

## Acceptance Criteria

- [ ] `AddApplication()` registers Application layer services
- [ ] `AddInfrastructure(builder.Configuration)` registers Infrastructure services
- [ ] JWT authentication configured with Issuer, Audience, Key from appsettings
- [ ] `UseGlobalExceptionHandling()` is first middleware
- [ ] `UseAuthentication()` and `UseAuthorization()` are configured
- [ ] Swagger enabled in Development environment
- [ ] `MapControllers()` maps API endpoints
- [ ] WebAPI project builds successfully
