using BookDataAccess.Repository;
using BookTest.Data.User;
using BookTest.Unit.Data.User;
using DomainModel;
using DomainModel.Validation;
using FluentAssertions;
using FluentValidation.TestHelper;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.Services;
using Xunit;

namespace BookTest.Unit
{
    public class UserTests
    {
        private readonly UserService service;
        private readonly UserValidation validator;
        private readonly IUserRepository _repository;
        public UserTests()
        {
            _repository = new UserRepository();
            service = new UserService(_repository);
            validator = new UserValidation();
        }

        [Fact]
        [Trait("User", "validation")]
        public void UserValidation_ValidatingNullName_ShouldHaveError()
        {
            var user = User.Create("", "family", 40, "123123", "jaivldo.efsdf.@gmail.com");
            var result = validator.TestValidate(user);
            result.ShouldHaveValidationErrorFor(user => user.Name)
                .WithErrorMessage("Invalid Name");
        }

        [Fact]
        [Trait("User", "vaidation")]
        public void UserValidation_ValidatingNotNullName_ShouldNotHaveError()
        {
            var user = User.Create("ali", "reza", 40, "123123", "fasdfdsf");
            var result = validator.TestValidate(user);
            result.ShouldNotHaveValidationErrorFor(user => user.Name);
        }

        [Fact]
        [Trait("User", "validation")]
        public void UserValidation_ValidatingNullFamily_ShouldHaveError()
        {
            var user = User.Create("ali", "", 40, "12313", "fdsfsd");
            var result = validator.TestValidate(user);
            result.ShouldHaveValidationErrorFor(user => user.Family)
                .WithErrorMessage("Invalid Family");
        }

        [Theory, Trait("User", "create")]
        [UserValidTestData]
        public void CreateUser_CheckForCreatingSuccessfully_ReturnSuccessTaskStatus(string name, string family, int age, string nationalCode, string email)
        {
            var result = service.Create(name, family, age, nationalCode, email);
            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Theory, Trait("User", "create")]
        [UserInvalidTestData]
        public void CreateUser_CheckForCreatingWithInvalidValues_ThrowNotAcceptableExcepiton(string name, string family, int age, string nationalcode, string email)
        {
            void result() => service.Create(name, family, age, nationalcode, email);
            Assert.Throws<NotAcceptableException>(result);
        }
    }
}
