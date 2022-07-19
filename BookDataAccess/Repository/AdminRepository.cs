using BookDataAccess.Repository.Abstraction;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UseCases.RepositoryContract;
using UseCases.ViewModel;

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

    public List<AdminListViewModel> GetAll()
    {
        var adminList = _bookContext.Admin;
        var result = adminList.Select(i => new AdminListViewModel
        {
            FullName = i.Name + " " + i.Family,
            UserName = i.UserName,
            Email = i.Email,
        }).AsNoTracking().ToList();

        return result;
    }
}
