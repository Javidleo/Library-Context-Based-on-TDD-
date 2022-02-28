using DomainModel;
using System.Collections.Generic;

namespace UseCases.RepositoryContract
{
    public interface IBookRepository
    {
        void Add(Book book);
        List<Book> GetAll();
        Book FindById(int id);
    }
}