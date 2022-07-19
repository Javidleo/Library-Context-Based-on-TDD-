using DomainModel;
using DomainModel.Validation;
using System.Collections.Generic;
using System.Threading.Tasks;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.ServiceContract;

namespace UseCases.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly OwnerValidation validation;
        private readonly IOwnerRepository _repository;
        public OwnerService(IOwnerRepository repository)
        {
            validation = new OwnerValidation();
            _repository = repository;
        }
        // ///////////////////////////   Create 
        public Task Create(string name, string family, string nationalCode, string phoneNumber, string userName, string password)
        {
            Owner owner = Owner.Create(name, family, nationalCode, phoneNumber, userName, password);

            if (!validation.Validate(owner).IsValid)
                throw new NotAcceptableException("Invalid Owner");

            if (_repository.DoesExist(i=> i.NationalCode == nationalCode))
                throw new DuplicateException("Duplicate NationalCode");

            if (_repository.DoesExist(i => i.PhoneNumber == phoneNumber))
                throw new DuplicateException("Duplciate Phone Number");

            if (_repository.DoesExist(i => i.UserName == userName))
                throw new DuplicateException("Duplicate User Name");

            _repository.Add(owner);
            return Task.CompletedTask;
        }

        // //////////////////////////////////    Get All
        public Task<List<Owner>> GetAll()
        => Task.FromResult(_repository.FindAll());

        // ///////////////////////////////////   Find By Id
        public Task<Owner> FindById(int id)
        {
            Owner owner = _repository.Find(id);
            if (owner is null)
                throw new NotFoundException("Not Founded");
            return Task.FromResult(owner);
        }
    }
}