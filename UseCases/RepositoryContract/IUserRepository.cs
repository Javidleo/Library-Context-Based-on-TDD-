using DomainModel;
using System.Collections.Generic;

namespace UseCases.RepositoryContract
{
    public interface IUserRepository
    {
        void Add(User user);
        User Find(string name);
        List<User> GetAll();
        User Find(int Id);
        User FindWithBooks(int Id);
        void Update(User user);
        void Delete(User user);
        bool DoesEmailExist(string email);
    }
}
