# Task 3.4: Implement Base Repository

## Description

Implement the generic `Repository<T>` class that provides CRUD operations for any entity type. This is the foundation for all specific repositories.

## Priority
🔴 Critical — Core data access implementation

## Dependencies
- Task 3.2 (DbContext)
- Task 1.8 (IRepository<T> interface)

## Files to Create

| File | Action |
|------|--------|
| `EnglishLearning.Infrastructure/Repositories/Repository.cs` | Create |

## Steps

### Step 1: Create Repository<T> class
1. Generic class `Repository<T> : IRepository<T>` where T : class
2. Constructor accepts `ApplicationDbContext`
3. Get `DbSet<T>` from context

### Step 2: Implement interface methods
1. `GetByIdAsync(Guid id)` — use `FindAsync()`
2. `GetAllAsync()` — use `ToListAsync()`
3. `GetByExpressionAsync(Expression<Func<T, bool>> predicate)` — use `Where().ToListAsync()`
4. `GetPagedAsync(int pageNumber, int pageSize)` — use `Skip().Take().ToListAsync()` + `CountAsync()` for total
5. `AddAsync(T entity)` — use `AddAsync()`
6. `Update(T entity)` — use `Update()`
7. `Delete(T entity)` — use `Remove()`

## Expected Code

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace EnglishLearning.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(new object[] { id });
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetByExpressionAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task<(IReadOnlyList<T> Items, int TotalRecords)> GetPagedAsync(int pageNumber, int pageSize)
    {
        var totalRecords = await _dbSet.CountAsync();
        var items = await _dbSet
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalRecords);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
}
```

## Verification

- [x] Run `dotnet build EnglishLearning.Infrastructure` — 0 errors ✅
- [x] All interface methods are implemented ✅
- [x] Generic constraint `where T : class` is applied ✅
- [x] DbContext is injected via constructor ✅

## Acceptance Criteria

- [x] `Repository<T>` implements `IRepository<T>` ✅
- [x] Generic constraint `where T : class` ✅
- [x] Constructor accepts `ApplicationDbContext` ✅
- [x] `GetByIdAsync` uses `FindAsync` ✅
- [x] `GetAllAsync` returns all entities ✅
- [x] `GetByExpressionAsync` filters by expression ✅
- [x] `GetPagedAsync` returns tuple `(Items, TotalRecords)` with count ✅
- [x] `AddAsync`, `Update`, `Delete` properly use DbSet methods ✅
- [x] Infrastructure project builds successfully ✅

---

## ✅ Completed: 2026-07-06

- `Repository<T>` implements `IRepository<T>` with generic constraint `where T : class`
- Constructor injects `ApplicationDbContext`, exposes `_dbSet` via `context.Set<T>()`
- Methods implemented:
  - `GetByIdAsync` — `FindAsync`
  - `GetAllAsync` — `ToListAsync`
  - `GetByExpressionAsync` — `Where(predicate).ToListAsync`
  - `GetPagedAsync` — supports `predicate`, `orderBy`, `ascending` parameters
  - `AddAsync` — `AddAsync`
  - `Update` — `Update`
  - `Delete` — `Remove`
- Build verified: 0 errors
