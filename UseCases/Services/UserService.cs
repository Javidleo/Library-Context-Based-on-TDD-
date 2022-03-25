using DomainModel;
using DomainModel.Validation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.ServiceContract;

namespace UseCases.Exceptions
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly UserValidation validation;
        public UserService(IUserRepository userRepository, IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
            _userRepository = userRepository;
            validation = new UserValidation();
        }

        public Task Create(string name, string family, int age, string nationalCode, string email,int adminId)
        {
            User user = User.Create(name, family, age, nationalCode, email,adminId);

            if (!validation.Validate(user).IsValid)
                throw new NotAcceptableException("Invalid User");

            _userRepository.Add(user);
            return Task.CompletedTask;
        }

        public Task<List<User>> GetAll()
        => Task.FromResult(_userRepository.GetAll());

        public Task<User> GetById(int id)
        {
            User user = _userRepository.Find(id);
            if (user == null)
                throw new NotFoundException("Not Founede");
            return Task.FromResult(user);
        }

        public Task Delete(int Id)
        {
            User user = _userRepository.FindWithBooks(Id);
            if (user is null)
                throw new NotFoundException("User Not Founded");

            //if (user.Interactions is null)
            //    throw new NotAcceptableException("Cannot Delete User how Also Have Books");

            _userRepository.Delete(user);
            return Task.CompletedTask;
        }

        public Task Update(int id, string name, string family, int age, string email,int adminid)
        {
            string dummyNationalCode = "0990076016";
            if (!validation.Validate(User.Create(name, family, age, dummyNationalCode, email,1)).IsValid)
                throw new NotAcceptableException("Invalid Informations");

            if (_userRepository.DoesEmailExist(email))
                throw new DuplicateException("Duplicate Email");

            var user = _userRepository.Find(id);
            if (user is null)
                throw new NotFoundException("User Not Found");

            
            user.Modify(name, family, age, email, adminid);

            _userRepository.Update(user);
            return Task.CompletedTask;
        }

        public Task<User> GetByName(string name)
        {
            var user = _userRepository.Find(name);
            if (user is null)
                throw new NotFoundException("User Not Founded");

            return Task.FromResult(user);
        }
    }
}