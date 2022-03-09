using DomainModel;
using System.Collections.Generic;
using System.Linq;
using UseCases.RepositoryContract;

namespace BookDataAccess.Repository;

public class AdminRepository : IAdminRepository
{
    private readonly BookContext _context;
    public AdminRepository(BookContext context) => _context = context;

    public void Add(Admin admin)
    {
        _context.Admin.Add(admin);
        _context.SaveChanges();
    }

    public List<Admin> GetAll()
    => _context.Admin.ToList();

    public bool DoesExist(string nationalCode)
    => _context.Admin.Any(i => i.NationalCode == nationalCode);

    public Admin GetByNationalCode(string nationalCode)
    => _context.Admin.FirstOrDefault(i => i.NationalCode == nationalCode);

    public void Delete(Admin admin)
    {
        _context.Admin.Remove(admin);
        _context.SaveChanges();
    }

    public Admin Find(int id)
    => _context.Admin.FirstOrDefault(i => i.Id == id);

    public Admin Find(string name)
    => _context.Admin.FirstOrDefault(i => i.Name == name);
}
