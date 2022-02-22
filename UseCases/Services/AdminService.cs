using DomainModel;
using DomainModel.Validation;
using System.Threading.Tasks;
using UseCases.Exceptions;
namespace UseCases.Services
{
    public class AdminService
    {
        private readonly AdminValidation validation;
        public AdminService()
        => validation = new AdminValidation();

        public Task Create(int id, string name, string family, string dateofBirth, string nationalCode, string userName, string email, string Password)
        {
            Admin admin = Admin.Create(id, name, family, dateofBirth, nationalCode, userName, email, Password);

            if (!validation.Validate(admin).IsValid)
                throw new NotAcceptableException("Invalid Inputs");

            return Task.CompletedTask;
        }
    }
}