using BookTest.Unit.Data.AdminTestData;
using BookTest.Unit.TestDoubles;
using DomainModel;
using DomainModel.Validation;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using Xunit;
using NSubstitute;
namespace BookTest.Unit;

public class AdminTest
{
    private AdminService _service;
    private readonly AdminValidation _validation;
    private readonly AdminFakeRepository _repository;
    private  IAdminRepository _repositoryFake;
    public AdminTest()
    {
        _repository = new AdminFakeRepository();
        _service = new AdminService(_repository);
        _validation = new AdminValidation();
    }

    private void Setup()
    {
        _repositoryFake = Substitute.For<IAdminRepository>();
        _service = new AdminService(_repositoryFake);
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

    [Fact, Trait("Admin", "create")]
    public void CreateAdmin_checkForDuplicateNationalCode_ThrowDuplicateException()
    {
        var admin = new AdminBuilder().Build();
        _repository.SetExistingNationalCode(admin.NationalCode);

        void result() => _service.Create(admin.Name, admin.Family, admin.DateofBirth, admin.NationalCode, admin.UserName, admin.Email, admin.Password);
        var exception = Assert.Throws<DuplicateException>(result);
        exception.Message.Should().Be("Duplicate NationalCode");
    }

    [Fact, Trait("Admin", "create")]
    public void CreateAdmin_CheckForDuplicateUserName_ThrowDuplicateException()
    {
        var admin = new AdminBuilder().Build();
        _repository.SetExistingUserName(admin.UserName);

        void result() => _service.Create(admin.Name, admin.Family, admin.DateofBirth, admin.NationalCode, admin.UserName, admin.Email, admin.Password);
        var exception = Assert.Throws<DuplicateException>(result);
        exception.Message.Should().Be("Duplicate Username");
    }

    [Fact, Trait("Admin", "create")]
    public void CreateAdmin_CheckForDuplicateEmail_ThrowDuplicateException()
    {
        var admin = new AdminBuilder().Build();
        _repository.SetExistingEmail(admin.Email);

        void result() => _service.Create(admin.Name, admin.Family, admin.DateofBirth, admin.NationalCode, admin.UserName, admin.Email, admin.Password);
        var exception = Assert.Throws<DuplicateException>(result);
        exception.Message.Should().Be("Duplicate Email");
    }

    [Theory, Trait("Admin", "create")]
    [AdminInvalidTestData]
    public void CreateAdmin_CheckForCreatingWithInvalidValues_ThrowNotAcceptableException(string name, string family, string dateofbirth, string nationalcode, string username, string email, string password)
    {
        void result() => _service.Create(name, family, dateofbirth, nationalcode, username, email, password);
        Assert.Throws<NotAcceptableException>(result);
    }

    [Fact, Trait("Admin", "create")]
    
    public void CreateAdmin_CheckForCreatingSuccessfully_ReturnSuccessTaskStatus()
    {
        Setup();
        var admin = new AdminBuilder().Build();

        var result = _service.Create(admin.Name,admin.Family,admin.DateofBirth,admin.NationalCode,admin.UserName,admin.Email,admin.Password);
        _repositoryFake.Received(1).Add(Arg.Is<Admin> (e =>  e.NationalCode == admin.NationalCode));
    }

    [Fact, Trait("Admin", "delete")]
    public void DeleteAdmin_CheckForDeletingWhenUserNotExisting_ThrowNotFoundException()
    {
        void result() => _service.Delete(1);
        Assert.Throws<NotFoundException>(result);
    }

    [Fact, Trait("Admin", "delete")]
    public void DeleteAdmin_CheckForDeletingSuccessfully_ReturnTrueVerification()
    {
        Setup();
        var admin = new AdminBuilder().Build();
        _repositoryFake.Find(1).Returns(admin);

        var result = _service.Delete(1);
        _repositoryFake.Received(1).Delete(admin);
        result.Status.ToString().Should().Be("RanToCompletion");
    }

    [Fact, Trait("Admin","update")]
    public void UpdateAdmin_CheckForInvalidId_ThrowNotFoundException()
    {
        void result() => _service.Update(1, "ali", "rezaie", "11/12/1388", "user", "javid@gmail.com", "javid123#!!");
        var exception = Assert.Throws<NotFoundException>(result);
        exception.Message.Should().Be("Not Founded");
    }

    [Fact, Trait("Admin","update")]
    public void UpdateAdmin_CheckForDuplicateEmail_ThrowDuplicateException()
    {
        _repository.SetExistingId(1);
        _repository.SetExistingEmail("javidleo.ef@gmail.com");

        void result() => _service.Update(1, "ali", "reziae", "11/12/1366", "user","javidleo.ef@gmail.com", "javidl123#21");

        var exception = Assert.Throws<DuplicateException>(result);
        exception.Message.Should().Be("Duplicate Email");
    }

    [Fact, Trait("Admin","update")]
    public void UpdateAdmin_CheckForDuplicateUserName_ThrowDuplcateException()
    {
        var admin = new AdminBuilder().Build();
        _repository.SetExistingId(1);
        _repository.SetExistingUserName(admin.UserName);

        void result() => _service.Update(1, admin.Name, admin.Family, admin.DateofBirth, admin.UserName, admin.Email, admin.Password);

        var exception = Assert.Throws<DuplicateException>(result);
        exception.Message.Should().Be("Duplicate Username");
    }

    [Fact, Trait("Admin","update")]
    public void UpdateAdmin_CheckForWorkingWell_ReturnSuccessTaskStatus()
    {
        Setup();
        var oldAdmin = new AdminBuilder().WithName("ali").WithFamily("hasasni").Build();
        var newAdmin = new AdminBuilder().Build();
        _repositoryFake.Find(1).Returns(newAdmin);

        var result = _service.Update(1, newAdmin.Name, newAdmin.Family, newAdmin.DateofBirth, newAdmin.UserName,newAdmin.Email, newAdmin.Password);
        result.Status.ToString().Should().Be("RanToCompletion");
    }



  


}
