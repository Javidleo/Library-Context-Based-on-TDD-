using DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UseCases.ServiceContract
{
    public interface IUserService
    {
        Task Create(string name, string family, int age, string nationalCode, string email, int adminId);
        Task<List<User>> GetAll();
        Task<User> GetById(int id);
        Task Delete(int Id);
        Task Update(int id, string name, string family, int age, string email, int adminId);
    }
}
