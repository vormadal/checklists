using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ChecklistItemConfiguration : IEntityTypeConfiguration<ChecklistItem>
{
    public void Configure(EntityTypeBuilder<ChecklistItem> builder)
    {
        builder.ToTable(nameof(ChecklistItem));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Title).IsRequired();

        builder.HasOne(x => x.CopiedFrom)
            .WithMany()
            .HasForeignKey(x => x.CopiedFromId)
            .IsRequired(false);

        builder
            .HasOne(x => x.Checklist)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.ChecklistId)
            .IsRequired();
    }
}
