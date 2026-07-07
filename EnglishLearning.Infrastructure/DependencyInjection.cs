using EnglishLearning.Application.Interfaces;
using EnglishLearning.Domain.Interfaces;
using EnglishLearning.Infrastructure.Persistence;
using EnglishLearning.Infrastructure.Repositories;
using EnglishLearning.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Register DbContext with SQL Server
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Register Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

        // Register Repositories
        services.AddScoped<IVocabularyRepository, VocabularyRepository>();
        services.AddScoped<IQuizRepository, QuizRepository>();
        services.AddScoped<IQuizResultRepository, QuizResultRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IQuizAssignmentRepository, QuizAssignmentRepository>();
        services.AddScoped<ILearningHistoryRepository, LearningHistoryRepository>();
        services.AddScoped<ILeaderboardRepository, LeaderboardRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();

        // Register Services
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<ICheckQuizAssignmentsJob, CheckQuizAssignmentsJob>();
        services.AddScoped<ISendAssignmentNotificationsJob, SendAssignmentNotificationsJob>();

        return services;
    }
}
