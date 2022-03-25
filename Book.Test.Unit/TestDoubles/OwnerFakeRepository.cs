using BookTest.Unit.Data.OwnerTestData;
using DomainModel;
using System.Collections.Generic;
using UseCases.RepositoryContract;

namespace BookTest.Unit.TestDoubles
{
    public class OwnerFakeRepository : IOwnerRepository
    {
        private int validId;
        private string validNationalCode;
        private string validName;

        public void SetValidId(int id) => validId = id;

        public void SetValidNationalCode(string nationalCode) => validNationalCode = nationalCode;

        public void SetValidName(string name) => validName = name;

        public void Add(Owner owner)
        {

        }

        public Owner Find(int id)
        {
            if (id == validId) return new OwnerBuilder().Build();
            return null;
        }

        public List<Owner> GetAll()
        {
            var list = new List<Owner>()
            {
                new OwnerBuilder().Build(),
                new OwnerBuilder().Build(),
                new OwnerBuilder().Build()
            };
            return list;
        }

        public Owner Find(string name)
        {
            if (name == validName) return new OwnerBuilder().Build();
            return null;
        }

        public bool DoesNationalCodeExist(string nationalCode)
        {
            if (nationalCode == validNationalCode) return true;
            return false;
        }

        public void Update(Owner owner)
        {
        }

        public void Delete(Owner owner)
        {
        }
    }

}
