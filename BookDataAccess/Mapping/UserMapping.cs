using DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookDataAccess.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).ValueGeneratedNever();
            builder.Property(x => x.Family).ValueGeneratedNever();
            builder.Property(x => x.Age).ValueGeneratedNever();
            builder.Property(x => x.NationalCode).ValueGeneratedNever();
        }
    }
}
