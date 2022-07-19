using BookDataAccess.Repository.Abstraction;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using UseCases.RepositoryContract;
using UseCases.ViewModel;

namespace BookDataAccess.Repository;

public class BookRepository : BaseRepository<Book>, IBookRepository
{
    private readonly BookContext _bookContext;
    public BookRepository(BookContext bookContext) : base(bookContext)
    => _bookContext = bookContext;

    public Book Find(string name)
    => _bookContext.Books.FirstOrDefault(x => x.Name == name);

    public List<Book> FindByAddingDate(string dateofAdding)
    => _bookContext.Books.Where(i => i.DateofAdding == dateofAdding).ToList();

    public bool DoesNameExist(string name)
    => _bookContext.Books.Any(i => i.Name == name);

    public List<BookListViewModel> GetAll()
    => _bookContext.Books.Select(i => new BookListViewModel
    {
        BookName = i.Name,
        AuthorName = i.AuthorName,
        DateOfAdding = i.DateofAdding,
        InUse = i.InUse
    }).AsNoTracking().ToList();
}
