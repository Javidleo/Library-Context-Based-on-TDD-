using DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UseCases.ServiceContract
{
    public interface IOwnerService
    {
        Task Create(string name, string family, string nationalCode, string phoneNumber, string userName, string password);
        Task<List<Owner>> GetAll();
        Task<Owner> FindById(int id);
    }
}