using DomainModel;
using System.Linq;
using UseCases.RepositoryContract;

namespace BookDataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private BookContext _context;
        public UserRepository()
        => _context = new BookContext();

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public User FindByName(string name)
        => _context.Users.FirstOrDefault(i => i.Name == name);
    }
}
