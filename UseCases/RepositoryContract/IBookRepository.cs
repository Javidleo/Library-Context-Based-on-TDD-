using DomainModel;
using System.Collections.Generic;

namespace UseCases.RepositoryContract
{
    public interface IBookRepository
    {
        void Add(Book book);
        List<Book> GetAll();
        Book Find(string name);
        Book Find(int id);
        List<Book> FindByAddingDate(string dateofAdding);
        bool DoesNameExist(string name);
        void Update(Book book);
    }
}