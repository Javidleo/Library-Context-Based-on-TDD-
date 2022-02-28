using BookDataAccess.Mapping;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BookDataAccess
{
    public class BookContext : DbContext
    {
        private readonly IConfiguration _config;
        public BookContext(DbContextOptions<BookContext> options, IConfiguration config) : base(options)
        {
            _config = config;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Owner> Owner { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured is false)
                optionsBuilder.UseSqlServer(_config.GetConnectionString("LocalDb"));
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
