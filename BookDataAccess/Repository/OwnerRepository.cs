using DomainModel;
using System.Collections.Generic;
using System.Linq;
using UseCases.RepositoryContract;

namespace BookDataAccess.Repository;

public class OwnerRepository : IOwnerRepository
{
    private readonly BookContext _context;
    public OwnerRepository(BookContext context)
    => _context = context;

    public void Add(Owner owner)
    {
        _context.Owner.Add(owner);
        _context.SaveChanges();
    }

    public void Delete(Owner owner)
    {
        _context.Remove(owner);
        _context.SaveChanges();
    }

    public bool DoesNationalCodeExist(string nationalCode)
    => _context.Owner.Any(i => i.NationalCode == nationalCode);

    public Owner Find(int id)
    => _context.Owner.FirstOrDefault(i => i.Id == id);

    public Owner Find(string name)
    => _context.Owner.FirstOrDefault(i => i.Name == name);

    public List<Owner> GetAll()
    => _context.Owner.ToList();

    public void Update(Owner owner)
    {
        _context.Update(owner);
        _context.SaveChanges();
    }
}
