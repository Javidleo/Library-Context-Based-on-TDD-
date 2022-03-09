using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTest.Unit.Data.OwnerTestData;

public class OwnerBuilder
{
    private string _name = "javid";
    private string _family = "rezaie";
    private string _nationalCode = "0738845736";
    private string _userName = "javid";
    private string _phoneNumber = "09166034678";
    private string _password = "javid@3421jJ";

    public OwnerBuilder WithName(string name)
    {
        _name = name;
        return this;
    }
    public OwnerBuilder WithFamily(string family)
    {
        _family = family;
        return this;
    }
    public OwnerBuilder WithNationalCode(string nationalCode)
    {
        _nationalCode = nationalCode;
        return this;
    }
    public OwnerBuilder WithUserName(string username)
    {
        _userName = username;
        return this;
    }
    public OwnerBuilder WithPhoneNumber(string phoneNumber)
    {
        _phoneNumber = phoneNumber;
        return this;
    }
    public OwnerBuilder WithPassword(string password)
    {
        _password = password;
        return this;
    }
    public DomainModel.Owner Build()
    => DomainModel.Owner.Create(_name, _family, _nationalCode, _phoneNumber, _userName, _password);
}
