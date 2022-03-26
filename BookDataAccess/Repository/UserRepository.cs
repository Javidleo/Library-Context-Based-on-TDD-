using DomainModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using UseCases.RepositoryContract;

namespace BookDataAccess.Repository;

public class UserRepository : IUserRepository
{
    private BookContext _context;
    public UserRepository(BookContext context)
    => _context = context;

    public void Add(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }
    public User Find(string name)
    => _context.Users.FirstOrDefault(i => i.Name == name);

    public List<User> GetAll()
    => _context.Users.ToList();

    public User Find(int id)
    => _context.Users.FirstOrDefault(i => i.Id == id);

    //public User FindWithBooks(int Id)
    //=> _context.Users.Include(i => i.Interactions).Where(i => i.Id == Id && i.Interactions.Any(i => i.IsDeleted == false)).FirstOrDefault();

    public User FindWithBooks(int Id)
    {
        var find = from user in _context.Users
                   join intearction in _context.Interactions on user.Id equals intearction.UserId

                   select new 
                   {
                       Id = user.Id,
                       Name = user.Name,
                       Family = user.Family,
                       Age = user.Age,
                       NationalCode = user.NationalCode,
                       Email = user.Email,
                       AdminId = user.AdminId,
                       ExpirationDate = user.ExpirationDate,
                       Interactions = user.Interactions.Where(i => i.IsDeleted == false)
                   };
        return null;
    }

    public void Update(User user)
    {
        _context.Update(user);
        _context.SaveChanges();
    }

    public void Delete(User user)
    {
        _context.Remove(user);
        _context.SaveChanges();
    }

    public bool DoesEmailExist(string email)
    => _context.Users.Any(i => i.Email == email);

    public bool DoesNationalCodeExist(string nationalCode)
    => _context.Users.Any(i => i.NationalCode == nationalCode);
}
