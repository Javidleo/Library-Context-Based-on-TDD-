using DomainModel;
using DomainModel.Validation;
using System.Collections.Generic;
using System.Threading.Tasks;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.ServiceContract;

namespace UseCases.Services
{
    public class BookService : IBookService
    {
        private readonly BookValidation validation;
        private readonly IBookRepository _repository;
        public BookService(IBookRepository repository)
        {
            validation = new BookValidation();
            _repository = repository;
        }

        public Task Create(string BookName, string authorName, string AddingDate)
        {

            Book book = Book.Create(BookName, authorName, AddingDate);

            if (!validation.Validate(book).IsValid)
                throw new NotAcceptableException("Invalid Book");

            _repository.Add(book);
            return Task.CompletedTask;
        }

        public Task<List<Book>> GetAll()
        => Task.FromResult(_repository.GetAll());

        public Task<Book> FindById(int id)
        {
            Book book = _repository.Find(id);
            if (book == null)
                throw new KeyNotFoundException("Not Founded");
            return Task.FromResult(book);
        }
    }
}