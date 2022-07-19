using DomainModel;

namespace BookTest.Unit.Data.AdminTestData;

public class AdminBuilder
{
    private int _Id = 1;
    private string _name = "ali";
    private string _family = "rezaie";
    private string _dateofBirth = "11/12/1399";
    private string _nationalCode = "0738845736";
    private string _userName = "javid";
    private string _email = "javidleo@gmail.com";
    private string _password = "Jsdf24#342";

    public AdminBuilder WithId(int Id)
    {
        _Id = Id;
        return this;
    }
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
    public Admin Build()
    => Admin.CreateForTest(_Id, _name, _family, _dateofBirth, _nationalCode, _userName, _email, _password);
}
