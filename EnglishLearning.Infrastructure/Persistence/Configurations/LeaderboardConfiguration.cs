using EnglishLearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishLearning.Infrastructure.Persistence.Configurations;

public class LeaderboardConfiguration : IEntityTypeConfiguration<Leaderboard>
{
    public void Configure(EntityTypeBuilder<Leaderboard> builder)
    {
        builder.ToTable("Leaderboards");

        builder.Property(l => l.UserId).IsRequired().HasMaxLength(200);
        builder.HasIndex(l => l.UserId).IsUnique();
        builder.Property(l => l.TotalScore).HasPrecision(5, 2);
        builder.Property(l => l.AverageScore).HasPrecision(5, 2);
        builder.Property(l => l.LastActiveDate).IsRequired();
    }
}
