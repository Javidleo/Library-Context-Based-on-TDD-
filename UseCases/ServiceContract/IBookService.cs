using DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using UseCases.ViewModel;

namespace UseCases.ServiceContract
{
    public interface IBookService
    {
        Task Create(string name, string authorName, string DateofAdding);
        Task<List<BookListViewModel>> GetAll();
        Task Update(int id, string name, string authorname, string dateofadding);
        Task<Book> GetById(int id);
        Task<Book> GetByName(string name);
        Task Delete(int id);
    }
}
