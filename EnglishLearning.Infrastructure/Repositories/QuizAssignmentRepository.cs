using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;
using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearning.Infrastructure.Repositories;

public class QuizAssignmentRepository(ApplicationDbContext context)
    : Repository<QuizAssignment>(context), IQuizAssignmentRepository
{
    public async Task<List<QuizAssignment>> GetAllWithQuizAsync()
    {
        return await _dbSet.Include(a => a.Quiz).ToListAsync();
    }

    public async Task<List<QuizAssignment>> GetByUserIdAsync(string userId)
    {
        return await _dbSet.Where(a => a.TargetUserId == userId).ToListAsync();
    }

    public async Task<List<QuizAssignment>> GetByRoleAsync(UserRole role)
    {
        return await _dbSet.Where(a => a.TargetRole == role).ToListAsync();
    }

    public async Task<List<QuizAssignment>> GetActiveAssignmentsAsync()
    {
        var now = DateTime.UtcNow;
        return await _dbSet
            .Where(a => a.Status != AssignmentStatus.Cancelled &&
                       a.StartTime <= now && a.EndTime >= now)
            .ToListAsync();
    }

    public async Task<List<QuizAssignment>> GetExpiringSoonAsync(DateTime before)
    {
        return await _dbSet
            .Where(a => a.Status != AssignmentStatus.Cancelled &&
                       a.EndTime <= before)
            .ToListAsync();
    }
}
