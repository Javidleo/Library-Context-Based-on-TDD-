using DomainModel;
using DomainModel.Validation;
using FluentValidation.TestHelper;
using UseCases.Services;
using Xunit;

namespace BookTest.Unit
{
    public class UserTests
    {
        private readonly UserService service;
        private readonly UserValidation validator;
        public UserTests()
        {
            service = new UserService();
            validator = new UserValidation();
        }

        [Fact]
        [Trait("User", "validation")]
        public void UserValidation_ValidatingNullName_ShouldHaveError()
        {
            var user = User.Create(1, "", "family", 40, "123123", "jaivldo.efsdf.@gmail.com");
            var result = validator.TestValidate(user);
            result.ShouldHaveValidationErrorFor(user => user.Name)
                .WithErrorMessage("Invalid Name");
        }

        [Fact]
        [Trait("User", "vaidation")]
        public void UserValidation_ValidatingNotNullName_ShouldNotHaveError()
        {
            var user = User.Create(1, "ali", "reza", 40, "123123", "fasdfdsf");
            var result = validator.TestValidate(user);
            result.ShouldNotHaveValidationErrorFor(user => user.Name);
        }

        [Fact]
        [Trait("User", "validation")]
        public void UserValidation_ValidatingNullFamily_ShouldHaveError()
        {
            var user = User.Create(1, "ali", "", 40, "12313", "fdsfsd");
            var result = validator.TestValidate(user);
            result.ShouldHaveValidationErrorFor(user => user.Family)
                .WithErrorMessage("Invalid Family");
        }
    }
}
