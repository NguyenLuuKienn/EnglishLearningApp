using EnglishLearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishLearning.Infrastructure.Persistence.Configurations;

public class QuizResultConfiguration : IEntityTypeConfiguration<QuizResult>
{
    public void Configure(EntityTypeBuilder<QuizResult> builder)
    {
        builder.ToTable("QuizResults");

        builder.Property(r => r.UserId).IsRequired().HasMaxLength(200);
        builder.Property(r => r.Score).HasPrecision(5, 2);
        builder.Property(r => r.TotalQuestions).IsRequired();
        builder.Property(r => r.CorrectAnswers).IsRequired();
        builder.Property(r => r.DurationMinutes).IsRequired();
        builder.Property(r => r.CompletedAt).IsRequired();
        builder.HasIndex(r => r.UserId);
        builder.HasIndex(r => r.CompletedAt);
    }
}
