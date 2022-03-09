using DomainModel;
using System.Collections.Generic;
using System.Linq;
using UseCases.RepositoryContract;

namespace BookDataAccess.Repository;

public class BookRepository : IBookRepository
{
    private readonly BookContext _context;
    public BookRepository(BookContext context)
    => _context = context;

    public void Add(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
    }

    public List<Book> GetAll()
    => _context.Books.ToList();

    public Book Find(int id)
    => _context.Books.FirstOrDefault(i => i.Id == id);

    public Book Find(string name)
    => _context.Books.FirstOrDefault(x => x.Name == name);

    public List<Book> FindByAddingDate(string dateofAdding)
    => _context.Books.Where(i => i.DateofAdding == dateofAdding).ToList();
}
