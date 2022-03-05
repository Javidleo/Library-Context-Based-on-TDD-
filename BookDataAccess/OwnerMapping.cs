using DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookDataAccess
{
    public class OwnerMapping : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.HasKey(i=> i.Id);
            builder.Property(i => i.Name).ValueGeneratedNever();
            builder.Property(i => i.NationalCode).ValueGeneratedNever();
            builder.Property(i=> i.UserName).ValueGeneratedNever();
            builder.Property(i=> i.Password).ValueGeneratedNever();
            builder.Property(i => i.Family).ValueGeneratedNever();
        }
    }
}