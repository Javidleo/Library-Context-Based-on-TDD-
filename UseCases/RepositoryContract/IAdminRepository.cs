using DomainModel;
using UseCases.RepositoryContract.Abstraction;

namespace UseCases.RepositoryContract
{
    public interface IAdminRepository : IBaseRepository<Admin>
    {
        bool DoesNationalCodeExist(string nationalCode);
        bool DoesUsernameExist(string username);
        bool DoesEmailExist(string email);
        Admin GetByNationalCode(string nationalCode);
        Admin Find(string name);
    }
}