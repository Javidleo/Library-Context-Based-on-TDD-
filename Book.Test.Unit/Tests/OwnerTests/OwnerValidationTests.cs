using BookTest.Unit.Data.OwnerTestData;
using DomainModel.Validation;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookTest.Unit.Tests.OwnerTests;

public class OwnerValidationTests
{
    private readonly OwnerValidation _validation;
    public OwnerValidationTests()
    => _validation = new OwnerValidation();

    [Theory, Trait("Owner", "validation")]
    [InlineData("name123@", "Name Should not have Special Characters")]
    [InlineData("", "Name Should not be Empty")]
    public void OwnerValidation_ValidatingName_ThrowExcpectedMessage(string name, string errorMessage)
    {
        var owner = new OwnerBuilder().WithName(name).Build();
        var result = _validation.TestValidate(owner);
        result.ShouldHaveValidationErrorFor(owner => owner.Name).WithErrorMessage(errorMessage);
    }

    [Theory, Trait("Owner", "validation")]
    [InlineData("family#123", "Family Should not have Special Characters")]
    [InlineData("", "Family Should not be Empty")]
    public void OwnerValidation_ValidatingFamily_ThrowExceptedMessage(string family, string errorMessage)
    {
        var owner = new OwnerBuilder().WithFamily(family).Build();
        var result = _validation.TestValidate(owner);
        result.ShouldHaveValidationErrorFor(owner => owner.Family).WithErrorMessage(errorMessage);
    }

    [Theory, Trait("Owner", "validation")]
    [InlineData("", "NationalCode Should not be Empty")]
    [InlineData("3214123", "Invalid NationalCode")]
    public void OwnerValidation_ValidatingNationlCode_ThrowExceptedMessage(string nationalCode, string errorMessage)
    {
        var owner = new OwnerBuilder().WithNationalCode(nationalCode).Build();
        var result = _validation.TestValidate(owner);
        result.ShouldHaveValidationErrorFor(owner => owner.NationalCode).WithErrorMessage(errorMessage);
    }

    [Theory, Trait("Owner", "validation")]
    [InlineData("", "UserName Should not be Empty")]
    [InlineData("user@4Javid!!", "Invalid UserName")]

    public void OwnerValidation_ValidatingUserName_ThrowExcepetedMessage(string username, string errorMessage)
    {
        var owner = new OwnerBuilder().WithUserName(username).Build();
        var result = _validation.TestValidate(owner);
        result.ShouldHaveValidationErrorFor(owner => owner.UserName).WithErrorMessage(errorMessage);
    }

    [Theory, Trait("Owner", "validation")]
    [InlineData("", "Password Should not be Empty")]
    [InlineData("12313", "week Password")]

    public void OwnerValidationn_ValidatingPassword_ThrowExceptedMessage(string password, string errorMessage)
    {
        var owner = new OwnerBuilder().WithPassword(password).Build();
        var result = _validation.TestValidate(owner);
        result.ShouldHaveValidationErrorFor(owner => owner.Password).WithErrorMessage(errorMessage);
    }

    [Theory, Trait("Owner", "validation")]
    [InlineData("3123123", "Invalid PhoneNumber")]
    [InlineData("", "PhoneNumber Should not be Empty")]
    [InlineData("03434567540", "Invalid PhoneNumber")]
    [InlineData("19434567540", "Invalid PhoneNumber")]
    public void OwnerValidation_ValidatingPhoneNumber_ThrowExceptedMessage(string phoneNumber, string errorMessage)
    {
        var owner = new OwnerBuilder().WithPhoneNumber(phoneNumber).Build();
        var result = _validation.TestValidate(owner);
        result.ShouldHaveValidationErrorFor(owner => owner.PhoneNumber).WithErrorMessage(errorMessage);
    }

}
