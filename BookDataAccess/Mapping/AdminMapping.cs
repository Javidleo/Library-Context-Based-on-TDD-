using DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookDataAccess.Mapping
{
    public class AdminMapping : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name).ValueGeneratedNever();
            builder.Property(i => i.Family).ValueGeneratedNever();
            builder.Property(i => i.DateofBirth).ValueGeneratedNever();
            builder.Property(i => i.NationalCode).ValueGeneratedNever();
            builder.Property(i => i.Email).ValueGeneratedNever();
            builder.Property(i => i.Password).ValueGeneratedNever();
            builder.Property(i => i.UserName).ValueGeneratedNever();
        }
    }
}
