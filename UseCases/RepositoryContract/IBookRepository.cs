using DomainModel;
using System.Collections.Generic;
using UseCases.RepositoryContract.Abstraction;

namespace UseCases.RepositoryContract
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Book Find(string name);
        List<Book> FindByAddingDate(string dateofAdding);
        bool DoesNameExist(string name);
    }
}