using BookDataAccess.Repository;
using DomainModel;
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
        [InlineData("", "usefamily", 15, "0990076016", "javidleo.ef@gmail.com", "Name Should not be Empty")]
        [InlineData("name13", "usefamily", 15, "0990076016", "javidleo.ef@gmail.com", "Name Should not have Numbers or Special Characters")]
        [InlineData("name#213", "usefamily", 15, "0990076016", "javidleo.ef@gmail.com", "Name Should not have Numbers or Special Characters")]
        public void UserValidation_ValidatingName_ThrowExcepteedMessage(string name, string family, int age, string nationalcode, string email, string errorMessage)
        {
            var user = User.Create(name, family, age, nationalcode, email);
            var result = validation.TestValidate(user);
            result.ShouldHaveValidationErrorFor(user => user.Name).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("User", "validation")]
        [InlineData("user", "", 15, "0990076016", "javidleo.ef@gmail.com", "Family Should not be Empty")]
        [InlineData("user", "fam324liy", 15, "0990076016", "javidleo.ef@gmail.com", "Family Should not have Numbers or Special Characters")]
        [InlineData("user", "family324$123", 15, "0990076016", "javidleo.ef@gmail.com", "Family Should not have Numbers or Special Characters")]
        public void UserValidation_ValidatingFamily_ThrowExcpectedMessage(string name, string family, int age, string nationalcode, string email, string errorMessage)
        {
            var user = User.Create(name, family, age, nationalcode, email);
            var result = validation.TestValidate(user);
            result.ShouldHaveValidationErrorFor(user => user.Family).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("User", "validation")]
        [InlineData("user", "usefamily", 11, "0990076016", "javidleo.ef@gmail.com", "Age Should be between 12,70")]
        [InlineData("user", "usefamily", 99, "0990076016", "javidleo.ef@gmail.com", "Age Should be between 12,70")]
        public void UserValidation_ValidatingAge_ThrowExcpectedMessage(string name, string family, int age, string nationalCode, string email, string errorMessage)
        {
            var user = User.Create(name, family, age, nationalCode, email);
            var result = validation.TestValidate(user);
            result.ShouldHaveValidationErrorFor(user => user.Age).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("User", "validation")]
        [InlineData("user", "usefamily", 45, "", "javidleo.ef@gmail.com", "NationalCode Should not be Empty")]
        [InlineData("user", "usefamily", 45, "1111111111", "javidleo.ef@gmail.com", "Invalid NationalCode")]
        [InlineData("user", "usefamily", 45, "6541321635216158", "javidleo.ef@gmail.com", "Invalid NationalCode")]
        [InlineData("user", "usefamily", 45, "4651461", "javidleo.ef@gmail.com", "Invalid NationalCode")]
        [InlineData("user", "usefamily", 45, "651", "javidleo.ef@gmail.com", "Invalid NationalCode")]
        [InlineData("user", "usefamily", 45, "4516816514", "javidleo.ef@gmail.com", "Invalid NationalCode")]

        public void UserValidation_ValidatingNationalCode_ThrowExcpectedMessage(string name, string family, int age, string nationalcode, string email, string errorMessage)
        {
            var user = User.Create(name, family, age, nationalcode, email);
            var result = validation.TestValidate(user);
            result.ShouldHaveValidationErrorFor(user => user.NationalCode).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("User", "validation")]
        [InlineData("user", "usefamily", 45, "0990076016", "javidsjf!!~~##@@")]
        [InlineData("user", "usefamily", 45, "0990076016", "32fsdf")]
        [InlineData("user", "usefamily", 45, "0990076016", "@@f;lsidjfew")]
        [InlineData("user", "usefamily", 45, "0990076016", "jaldifj2343123@")]
        [InlineData("user", "usefamily", 45, "0990076016", "javidslf3.krjew023")]
        public void UserValidation_ValidatingEmail_ThrowExcpectedException(string name, string family, int age, string nationalcode, string email)
        {
            var user = User.Create(name, family, age, nationalcode, email);
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
