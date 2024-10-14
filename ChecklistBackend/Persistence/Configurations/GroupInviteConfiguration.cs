using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class GroupInviteConfiguration : IEntityTypeConfiguration<GroupInvite>
{
    public void Configure(EntityTypeBuilder<GroupInvite> builder)
    {
        builder.ToTable(nameof(GroupInvite));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.InviteeEmail).IsRequired(false);
        builder.Property(x => x.IsAccepted).HasDefaultValue(false);

        builder.HasOne(x => x.Group)
            .WithMany()
            .HasForeignKey(x => x.GroupId)
            .IsRequired();

        builder.HasOne(x => x.Inviter as ApplicationUser)
            .WithMany()
            .HasForeignKey(x => x.InviterId)
            .IsRequired();

        builder.HasOne(x => x.Invitee as ApplicationUser)
            .WithMany()
            .HasForeignKey(x => x.InviteeId)
            .IsRequired(false);
    }
}
