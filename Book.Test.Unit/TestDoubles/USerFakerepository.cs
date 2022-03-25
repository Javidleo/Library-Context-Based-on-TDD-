using BookTest.Unit.Data.UserTestData;
using DomainModel;
using System.Collections.Generic;
using UseCases.RepositoryContract;

namespace BookTest.Unit.TestDoubles
{
    public class USerFakerepository : IUserRepository
    {
        private int id;
        private string name;
        private string family;
        private int age;
        private string nationalCode;
        private string email;
        private int adminId;

        public void SetExistingId(int id) => this.id = id;
        public void SetExistingName(string name) => this.name = name;
        public void SetExistingNationalCode(string nationalCode) => this.nationalCode = nationalCode;
        public void SetExisintEmail(string email) => this.email = email;

        public void Add(User user)
        {

        }

        public void Delete(User user)
        {

        }

        public bool DoesEmailExist(string email)
        {
            if (this.email == email) return true;
            return false;
        }

        public User Find(string name)
        {
            if (this.name == name) return new UserBuilder().Build();
            return null;
        }

        public User Find(int Id)
        {
            if (this.id == Id) return new UserBuilder().Build();
            return null;
        }

        public User FindWithBooks(int Id)
        {
            if (this.id == Id) return new UserBuilder().Build();
            return null;
        }

        public List<User> GetAll()
        {
            return new List<User>
            {
                new UserBuilder().Build()
            };
        }

        public void Update(User user)
        {
        }
    }
}
