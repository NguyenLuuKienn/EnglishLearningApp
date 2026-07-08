using EnglishLearning.Domain.Entities;
using EnglishLearning.Domain.Enums;

namespace EnglishLearning.Domain.Interfaces;

public interface IQuizAssignmentRepository : IRepository<QuizAssignment>
{
    Task<List<QuizAssignment>> GetAllWithQuizAsync();
    Task<List<QuizAssignment>> GetByUserIdAsync(string userId);
    Task<List<QuizAssignment>> GetByRoleAsync(UserRole role);
    Task<List<QuizAssignment>> GetActiveAssignmentsAsync();
    Task<List<QuizAssignment>> GetExpiringSoonAsync(DateTime before);
}