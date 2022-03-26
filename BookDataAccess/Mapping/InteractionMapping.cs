using DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookDataAccess.Mapping;

public class InteractionMapping : IEntityTypeConfiguration<Interaction>
{
    public void Configure(EntityTypeBuilder<Interaction> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Date).ValueGeneratedNever();
        builder
            .HasOne(i => i.Admin)
            .WithMany(i => i.Interactions)
            .HasForeignKey(i => i.AdminId);
        builder
            .HasOne(i => i.User)
            .WithMany(i => i.Interactions)
            .HasForeignKey(i => i.UserId);
        builder
            .HasOne(i => i.Book)
            .WithMany(i => i.Interactions)
            .HasForeignKey(i => i.BookId);
    }
}
