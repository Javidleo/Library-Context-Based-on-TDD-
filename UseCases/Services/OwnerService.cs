using DomainModel;
using DomainModel.Validation;
using System.Collections.Generic;
using System.Threading.Tasks;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.ServiceContract;

namespace UseCases.Exceptions
{
    public class OwnerService :IOwnerService
    {
        private readonly OwnerValidation validation;
        private readonly IOwnerRepository _repository;
        public OwnerService(IOwnerRepository repository)
        {
            validation = new OwnerValidation();
            _repository = repository;
        }

        public Task Create(string name, string family ,string nationalCode, string phoneNumber, string userName, string password)
        {
            Owner owner = Owner.Create(name, family, nationalCode, phoneNumber, userName, password);

            if (!validation.Validate(owner).IsValid)
                throw new NotAcceptableException("Invalid Owner");

            _repository.Add(owner);
            return Task.CompletedTask;

        }

        public Task<List<Owner>> GetAll()
        => Task.FromResult(_repository.GetAll());

        public Task<Owner> FindById(int id)
        {
            Owner owner = _repository.FindById(id);
            if (owner is null)
                throw new NotFoundException("Not Founded");
            return Task.FromResult(owner);
        }
    }
}