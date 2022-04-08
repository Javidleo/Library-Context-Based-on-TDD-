using BookDataAccess;
using Microsoft.EntityFrameworkCore;

namespace BookTest.Integration
{
    public class ContextOptionBuilderGenerator
    {
        public DbContextOptionsBuilder<BookContext> Build()
        {
            var optionBuilder = new DbContextOptionsBuilder<BookContext>();
            optionBuilder.UseSqlServer("Server=DESKTOP-MONHQ70;Database=bookdb;Trusted_Connection=True;");
            return optionBuilder;
        }
    }
}
