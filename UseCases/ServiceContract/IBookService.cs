using DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UseCases.ServiceContract
{
    public interface IBookService
    {
        Task Create(string name, string authorName, string DateofAdding);
        Task<List<Book>> GetAll();
        Task<Book> GetById(int id);
    }
}
