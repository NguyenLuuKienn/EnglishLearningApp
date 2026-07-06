using EnglishLearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishLearning.Infrastructure.Persistence.Configurations;

public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        builder.ToTable("Quizzes");

        builder.Property(q => q.Title).IsRequired().HasMaxLength(200);
        builder.Property(q => q.Description).HasMaxLength(1000);
        builder.Property(q => q.Difficulty).HasConversion<int>();
        builder.HasIndex(q => q.Difficulty);
        builder.Property(q => q.TimeLimitMinutes);
        builder.Property(q => q.PassingScore).HasPrecision(5, 2);
    }
}
