using DomainModel;
using System.Collections.Generic;

namespace UseCases.RepositoryContract
{
    public interface IUserRepository
    {
        void Add(User user);
        User FindByName(string name);
        List<User> GetAll();
        User FindById(int Id);
        void Update(User user);
    }
}
