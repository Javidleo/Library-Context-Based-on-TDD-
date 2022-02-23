using DomainModel;
using DomainModel.Validation;
using System.Threading.Tasks;
using UseCases.Exceptions;
using UseCases.RepositoryContract;

namespace UseCases.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;
        private readonly UserValidation validation;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
            validation = new UserValidation();
        }

        public Task Create(string name, string family, int age, string nationalCode, string email)
        {
            User user = User.Create(name, family, age, nationalCode, email);

            if (!validation.Validate(user).IsValid)
                throw new NotAcceptableException("Invalid User");

            _repository.Add(user);
            return Task.CompletedTask;
        }
    }
}