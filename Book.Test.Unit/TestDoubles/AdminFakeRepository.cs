using BookTest.Unit.Data.AdminTestData;
using DomainModel;
using System.Collections.Generic;
using UseCases.RepositoryContract;

namespace BookTest.Unit.TestDoubles
{
    public class AdminFakeRepository : IAdminRepository
    {
        private string validEmail;
        private string validNationalCode;
        private string validUserName;
        private int validId;
        private string validName;

        public void SetExistingEmail(string email) => validEmail = email;

        public void SetExistingNationalCode(string nationalCode) => validNationalCode = nationalCode;

        public void SetExistingUserName(string userName) => validUserName = userName;

        public void SetExistingId(int id) => validId = id;

        public void SetExistingName(string name) => validName = name;

        public void Add(Admin admin) { }

        public void Delete(Admin admin) { }

        public bool DoesEmailExist(string email)
        {
            if (email == validEmail) return true;
            return false;
        }

        public bool DoesNationalCodeExist(string nationalCode)
        {
            if (nationalCode == validNationalCode) return true;
            return false;
        }

        public bool DoesUsernameExist(string username)
        {
            if (username == validUserName) return true;
            return false;
        }

        public Admin Find(int id)
        {
            if (id == validId) return new AdminBuilder().Build();
            return null;
        }

        public Admin Find(string name)
        {
            if (name == validName) return new AdminBuilder().Build();
            return null;
        }

        public List<Admin> FindAll()
        {
            var list = new List<Admin>()
            {
                new AdminBuilder().Build(),
                new AdminBuilder().Build(),
                new AdminBuilder().Build()
            };
            return list;
        }

        public Admin GetByNationalCode(string nationalCode)
        {
            if (nationalCode == validNationalCode) return new AdminBuilder().Build();
            return null;
        }

        public void Update(Admin admin) { }
    }
}
