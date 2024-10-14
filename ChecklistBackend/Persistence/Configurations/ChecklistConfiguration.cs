using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ChecklistConfiguration : IEntityTypeConfiguration<Checklist>
{
    public void Configure(EntityTypeBuilder<Checklist> builder)
    {
        builder.ToTable(nameof(Checklist));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Type)
            .HasDefaultValue(ChecklistType.Checklist);

        builder.Property(x => x.CreatedOn).HasDefaultValueSql("NOW()");
        builder.Property(x => x.ModifiedOn).HasDefaultValueSql("NOW()");
        builder.HasOne(x => x.Owner as ApplicationUser)
            .WithMany()
            .HasForeignKey(x => x.OwnerId)
            .IsRequired(false); //TODO will be later

        builder.HasOne(x => x.Template)
            .WithMany()
            .HasForeignKey(x => x.TemplateId)
            .IsRequired(false);

        builder.Property(x => x.Title)
            .IsRequired();

        builder
            .HasMany(x => x.Items)
            .WithOne(x => x.Checklist)
            .HasForeignKey(x => x.Id)
            .IsRequired();
    }
}
