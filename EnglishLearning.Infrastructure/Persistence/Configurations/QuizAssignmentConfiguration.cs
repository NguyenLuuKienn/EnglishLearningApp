using EnglishLearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishLearning.Infrastructure.Persistence.Configurations;

public class QuizAssignmentConfiguration : IEntityTypeConfiguration<QuizAssignment>
{
    public void Configure(EntityTypeBuilder<QuizAssignment> builder)
    {
        builder.ToTable("QuizAssignments");

        builder.Property(a => a.QuizId).IsRequired();
        builder.HasOne(a => a.Quiz).WithMany().HasForeignKey(a => a.QuizId).OnDelete(DeleteBehavior.Restrict);

        builder.Property(a => a.TargetRole).HasConversion<int>().IsRequired(false);
        builder.Property(a => a.TargetUserId).HasMaxLength(200).IsRequired(false);
        builder.Property(a => a.StartTime).IsRequired();
        builder.Property(a => a.EndTime).IsRequired();
        builder.Property(a => a.Status).HasConversion<int>();

        builder.HasIndex(a => a.QuizId);
        builder.HasIndex(a => a.TargetRole);
        builder.HasIndex(a => a.TargetUserId);
        builder.HasIndex(a => a.StartTime);
        builder.HasIndex(a => a.EndTime);
    }
}
