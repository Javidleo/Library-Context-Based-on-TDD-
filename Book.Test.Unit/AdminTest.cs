using BookTest.Unit.Data.Admin;
using DomainModel;
using DomainModel.Validation;
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
            adminRepositoryMock = mockRepository.Create<IAdminRepository>();
            service = new AdminService(adminRepositoryMock.Object);
            validation = new AdminValidation();
        }

        [Theory, Trait("Admin", "validation")]
        [InlineData("", "Name Should not be Empty")]
        [InlineData("ali123", "Name Should not have Numbers or Special Characters")]
        [InlineData("ali@", "Name Should not have Numbers or Special Characters")]
        public void AdminValidation_ValidatingName_ThrowExcpectedMessage(string name, string errorMessage)
        {
            var admin = new AdminBuilder().WithName(name).Build();
            var result = validation.TestValidate(admin);
            result.ShouldHaveValidationErrorFor(admin => admin.Name).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Admin", "validation")]
        [InlineData("", "Family Should not be Empty")]
        [InlineData("re1zied", "Family Should not have Numbers or Special Characters")]
        [InlineData("red#wer", "Family Should not have Numbers or Special Characters")]
        public void AdminValidation_ValidatingFamily_ThrowExcpectedMessage(string family, string errorMessage)
        {
            var admin = new AdminBuilder().WithFamily(family).Build();
            var result = validation.TestValidate(admin);
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
            var result = validation.TestValidate(admin);
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
            var result = validation.TestValidate(admin);
            result.ShouldHaveValidationErrorFor(admin => admin.NationalCode).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Admin", "validation")]
        [InlineData("", "UserName Should not be Empty")]
        [InlineData("ali%$@", "Invalid UserName")]
        [InlineData("AliBarati", "Invalid UserName")]
        public void AdminVlidation_ValidateingUserName_ThrowExcpectedMessage(string username, string errorMessage)
        {
            var admin = new AdminBuilder().WithUserName(username).Build();
            var result = validation.TestValidate(admin);
            result.ShouldHaveValidationErrorFor(admin => admin.UserName).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Admin", "validation")]
        [InlineData("Javidlerw$432@")]
        [InlineData("javid123@$")]
        public void AdminValidation_ValidatingEmail_ThrowExcpectedMessage(string email)
        {
            var admin = new AdminBuilder().WithEmail(email).Build();
            var result = validation.TestValidate(admin);
            result.ShouldHaveValidationErrorFor(admin => admin.Email);
        }

        [Theory, Trait("Admin", "validation")]
        [InlineData("", "Password Should not be Empty")]
        [InlineData("1231234324234", "week Password")]
        [InlineData("g#13G", "week Password")]
        public void AdminValidation_ValidatingPassword_ThrowExcpectedMessage(string password, string errorMessage)
        {
            var admin = new AdminBuilder().WithPassword(password).Build();
            var result = validation.TestValidate(admin);
            result.ShouldHaveValidationErrorFor(admin => admin.Password).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Admin", "create")]
        [AdminTestInvalidData]
        public void CreateAdmin_CheckForCreatingWithInvalidValues_ThrowNotAcceptableException(string name, string family, string dateofbirth, string nationalcode, string username, string email, string password)
        {
            void result() => service.Create(name, family, dateofbirth, nationalcode, username, email, password);
            Assert.Throws<NotAcceptableException>(result);
        }

        [Theory, Trait("Admin", "create")]
        [AdminValidTestData]
        public void CreateAdmin_CheckForCreatingSuccessfully_ReturnTrueVerification(string name, string family, string nationalCode, string dateofbirth, string username, string email, string password)
        {
            adminRepositoryMock.Setup(i => i.DoesExist(nationalCode)).Returns(false);
            var result = service.Create(name, family, dateofbirth, nationalCode, username, email, password);
            adminRepositoryMock.Verify(i => i.Add(It.Is<Admin>(i => i.Name == name)), Times.Once());
            //Assert.Equal("RanToCompletion", result.Status.ToString());
        }

        [Fact, Trait("Admin", "delete")]
        public void DeleteAdmin_CheckForDeletingSuccessfully_ReturnTrueVerification()
        {
            var admin = new AdminBuilder().Build();
            adminRepositoryMock.Setup(i => i.Find(1)).Returns(admin);
            var result = service.Delete(1);
            adminRepositoryMock.Verify(i => i.Delete(admin), Times.Once());
            Assert.Equal("RanToCompletion", result.Status.ToString());
        }

        [Fact, Trait("Admin", "delete")]
        public void DeleteAdmin_CheckForDeletingWhenUserNotExisting_ThrowNotFoundException()
        {
            var admin = new AdminBuilder().Build();
            adminRepositoryMock.Setup(i => i.Find(admin.Id)).Returns(admin);
            void result() => service.Delete(2);
            Assert.Throws<NotFoundException>(result);
        }


    }
}
