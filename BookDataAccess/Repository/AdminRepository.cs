using BookDataAccess.Repository.Abstraction;
using DomainModel;
using System.Linq;
using UseCases.RepositoryContract;

namespace BookDataAccess.Repository;

public class AdminRepository : BaseRepository<Admin>, IAdminRepository
{
    private readonly BookContext _bookContext;

    public AdminRepository(BookContext bookContext) : base(bookContext)
    {
        _bookContext = bookContext;
    }

    public bool DoesNationalCodeExist(string nationalCode)
    => _bookContext.Admin.Any(i => i.NationalCode == nationalCode);

    public Admin GetByNationalCode(string nationalCode)
    => _bookContext.Admin.FirstOrDefault(i => i.NationalCode == nationalCode);

    public Admin Find(string name)
    => _bookContext.Admin.FirstOrDefault(i => i.Name == name);

    public bool DoesUsernameExist(string username)
    => _bookContext.Admin.Any(i => i.UserName == username);

    public bool DoesEmailExist(string email)
    => _bookContext.Admin.Any(i => i.Email == email);
}
