using BookTest.Unit.Data.User;
using DomainModel.Validation;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.Services;
using Xunit;

namespace BookTest.Unit
{
    public class UserTests
    {
        private readonly UserService service;
        private readonly UserValidation validation;
        private readonly Mock<IUserRepository> userRepositoryMock;
        private readonly MockRepository mockRepository;
        public UserTests()
        {
            mockRepository = new MockRepository(MockBehavior.Loose);
            userRepositoryMock = mockRepository.Create<IUserRepository>();
            service = new UserService(userRepositoryMock.Object);
            validation = new UserValidation();
        }

        [Theory, Trait("User", "validation")]
        [InlineData("", "Name Should not be Empty")]
        [InlineData("name13", "Name Should not have Numbers or Special Characters")]
        [InlineData("name#2", "Name Should not have Numbers or Special Characters")]
        public void UserValidation_ValidatingName_ThrowExcepteedMessage(string name, string errorMessage)
        {
            var user = new UserBuilder().WithName(name).Build();
            var result = validation.TestValidate(user);
            result.ShouldHaveValidationErrorFor(user => user.Name).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("User", "validation")]
        [InlineData("", "Family Should not be Empty")]
        [InlineData("fam324liy", "Family Should not have Numbers or Special Characters")]
        [InlineData("family34$3", "Family Should not have Numbers or Special Characters")]
        public void UserValidation_ValidatingFamily_ThrowExcpectedMessage(string family, string errorMessage)
        {
            var user = new UserBuilder().WithFamily(family).Build();
            var result = validation.TestValidate(user);
            result.ShouldHaveValidationErrorFor(user => user.Family).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("User", "validation")]
        [InlineData(11, "Age Should be between 12,70")]
        [InlineData(99, "Age Should be between 12,70")]
        public void UserValidation_ValidatingAge_ThrowExcpectedMessage(int age, string errorMessage)
        {
            var user = new UserBuilder().WithAge(age).Build();
            var result = validation.TestValidate(user);
            result.ShouldHaveValidationErrorFor(user => user.Age).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("User", "validation")]
        [InlineData("", "NationalCode Should not be Empty")]
        [InlineData("1111111111", "Invalid NationalCode")]
        [InlineData("65413216352", "Invalid NationalCode")]
        [InlineData("4651461", "Invalid NationalCode")]
        [InlineData("651", "Invalid NationalCode")]
        [InlineData("4516816514", "Invalid NationalCode")]

        public void UserValidation_ValidatingNationalCode_ThrowExcpectedMessage(string nationalcode, string errorMessage)
        {
            var user = new UserBuilder().WithNationalCode(nationalcode).Build();
            var result = validation.TestValidate(user);
            result.ShouldHaveValidationErrorFor(user => user.NationalCode).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("User", "validation")]
        [InlineData("javidsjf!!~~##@@")]
        [InlineData("32fsdf")]
        [InlineData("@@f;lsidjfew")]
        [InlineData("jaldifj2343123@")]
        [InlineData("javidslf3.krjew023")]
        public void UserValidation_ValidatingEmail_ThrowExcpectedException(string email)
        {
            var user = new UserBuilder().WithEmail(email).Build();
            var result = validation.TestValidate(user);
            result.ShouldHaveValidationErrorFor(user => user.Email);
        }

        [Fact, Trait("User", "create")]
        public void CreateUser_CheckforCreatingSuccessfully_ReturnSuccessTaskStatus()
        {
            var result = service.Create("ali", "rezaie", 16, "0990076016", "javidleo.ef@gmail.com");
            result.Status.ToString().Should().Be("RanToCompletion");
        }
        [Fact, Trait("User", "Create")]
        public void CreateUser_CheckForCreatingWithInvalidValues_ThrowNotAcceptableExcpetion()
        {
            void result() => service.Create("ali", "reza@#", 15, "0990076016", "javidleo.ef@gmial.com");
            Assert.Throws<NotAcceptableException>(result);
        }
    }
}
