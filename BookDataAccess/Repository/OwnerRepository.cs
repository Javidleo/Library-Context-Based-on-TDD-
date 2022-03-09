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

    public Owner FindById(int id)
    => _context.Owner.FirstOrDefault(i => i.Id == id);

    public List<Owner> GetAll()
    => _context.Owner.ToList();
}
