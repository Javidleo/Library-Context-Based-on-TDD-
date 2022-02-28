using DomainModel;
using System.Collections.Generic;
using System.Linq;
using UseCases.RepositoryContract;

namespace BookDataAccess.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _context;
        public BookRepository(BookContext context)
        => _context = context;

        public void Add(Book book)
        {
            _context.Book.Add(book);
            _context.SaveChanges();
        }

        public List<Book> GetAll()
        => _context.Book.ToList();

        public Book FindById(int id)
        => _context.Book.FirstOrDefault(i => i.Id == id);
    }
}
