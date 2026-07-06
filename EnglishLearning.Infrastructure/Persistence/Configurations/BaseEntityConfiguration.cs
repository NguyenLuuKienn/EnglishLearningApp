using EnglishLearning.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishLearning.Infrastructure.Persistence.Configurations;

public class BaseEntityConfiguration : IEntityTypeConfiguration<BaseEntity>
{
    public void Configure(EntityTypeBuilder<BaseEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();
        builder.Property(e => e.CreatedBy).HasMaxLength(200);
        builder.Property(e => e.UpdatedBy).HasMaxLength(200);
    }
}
