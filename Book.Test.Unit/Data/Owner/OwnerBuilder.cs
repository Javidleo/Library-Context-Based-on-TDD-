using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTest.Unit.Data.Owner
{
    public class OwnerBuilder
    {
        private string _name;
        private string _family;
        private string _nationalCode;
        private string _userName;
        private string _phoneNumber;
        private string _password;

        public OwnerBuilder WithName(string name)
        {
            this._name = name;
            return this;
        }
        public OwnerBuilder WithFamily(string family)
        {
            this._family = family;
            return this;
        }
        public OwnerBuilder WithNationalCode(string nationalCode)
        {
            this._nationalCode = nationalCode;
            return this;
        }
        public OwnerBuilder WithUserName(string username)
        {
            this._userName = username;
            return this;
        }
        public OwnerBuilder WithPhoneNumber(string phoneNumber)
        {
            this._phoneNumber = phoneNumber;
            return this;
        }
        public OwnerBuilder WithPassword(string password)
        {
            this._password = password;
            return this;
        }
        public DomainModel.Owner Build()
        => DomainModel.Owner.Create(_name, _family, _nationalCode, _phoneNumber, _userName, _password);
    }
}
