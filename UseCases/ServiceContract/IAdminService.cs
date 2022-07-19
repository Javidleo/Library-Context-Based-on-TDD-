using DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using UseCases.ViewModel;

namespace UseCases.ServiceContract
{
    public interface IAdminService
    {
        Task Create(string name, string family, string dateofBirth, string nationalCode, string userName, string email, string password);
        Task<List<AdminListViewModel>> GetAll();
        Task<Admin> GetByNationalCode(string nationalCode);
        Task Delete(int id);
    }
}
