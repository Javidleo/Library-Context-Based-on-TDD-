using BookDataAccess.Mapping;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BookDataAccess
{
    public class BookContext : DbContext
    {
        private readonly IConfiguration _config;
        
        public BookContext(){}

        public BookContext(DbContextOptions<BookContext>option):base(option) { }


        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Owner> Owner { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured is false)
                optionsBuilder.UseSqlServer(("Server=DESKTOP-MONHQ70;Database=bookdb;Trusted_Connection=True;"));

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookContext).Assembly);
            modelBuilder.ApplyConfiguration(new AdminMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new BookMappping());
            modelBuilder.ApplyConfiguration(new OwnerMapping());
        }
    }
}
