using DomainModel;

namespace BookTest.Unit.Data.UserTestData;

public class UserBuilder
{
    private int _adminId = 1;
    private string _name = "ali";
    private string _family = "rezaie";
    private string _nationalCode = "0738845736";
    private int _age = 16;
    private string _email = "javidleo.ef@gmail.com";

    public UserBuilder WithName(string name)
    {
        _name = name;
        return this;
    }
    public UserBuilder WithFamily(string family)
    {
        _family = family;
        return this;
    }
    public UserBuilder WithNationalCode(string nationalCode)
    {
        _nationalCode = nationalCode;
        return this;
    }
    public UserBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }
    public UserBuilder WithAge(int age)
    {
        _age = age;
        return this;
    }
    public DomainModel.User Build()
    => DomainModel.User.Create(_name, _family, _age, _nationalCode, _email, _adminId);

}
