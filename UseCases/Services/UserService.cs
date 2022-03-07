using DomainModel;
using DomainModel.Validation;
using System.Collections.Generic;
using System.Threading.Tasks;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.ServiceContract;

namespace UseCases.Services
{
    public class UserService : IUserService
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

        public Task<List<User>> GetAll()
        => Task.FromResult(_repository.GetAll());

        public Task<User> GetById(int id)
        {
            User user = _repository.FindById(id);
            if (user == null)
                throw new NotFoundException("Not Founede");
            return Task.FromResult(user);
        }
    }
}