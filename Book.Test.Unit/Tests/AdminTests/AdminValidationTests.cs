using BookTest.Unit.Data.AdminTestData;
using DomainModel.Validation;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookTest.Unit.Tests.AdminTests
{
    public  class AdminValidationTests
    {
        private readonly AdminValidation _validation;
        public AdminValidationTests()
        {
            _validation = new AdminValidation();
        }

        [Theory, Trait("Admin", "validation")]
        [InlineData("", "Name Should not be Empty")]
        [InlineData("ali123", "Name Should not have Numbers or Special Characters")]
        [InlineData("ali@", "Name Should not have Numbers or Special Characters")]
        public void AdminValidation_ValidatingName_ThrowExcpectedMessage(string name, string errorMessage)
        {
            var admin = new AdminBuilder().WithName(name).Build();
            var result = _validation.TestValidate(admin);
            result.ShouldHaveValidationErrorFor(admin => admin.Name).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Admin", "validation")]
        [InlineData("", "Family Should not be Empty")]
        [InlineData("re1zied", "Family Should not have Numbers or Special Characters")]
        [InlineData("red#wer", "Family Should not have Numbers or Special Characters")]
        public void AdminValidation_ValidatingFamily_ThrowExcpectedMessage(string family, string errorMessage)
        {
            var admin = new AdminBuilder().WithFamily(family).Build();
            var result = _validation.TestValidate(admin);
            result.ShouldHaveValidationErrorFor(admin => admin.Family).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Admin", "validation")]
        [InlineData("", "DateofBirth Should not be Empty")]
        [InlineData("11/14/1366", "Invalid DateofBirth")]
        [InlineData("11/11/13555", "Invalid DateofBirth")]
        [InlineData("45/12/1355", "Invalid DateofBirth")]
        public void AdminValidation_ValidatingDateofBirth_ThrowExcpectedMessage(string dateofbirth, string errorMessage)
        {
            var admin = new AdminBuilder().WithDateofBirth(dateofbirth).Build();
            var result = _validation.TestValidate(admin);
            result.ShouldHaveValidationErrorFor(admin => admin.DateofBirth).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Admin", "validation")]
        [InlineData("", "NationalCode Should not be Empty")]
        [InlineData("1111111111", "Invalid NationalCode")]
        [InlineData("1239128301212312", "Invalid NationalCode")]
        [InlineData("1231231354", "Invalid NationalCode")]
        public void AdminValidation_ValidatingNationalCode_ThrowExceptedMessage(string nationalcode, string errorMessage)
        {
            var admin = new AdminBuilder().WithNationalCode(nationalcode).Build();
            var result = _validation.TestValidate(admin);
            result.ShouldHaveValidationErrorFor(admin => admin.NationalCode).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Admin", "validation")]
        [InlineData("", "UserName Should not be Empty")]
        [InlineData("ali%$@", "Invalid UserName")]
        [InlineData("AliBarati", "Invalid UserName")]
        public void AdminVlidation_ValidateingUserName_ThrowExcpectedMessage(string username, string errorMessage)
        {
            var admin = new AdminBuilder().WithUserName(username).Build();
            var result = _validation.TestValidate(admin);
            result.ShouldHaveValidationErrorFor(admin => admin.UserName).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Admin", "validation")]
        [InlineData("Javidlerw$432@")]
        [InlineData("javid123@$@#@#$@")]
        public void AdminValidation_ValidatingEmail_ThrowExcpectedMessage(string email)
        {
            var admin = new AdminBuilder().WithEmail(email).Build();
            var result = _validation.TestValidate(admin);
            result.ShouldHaveValidationErrorFor(admin => admin.Email);
        }

        [Theory, Trait("Admin", "validation")]
        [InlineData("", "Password Should not be Empty")]
        [InlineData("1231234324234", "week Password")]
        [InlineData("g#13G", "week Password")]
        public void AdminValidation_ValidatingPassword_ThrowExcpectedMessage(string password, string errorMessage)
        {
            var admin = new AdminBuilder().WithPassword(password).Build();
            var result = _validation.TestValidate(admin);
            result.ShouldHaveValidationErrorFor(admin => admin.Password).WithErrorMessage(errorMessage);
        }
    }
}
