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

            if (_repository.DoesNationalCodeExist(admin.NationalCode))
                throw new DuplicateException("Duplicate NationalCode");

            if (_repository.DoesUsernameExist(admin.UserName))
                throw new DuplicateException("Duplicate Username");

            if (_repository.DoesEmailExist(admin.Email))
                throw new DuplicateException("Duplicate Email");

            _repository.Add(admin);
            return Task.CompletedTask;
        }

        public Task<List<Admin>> GetAll()
        => Task.FromResult(_repository.FindAll());

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

        public Task Update(int id, string name, string family, string dateofBirth, string username, string email, string password)
        {

            var admin = _repository.Find(id);

            if (admin is null)
                throw new NotFoundException("Not Founded");

            if (_repository.DoesEmailExist(email))
                throw new DuplicateException("Duplicate Email");

            if (_repository.DoesUsernameExist(username))
                throw new DuplicateException("Duplicate Username");

            admin.Modify(name, family, dateofBirth, username, email, password);

            if (!validation.Validate(admin).IsValid)
                throw new NotAcceptableException("Invalid Admin");

            _repository.Update(admin);
            return Task.CompletedTask;
        }
    }
}