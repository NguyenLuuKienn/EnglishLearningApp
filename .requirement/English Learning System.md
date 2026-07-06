# English Learning System - Backend Architecture (Enterprise Standard)

---

## 1. Overview

* Architecture: Clean Architecture + CQRS
* Pattern:

  * Repository Pattern
  * Unit of Work (optional)
  * Mediator Pattern (MediatR)
* Principles:

  * SOLID
  * Separation of Concerns
  * Dependency Inversion

---

## 2. Solution Structure

```
EnglishLearning.sln

src/
├── EnglishLearning.Domain
├── EnglishLearning.Application
├── EnglishLearning.Infrastructure
├── EnglishLearning.API

tests/
├── EnglishLearning.UnitTests
```

---

## 3. Layer Responsibilities

### 3.1 Domain Layer

* Chứa:

  * Entities
  * Enums
  * ValueObjects
* Không phụ thuộc bất kỳ layer nào

```
Domain/
├── Entities/
├── Enums/
├── ValueObjects/
├── Interfaces/
└── Common/
```

---

### 3.2 Application Layer

* Chứa:

  * Business logic
  * Use cases (CQRS)
* Không phụ thuộc Infrastructure

```
Application/
├── Features/
│   ├── Users/
│   │   ├── Commands/
│   │   ├── Queries/
│   │   ├── DTOs/
│   │   └── Validators/
├── Interfaces/
├── Common/
```

---

### 3.3 Infrastructure Layer

* Chứa:

  * EF Core
  * Repository implementation
  * External services

```
Infrastructure/
├── Persistence/
├── Repositories/
├── Services/
└── Migrations/
```

---

### 3.4 API Layer

* Chứa:

  * Controllers
  * Middleware
  * Request/Response contract

```
API/
├── Controllers/
├── Contracts/
│   ├── Requests/
│   └── Responses/
├── Middleware/
├── Extensions/
```

---

## 4. Core Features

### 4.1 Authentication

* Register / Login
* JWT Token
* Refresh Token

---

### 4.2 User Management

* CRUD User
* Profile
* Role (Admin / User)

---

### 4.3 Course System

* Create Course
* Add Lesson
* Categorize

---

### 4.4 Question System

* Types:

  * Multiple choice
  * Fill in blank
  * Listening
* CRUD Question

---

### 4.5 Exam System

* Create Exam
* Assign questions
* Submit exam
* Auto grading

---

### 4.6 Result Tracking

* Save score
* History
* Analytics

---

## 5. API Standard (Enterprise)

### 5.1 Base Response

```csharp
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public List<string> Errors { get; set; }
}
```

---

### 5.2 Paging Response

```csharp
public class PagedResponse<T> : ApiResponse<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
}
```

---

### 5.3 Error Response

```json
{
  "success": false,
  "message": "Validation failed",
  "errors": [
    "Email is required",
    "Password too short"
  ]
}
```

---

## 6. Request / Response Pattern

### Request

* Chỉ dùng cho API layer
* Không dùng Entity trực tiếp

```csharp
public class CreateUserRequest
{
    public string Username { get; set; }
    public string Email { get; set; }
}
```

---

### Response

```csharp
public class UserResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
}
```

---

## 7. CQRS Pattern

### Command

```csharp
public class CreateUserCommand : IRequest<Guid>
{
    public string Username { get; set; }
}
```

---

### Handler

```csharp
public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // logic
    }
}
```

---

### Query

```csharp
public class GetUserByIdQuery : IRequest<UserDto>
{
    public Guid Id { get; set; }
}
```

---

## 8. Validation (FluentValidation)

```csharp
public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
    }
}
```

---

## 9. Mapping (AutoMapper)

```csharp
CreateMap<User, UserDto>();
CreateMap<CreateUserRequest, CreateUserCommand>();
```

---

## 10. Exception Handling

### Middleware

* Handle global exception
* Return standardized response

```csharp
{
  "success": false,
  "message": "Internal Server Error"
}
```

---

## 11. Naming Convention

* PascalCase: Class, Method
* camelCase: variable
* Suffix:

  * Command
  * Query
  * Handler
  * Validator
  * Response
  * Request

---

## 12. Dependency Injection

* Register in `Program.cs`

```csharp
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
```

---

## 13. Security

* JWT Authentication
* Role-based Authorization
* HTTPS
* Input validation

---

## 14. Logging

* Use Serilog / built-in logging
* Log:

  * Request
  * Error
  * Performance

---

## 15. Testing

* Unit test:

  * Application layer
* Mock repository
* Test handlers

---

## 16. Best Practices

* Không expose Entity ra API
* Không viết logic trong Controller
* Mỗi feature tách folder riêng
* Dùng async/await toàn bộ
* Không hardcode config

---

## 17. Recommended Packages

```
MediatR
FluentValidation
AutoMapper
Microsoft.EntityFrameworkCore (v8.x)
Serilog
Swashbuckle (Swagger)
```

---

## 18. Versioning

* API version: `/api/v1/...`
* Backward compatible

---

## 19. Deployment Ready

* Docker support
* CI/CD pipeline
* Environment config

---

## 20. Flow Summary

```
Client
 → Controller
 → Request
 → Command/Query
 → Handler
 → Repository
 → DB
 → Response
 → ApiResponse
 → Client
```

---
