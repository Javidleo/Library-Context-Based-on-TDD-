namespace BookTest.Unit.Data.Admin
{
    public class AdminBuilder
    {
        private string _name;
        private string _family;
        private string _dateofBirth;
        private string _nationalCode;
        private string _userName;
        private string _email;
        private string _password;

        public AdminBuilder WithName(string name)
        {
            _name = name;
            return this;
        }
        public AdminBuilder WithFamily(string family)
        {
            _family = family;
            return this;
        }
        public AdminBuilder WithDateofBirth(string dateofBirth)
        {
            _dateofBirth = dateofBirth;
            return this;
        }
        public AdminBuilder WithNationalCode(string nationalCode)
        {
            _nationalCode = nationalCode;
            return this;
        }
        public AdminBuilder WithUserName(string userName)
        {
            _userName = userName;
            return this;
        }
        public AdminBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }
        public AdminBuilder WithPassword(string password)
        {
            _password = password;
            return this;
        }
        public DomainModel.Admin Build()
        {
            DomainModel.Admin admin = DomainModel.Admin.Create(_name, _family, _dateofBirth, _nationalCode, _userName, _email, _password);
            return admin;
        }
    }
}
