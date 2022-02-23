using BookDataAccess.Mapping;
using DomainModel;
using Microsoft.EntityFrameworkCore;

namespace BookDataAccess
{
    public class BookContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Book> Book { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-MONHQ70;Database=bookdb;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookContext).Assembly);
            modelBuilder.ApplyConfiguration(new AdminMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new BookMappping());
        }
    }
}
