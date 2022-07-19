using DomainModel;
using UseCases.RepositoryContract.Abstraction;

namespace UseCases.RepositoryContract
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User Find(string name);
        User FindWithBooks(int Id);
        bool DoesEmailExist(string email);
        bool DoesNationalCodeExist(string nationalCode);
    }
}
