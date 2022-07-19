using BookTest.Unit.Data.UserTestData;
using DomainModel.Validation;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.Services;
using Xunit;

namespace BookTest.Unit;

public class UserTests
{
    private readonly UserService service;
    private readonly UserValidation validation;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IAdminRepository> _adminRepositoryMock;
    private readonly Mock<IInteractionRepository> _interactionRepository;
    private readonly MockRepository mockRepository;
    public UserTests()
    {
        mockRepository = new MockRepository(MockBehavior.Loose);
        _userRepositoryMock = mockRepository.Create<IUserRepository>();
        _adminRepositoryMock = mockRepository.Create<IAdminRepository>();
        _interactionRepository = mockRepository.Create<IInteractionRepository>();
        service = new UserService(_userRepositoryMock.Object, _adminRepositoryMock.Object);
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

    [Theory, Trait("User", "create")]
    [UserValidTestData]
    public void CreateUser_CheckforCreatingSuccessfully_ReturnSuccessTaskStatus(UserValidTestObject user)
    {
        var result = service.Create(user.name, user.family, user.age, user.nationalCode, user.email, user.adminId);
        result.Status.ToString().Should().Be("RanToCompletion");
    }
    [Fact, Trait("User", "create")]
    public void CreateUser_CheckForCreatingWithInvalidValues_ThrowNotAcceptableExcpetion()
    {
        void result() => service.Create("ali", "reza@#", 15, "0990076016", "javidleo.ef@gmial.com", 1);
        Assert.Throws<NotAcceptableException>(result);
    }

    [Fact, Trait("User", "delete")]
    public void DeleteUser_CheckForWorkingWell_VerifingSuccessfully()
    {
        var user = new UserBuilder().Build();
        _userRepositoryMock.Setup(i => i.FindWithBooks(1)).Returns(user);

        var result = service.Delete(1);
        _userRepositoryMock.Verify(i => i.Delete(user), Times.Once());
        result.Status.ToString().Should().Be("RanToCompletion");
    }

    [Fact, Trait("User", "delete")]
    public void DeleteUser_DeleteInvalidId_ThrowNotFoundException()
    {
        void result() => service.Delete(1);
        Assert.Throws<NotFoundException>(result);
    }

    [Fact, Trait("User", "update")]
    public void UpdateUser_CheckForWorkingWell_ReturnSuccessTaskStatus()
    {
        var user = new UserBuilder().Build();
        _userRepositoryMock.Setup(i => i.Find(1)).Returns(user);
        var result = service.Update(1, "reza", "mohamadi", 18, "rezaahmadi.ef@gmail.com", 1);

        _userRepositoryMock.Verify(i => i.Update(user), Times.Once());
        user.Name.Should().Be("reza");
        user.Family.Should().Be("mohamadi");
    }

    [Fact, Trait("User", "update")]
    public void UpdateUser_CheckForDuplicateEmail_ThrowNotAcceptableException()
    {
        _userRepositoryMock.Setup(i => i.DoesEmailExist("javidleo.ef@gmail.com")).Returns(true);
        void result() => service.Update(1, "ali", "rezaie", 18, "javidleo.ef@gmail.com", 1);

        Assert.Throws<DuplicateException>(result);
    }

    [Fact, Trait("User", "update")]
    public void UpdateUser_SendInvalidUserId_ThrowNotFoundException()
    {
        void result() => service.Update(1, "ali", "rezaie", 18, "javidleo.ef@gmail.com", 1);
        Assert.Throws<NotFoundException>(result);
    }

    [Fact, Trait("User", "update")]
    public void UpdateUser_SendInvalidInformations_ThrowNotAcceptableException()
    {
        void result() => service.Update(1, "ali", "re21", 18, "javidleo.ef@gmail.com", 1);
        Assert.Throws<NotAcceptableException>(result);
    }

    [Fact, Trait("User", "getbyid")]
    public void GetById_CheckWithInvalidId_ThrowNotFoundException()
    {
        var user = new UserBuilder().Build();

        void result() => service.GetById(1);
        Assert.Throws<NotFoundException>(result);
    }

    [Fact, Trait("User", "getbyid")]
    public void GetById_CheckForWorkingWell_ReturnExcpectedUser()
    {
        var user = new UserBuilder().Build();

        _userRepositoryMock.Setup(i => i.Find(1)).Returns(user);
        var result = service.GetById(1);

        result.Result.Should().Be(user);
    }

    [Fact, Trait("User", "getbyname")]
    public void GetByName_CheckWithInvalidName_ThrowNotFoundException()
    {
        var user = new UserBuilder().Build();

        void result() => service.GetByName(user.Name);
        Assert.Throws<NotFoundException>(result);
    }

    [Fact, Trait("User", "getbyname")]
    public void GetByName_CheckForWorkingWell_ReturnExcpectedUser()
    {
        var user = new UserBuilder().Build();

        _userRepositoryMock.Setup(i => i.Find(user.Name)).Returns(user);
        var result = service.GetByName(user.Name);
    }

}
