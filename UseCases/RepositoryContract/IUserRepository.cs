using DomainModel;

namespace UseCases.RepositoryContract
{
    public interface IUserRepository
    {
        void Add(User user);
        User FindByName(string name);
    }
}
