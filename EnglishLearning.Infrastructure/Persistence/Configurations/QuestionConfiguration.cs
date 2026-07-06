using EnglishLearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishLearning.Infrastructure.Persistence.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Questions");

        builder.Property(q => q.QuestionText).IsRequired().HasMaxLength(2000);
        builder.Property(q => q.QuestionType).HasConversion<int>();
        builder.Property(q => q.Difficulty).HasConversion<int>();
        builder.Property(q => q.CorrectAnswer).HasMaxLength(1000);
        builder.Property(q => q.Explanation).HasMaxLength(1000);
        builder.HasIndex(q => q.QuizId);
    }
}
