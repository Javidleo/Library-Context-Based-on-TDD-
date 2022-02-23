using DomainModel;
using UseCases.RepositoryContract;

namespace BookDataAccess.Repository
{
    public class AdminRepository : IAdminRepository
    {

        public void Add(Admin admin)
        {
            using (var context = new BookContext())
            {
                context.Admin.Add(admin);
                context.SaveChanges();
            }
        }

    }
}
