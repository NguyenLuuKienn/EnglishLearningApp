using EnglishLearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishLearning.Infrastructure.Persistence.Configurations;

public class ChoiceConfiguration : IEntityTypeConfiguration<Choice>
{
    public void Configure(EntityTypeBuilder<Choice> builder)
    {
        builder.ToTable("Choices");

        builder.Property(c => c.ChoiceText).IsRequired().HasMaxLength(500);
        builder.Property(c => c.IsCorrect).IsRequired();
        builder.HasIndex(c => c.QuestionId);
    }
}
