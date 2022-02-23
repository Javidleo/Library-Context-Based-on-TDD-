using DomainModel;
using UseCases.RepositoryContract;

namespace BookDataAccess.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext bookContext;
        public BookRepository()
        => bookContext = new BookContext();

        public void Add(Book book)
        => bookContext.Book.Add(book);
    }
}
