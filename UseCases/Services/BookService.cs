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

        public Task Create(string bookName, string authorName, string addingDate)
        {
            var book = Book.Create(bookName, authorName, addingDate);

            if (!validation.Validate(book).IsValid)
                throw new NotAcceptableException("Invalid Book");

            if (_repository.DoesNameExist(bookName))
                throw new DuplicateException("Duplicate Book");

            _repository.Add(book);
            return Task.CompletedTask;
        }

        public Task<List<Book>> GetAll()
        => Task.FromResult(_repository.FindAll());

        public Task<Book> GetById(int id)
        {
            Book book = _repository.Find(id);
            if (book == null)
                throw new NotFoundException("Not Founded");
            return Task.FromResult(book);
        }

        public Task<Book> GetByName(string name)
        {
            var book = _repository.Find(name);
            if (book is null)
                throw new NotFoundException("Book Not Founded");

            return Task.FromResult(book);
        }

        public Task Update(int Id, string name, string authorName, string dateofAdding)
        {
            if (_repository.DoesNameExist(name) is true)
                throw new DuplicateException("Duplicate Name");

            var book = _repository.Find(Id);
            if (book is null)
                throw new NotFoundException("Book Not Founded");

            _repository.Update(book);
            return Task.CompletedTask;
        }

        public Task Delete(int Id)
        {
            var book = _repository.Find(Id);
            if (book is null)
                throw new NotFoundException("Book Not Founded");

            if (book.InUse is true)
                throw new NotAcceptableException("Cannot Delete InUse Book");

            return Task.CompletedTask;
        }
    }
}