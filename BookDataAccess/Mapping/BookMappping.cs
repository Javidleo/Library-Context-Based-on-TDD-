using DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookDataAccess.Mapping
{
    public class BookMappping : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.Property(i => i.Name).ValueGeneratedNever();
            builder.Property(i => i.authorName).ValueGeneratedNever();
            builder.Property(i => i.DateofAdding).ValueGeneratedNever();
        }
    }
}
