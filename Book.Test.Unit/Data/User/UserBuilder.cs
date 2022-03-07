namespace BookTest.Unit.Data.User
{
    public class UserBuilder
    {
        private string _name;
        private string _family;
        private string _nationalCode;
        private int _age;
        private string _email;

        public UserBuilder WithName(string name)
        {
            this._name = name;
            return this;
        }
        public UserBuilder WithFamily(string family)
        {
            this._family = family;
            return this;
        }
        public UserBuilder WithNationalCode(string nationalCode)
        {
            this._nationalCode = nationalCode;
            return this;
        }
        public UserBuilder WithEmail(string email)
        {
            this._email = email;
            return this;
        }
        public UserBuilder WithAge(int age)
        {
            this._age = age;
            return this;
        }
        public DomainModel.User Build()
        => DomainModel.User.Create(_name, _family, _age, _nationalCode, _email);

    }
}
