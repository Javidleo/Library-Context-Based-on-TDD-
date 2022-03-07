using DomainModel;
using System.Collections.Generic;

namespace UseCases.RepositoryContract
{
    public interface IAdminRepository
    {
        void Add(Admin admin);
        List<Admin> GetAll();
        bool DoesExist(string nationalCode);
        Admin GetByNationalCode(string nationalCode);
        void Delete(Admin admin);
        Admin Find(int id);
        Admin Find(string name);
    }
}