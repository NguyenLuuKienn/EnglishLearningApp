using EnglishLearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearning.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Vocabulary> Vocabularies => Set<Vocabulary>();
    public DbSet<Quiz> Quizzes => Set<Quiz>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Choice> Choices => Set<Choice>();
    public DbSet<QuizResult> QuizResults => Set<QuizResult>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Apply entity configurations from this assembly
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Configure relationships
        builder.Entity<Quiz>()
            .HasMany(q => q.Questions)
            .WithOne(q => q.Quiz)
            .HasForeignKey(q => q.QuizId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Question>()
            .HasMany(q => q.Choices)
            .WithOne(c => c.Question)
            .HasForeignKey(c => c.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Quiz>()
            .HasMany(q => q.Results)
            .WithOne(r => r.Quiz)
            .HasForeignKey(r => r.QuizId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
