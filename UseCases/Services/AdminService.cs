using DomainModel;
using DomainModel.Validation;
using System.Collections.Generic;
using System.Threading.Tasks;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.ServiceContract;

namespace UseCases.Services
{
    public class AdminService : IAdminService
    {
        private readonly AdminValidation validation;
        private readonly IAdminRepository _repository;
        public AdminService(IAdminRepository repository)
        {
            validation = new AdminValidation();
            _repository = repository;
        }

        public Task Create(string name, string family, string dateofBirth, string natioanlCode, string userName, string email, string password)
        {
            Admin admin = Admin.Create(name, family, dateofBirth, natioanlCode, userName, email, password);
            if (!validation.Validate(admin).IsValid)
                throw new NotAcceptableException("Invalid Inputs");

            if (_repository.DoesExist(admin.NationalCode))
                throw new NotAcceptableException("User not Exist in Db");

            _repository.Add(admin);
            return Task.CompletedTask;
        }

        public Task<List<Admin>> GetAll()
        => Task.FromResult(_repository.GetAll());

        public Task<Admin> GetByNationalCode(string nationalCode)
        {
            if (string.IsNullOrEmpty(nationalCode))
                throw new NotAcceptableException("NationalCode is empty");
            Admin admin = _repository.GetByNationalCode(nationalCode);
            if (admin == null)
                throw new NotFoundException("Not Founded");

            return Task.FromResult(admin);
        }

        public Task Delete(int id)
        {
            Admin admin = _repository.Find(id);
            if (admin is null)
                throw new NotFoundException("Admin Not Found");

            _repository.Delete(admin);
            return Task.CompletedTask;
        }
    }
}