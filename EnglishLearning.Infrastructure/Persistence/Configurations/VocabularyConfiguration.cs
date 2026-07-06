using EnglishLearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishLearning.Infrastructure.Persistence.Configurations;

public class VocabularyConfiguration : IEntityTypeConfiguration<Vocabulary>
{
    public void Configure(EntityTypeBuilder<Vocabulary> builder)
    {
        builder.ToTable("Vocabularies");

        builder.Property(v => v.Word)
            .IsRequired()
            .HasMaxLength(200);
        builder.HasIndex(v => v.Word);
        builder.HasIndex(v => v.Difficulty);

        builder.Property(v => v.Definition)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(v => v.Example).HasMaxLength(1000);
        builder.Property(v => v.PartOfSpeech).HasMaxLength(50);
        builder.Property(v => v.Difficulty).HasConversion<int>();
    }
}
