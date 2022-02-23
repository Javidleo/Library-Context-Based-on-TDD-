using DomainModel;
using DomainModel.Validation;
using System.Threading.Tasks;
using UseCases.Exceptions;
using UseCases.RepositoryContract;

namespace UseCases.Services
{
    public class AdminService
    {
        private readonly AdminValidation validation;
        private readonly IAdminRepository _repository;
        public AdminService(IAdminRepository repository)
        {
            validation = new AdminValidation();
            _repository = repository;
        }

        public Task Create(string name, string family, string dateofBirth, string nationalCode, string userName, string email, string Password)
        {
            Admin admin = Admin.Create(name, family, dateofBirth, nationalCode, userName, email, Password);

            if (!validation.Validate(admin).IsValid)
                throw new NotAcceptableException("Invalid Inputs");

            _repository.Add(admin);

            return Task.CompletedTask;
        }
    }
}