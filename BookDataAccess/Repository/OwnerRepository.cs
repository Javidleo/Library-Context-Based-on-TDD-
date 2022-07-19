using BookDataAccess.Repository.Abstraction;
using DomainModel;
using System.Linq;
using UseCases.RepositoryContract;

namespace BookDataAccess.Repository;

public class OwnerRepository : BaseRepository<Owner>, IOwnerRepository
{
    private readonly BookContext _context;
    public OwnerRepository(BookContext bookContext) : base(bookContext)
    => _context = bookContext;

    public bool DoesNationalCodeExist(string nationalCode)
    => _context.Owner.Any(i => i.NationalCode == nationalCode);

    public Owner Find(string name)
    => _context.Owner.FirstOrDefault(i => i.Name == name);

}
