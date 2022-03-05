using BookDataAccess.Repository;
using BookTest.Unit.Data.Admin;
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
    public class AdminTest
    {
        private readonly AdminService service;
        private readonly AdminValidation validation;
        private readonly Mock<IAdminRepository> adminRepositoryMock;
        private readonly MockRepository mockRepository;
        public AdminTest()
        {
            mockRepository = new MockRepository(MockBehavior.Loose);
            adminRepositoryMock= mockRepository.Create<IAdminRepository>();
            service = new AdminService(adminRepositoryMock.Object);
            validation = new AdminValidation();
        }

        [Theory, Trait("Admin", "validation")]
        [InlineData("", "rezaie", "11/12/1370", "0317144073", "user123", "javidleo.ef@gmail.com", "javid123@$", "Name Should not be Empty")]
        [InlineData("ali123", "rezaie", "11/12/1370", "0317144073", "user123", "javidleo.ef@gmail.com", "javid123@$", "Name Should not have Numbers or Special Characters")]
        [InlineData("ali@", "rezaie", "11/12/1370", "0317144073", "user123", "javidleo.ef@gmail.com", "javid123@$", "Name Should not have Numbers or Special Characters")]
        public void AdminValidation_ValidatingName_ThrowExcpectedMessage(string name, string family, string dateofbirth, string nationalcode, string username, string email, string passwords, string errorMessage)
        {
            var admin = Admin.Create(name, family, dateofbirth, nationalcode, username, email, passwords);
            var result = validation.TestValidate(admin);
            result.ShouldHaveValidationErrorFor(admin => admin.Name).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Admin", "validation")]
        [InlineData("ali", "", "11/12/1370", "0317144073", "user123", "javidleo.ef@gmail.com", "javid123@$", "Family Should not be Empty")]
        [InlineData("ali", "re1zied", "11/12/1370", "0317144073", "user123", "javidleo.ef@gmail.com", "javid123@$", "Family Should not have Numbers or Special Characters")]
        [InlineData("ali", "red#wer", "11/12/1370", "0317144073", "user123", "javidleo.ef@gmail.com", "javid123@$", "Family Should not have Numbers or Special Characters")]
        public void AdminValidation_ValidatingFamily_ThrowExcpectedMessage(string name, string family, string dateofbirth, string nationalcode, string username, string email, string password, string errorMessage)
        {
            var admin = Admin.Create(name, family, dateofbirth, nationalcode, username, email, password);
            var result = validation.TestValidate(admin);
            result.ShouldHaveValidationErrorFor(admin => admin.Family).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Admin", "validation")]
        [InlineData("ali", "rezie", "", "0317144073", "user123", "javidleo.ef@gmail.com", "javid123@$", "DateofBirth Should not be Empty")]
        [InlineData("ali", "rezie", "11/14/1366", "0317144073", "user123", "javidleo.ef@gmail.com", "javid123@$", "Invalid DateofBirth")]
        [InlineData("ali", "rezie", "11/11/13555", "0317144073", "user123", "javidleo.ef@gmail.com", "javid123@$", "Invalid DateofBirth")]
        [InlineData("ali", "rezie", "45/12/1355", "0317144073", "user123", "javidleo.ef@gmail.com", "javid123@$", "Invalid DateofBirth")]
        public void AdminValidation_ValidatingDateofBirth_ThrowExcpectedMessage(string name, string family, string dateofbirth, string nationalcode, string username, string email, string password, string errorMessage)
        {
            var admin = Admin.Create(name, family, dateofbirth, nationalcode, username, email, password);
            var result = validation.TestValidate(admin);
            result.ShouldHaveValidationErrorFor(admin => admin.DateofBirth).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Admin", "validation")]
        [InlineData("ali", "rezie", "11/12/1399", "", "user123", "javidleo.ef@gmail.com", "javid123@$", "NationalCode Should not be Empty")]
        [InlineData("ali", "rezie", "11/12/1399", "1111111111", "user123", "javidleo.ef@gmail.com", "javid123@$", "Invalid NationalCode")]
        [InlineData("ali", "rezie", "11/12/1399", "1239128301212312", "user123", "javidleo.ef@gmail.com", "javid123@$", "Invalid NationalCode")]
        [InlineData("ali", "rezie", "11/12/1399", "1231231354", "user123", "javidleo.ef@gmail.com", "javid123@$", "Invalid NationalCode")]
        public void AdminValidation_ValidatingNationalCode_ThrowExceptedMessage(string name, string family, string dateofbirth, string nationalcode, string username, string email, string password, string errorMessage)
        {
            var admin = Admin.Create(name, family, dateofbirth, nationalcode, username, email, password);
            var result = validation.TestValidate(admin);
            result.ShouldHaveValidationErrorFor(admin => admin.NationalCode).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Admin", "validation")]
        [InlineData("ali", "rezie", "11/12/1399", "0990076016", "", "javidleo.ef@gmail.com", "javid123@$", "UserName Should not be Empty")]
        [InlineData("ali", "rezie", "11/12/1399", "0990076016", "ali%$@", "javidleo.ef@gmail.com", "javid123@$", "Invalid UserName")]
        [InlineData("ali", "rezie", "11/12/1399", "0990076016", "AliBarati", "javidleo.ef@gmail.com", "javid123@$", "Invalid UserName")]
        public void AdminVlidation_ValidateingUserName_ThrowExcpectedMessage(string name, string family, string dateofbirth, string nationalcode, string username, string email, string password, string errorMessage)
        {
            var admin = Admin.Create(name, family, dateofbirth, nationalcode, username, email, password);
            var result = validation.TestValidate(admin);
            result.ShouldHaveValidationErrorFor(admin => admin.UserName).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Admin", "validation")]
        [InlineData("ali", "rezie", "11/12/1399", "0317144073", "javid", "Javidlerw$432@", "javid123@$")]
        [InlineData("ali", "rezie", "11/12/1399", "0317144073", "javid", "", "javid123@$")]
        public void AdminValidation_ValidatingEmail_ThrowExcpectedMessage(string name, string family, string dateofbirth, string nationalcode, string username, string email, string password)
        {
            var admin = Admin.Create(name, family, dateofbirth, nationalcode, username, email, password);
            var result = validation.TestValidate(admin);
            result.ShouldHaveValidationErrorFor(admin => admin.Email);
        }

        [Theory, Trait("Admin", "validation")]
        [InlineData("ali", "rezie", "11/12/1399", "0317144073", "javid", "javidleo.ef@gmail.com", "", "Password Should not be Null")]
        [InlineData("ali", "rezie", "11/12/1399", "0317144073", "javid", "javidleo.ef@gmail.com", "1231234324234", "week Password")]
        [InlineData("ali", "rezie", "11/12/1399", "0317144073", "javid", "javidleo.ef@gmail.com", "g#13G", "week Password")]
        public void AdminValidation_ValidatingPassword_ThrowExcpectedMessage(string name, string family, string dateofbirth, string nationalcode, string username, string email, string password, string errorMessage)
        {
            var admin = Admin.Create(name, family, dateofbirth, nationalcode, username, email, password);
            var result = validation.TestValidate(admin);
            result.ShouldHaveValidationErrorFor(admin => admin.Password).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Admin", "validation")]
        [AdminTestInvalidData]
        public void CreateAdmin_CheckForCreatingWithInvalidValues_ThrowNotAcceptableException(string name, string family, string dateofbirth, string nationalcode, string username, string email, string password)
        {
            void result() => service.Create(name, family ,dateofbirth, nationalcode,username, email,password);
            Assert.Throws<NotAcceptableException>(result);
        }

        [Fact, Trait("Admin", "validation")]
        public void CreateAdmin_CheckForCreatingSuccessfully_ReturnSuccessStatusTask()
        {
            
            var result = service.Create("ali", "rezie", "11/12/1399", "0317144073", "javid", "javidleo.ef@gmail.com", "123@#javid");
            Admin admin = Admin.Create("ali", "rezie", "11/12/1399", "0317144073", "javid", "javidleo.ef@gmail.com", "123@#javid");
            adminRepositoryMock.Verify(i => i.Add(It.Is<Admin>(i => i.Name == "ali")),Times.Once());
        }
    }
}
