using EnglishLearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishLearning.Infrastructure.Persistence.Configurations;

public class LearningHistoryConfiguration : IEntityTypeConfiguration<LearningHistory>
{
    public void Configure(EntityTypeBuilder<LearningHistory> builder)
    {
        builder.ToTable("LearningHistories");

        builder.Property(h => h.UserId).IsRequired().HasMaxLength(200);
        builder.HasIndex(h => h.UserId);
        builder.HasIndex(h => h.CreatedAt);
        builder.Property(h => h.ActionType).HasConversion<int>();
        builder.Property(h => h.Details).HasMaxLength(1000);
    }
}
