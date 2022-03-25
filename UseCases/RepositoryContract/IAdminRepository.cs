using DomainModel;
using System.Collections.Generic;

namespace UseCases.RepositoryContract
{
    public interface IAdminRepository
    {
        void Add(Admin admin);
        List<Admin> GetAll();
        bool DoesNationalCodeExist(string nationalCode);
        bool DoesUsernameExist(string username);
        bool DoesEmailExist(string email);
        Admin GetByNationalCode(string nationalCode);
        void Delete(Admin admin);
        Admin Find(int id);
        Admin Find(string name);
        void Update(Admin admin);
    }
}