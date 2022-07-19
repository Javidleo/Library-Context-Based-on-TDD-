using BookTest.Unit.Data.AdminTestData;
using DomainModel;
using FluentAssertions;
using Moq;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.Services;
using Xunit;

namespace BookTest.Unit.Tests.AdminTests;

public class AdminServiceTests
{
    private AdminService _service;
    private Mock<IAdminRepository> _adminRepository;
    public AdminServiceTests()
    {
        _adminRepository = new Mock<IAdminRepository>();
        _service = new AdminService(_adminRepository.Object);
    }

    [Theory, Trait("Admin", "create")]
    [AdminInvalidTestData]
    public void CreateAdmin_CheckForCreatingWithInvalidValues_ThrowNotAcceptableException(string name, string family,
                            string dateofbirth, string nationalcode, string username, string email, string password)
    {
        void result() => _service.Create(name, family, dateofbirth, nationalcode, username, email, password);
        Assert.Throws<NotAcceptableException>(result);
    }

    [Fact, Trait("Admin", "create")]
    public void CreateAdmin_CheckForDuplicateNationalCode_ThrowDuplicateException()
    {
        var admin = new AdminBuilder().Build();
        _adminRepository.Setup(i => i.DoesExist(i => i.NationalCode == admin.NationalCode)).Returns(true);

        void result() => _service.Create(admin.Name, admin.Family, admin.DateofBirth, admin.NationalCode,
                                         admin.UserName, admin.Email, admin.Password);

        var exception = Assert.Throws<DuplicateException>(result);
        exception.Message.Should().Be("Duplicate NationalCode");
    }

    [Fact, Trait("Admin", "create")]
    public void CreateAdmin_CheckForDuplicateUserName_ThrowDuplicateException()
    {
        var admin = new AdminBuilder().Build();
        _adminRepository.Setup(i => i.DoesExist(i => i.UserName == admin.UserName)).Returns(true);

        void result() => _service.Create(admin.Name, admin.Family, admin.DateofBirth, admin.NationalCode,
                                        admin.UserName, admin.Email, admin.Password);

        var exception = Assert.Throws<DuplicateException>(result);
        exception.Message.Should().Be("Duplicate Username");
    }

    [Fact, Trait("Admin", "create")]
    public void CreateAdmin_CheckForDuplicateEmail_ThrowDuplicateException()
    {
        var admin = new AdminBuilder().Build();
        _adminRepository.Setup(i => i.DoesExist(i => i.Email == admin.Email)).Returns(true);

        void result() => _service.Create(admin.Name, admin.Family, admin.DateofBirth, admin.NationalCode,
                                            admin.UserName, admin.Email, admin.Password);

        var exception = Assert.Throws<DuplicateException>(result);
        exception.Message.Should().Be("Duplicate Email");
    }

    [Fact, Trait("Admin", "create")]

    public void CreateAdmin_CheckForCreatingSuccessfully_ReturnSuccessTaskStatus()
    {
        var admin = new AdminBuilder().Build();

        var result = _service.Create(admin.Name, admin.Family, admin.DateofBirth, admin.NationalCode,
                                    admin.UserName, admin.Email, admin.Password);

        _adminRepository.Verify(i => i.Add(It.IsAny<Admin>()), Times.Once());
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
        var admin = new AdminBuilder().Build();
        _adminRepository.Setup(i => i.Find(admin.Id));

        var result = _service.Delete(admin.Id);

        _adminRepository.Verify(i => i.Delete(It.IsAny<Admin>()), Times.Once());
    }

    [Fact, Trait("Admin", "update")]
    public void UpdateAdmin_CheckForInvalidId_ThrowNotFoundException()
    {
        var admin = new AdminBuilder().Build();

        void result() => _service.Update(admin.Id, admin.Name, admin.Family, admin.DateofBirth, admin.UserName,
                                        admin.Email, admin.Password);

        var exception = Assert.Throws<NotFoundException>(result);
        exception.Message.Should().Be("Not Founded");
    }

    [Fact, Trait("Admin", "update")]
    public void UpdateAdmin_CheckForDuplicateEmail_ThrowDuplicateException()
    {
        var admin = new AdminBuilder().Build();
        _adminRepository.Setup(i => i.Find(admin.Id)).Returns(admin);
        void result() => _service.Update(admin.Id, admin.Name, admin.Family, admin.DateofBirth, admin.UserName,
                                            admin.Email, admin.Password);

        var exception = Assert.Throws<DuplicateException>(result);
        exception.Message.Should().Be("Duplicate Email");
    }

    [Fact, Trait("Admin", "update")]
    public void UpdateAdmin_CheckForDuplicateUserName_ThrowDuplcateException()
    {
        var admin = new AdminBuilder().Build();
        _adminRepository.Setup(i => i.Find(admin.Id)).Returns(admin);
        void result() => _service.Update(admin.Id, admin.Name, admin.Family, admin.DateofBirth,
                                            admin.UserName, admin.Email, admin.Password);

        var exception = Assert.Throws<DuplicateException>(result);
        exception.Message.Should().Be("Duplicate Username");
    }

    [Fact, Trait("Admin", "update")]
    public void UpdateAdmin_CheckForValidtaion_ThrowsNotAcceptableException()
    {
        var admin = new AdminBuilder().WithName("").Build();
        _adminRepository.Setup(i => i.Find(admin.Id)).Returns(admin);
        void result() => _service.Update(admin.Id, admin.Name, admin.Family, admin.DateofBirth,
                                            admin.UserName, admin.Email, admin.Password);

        Assert.Throws<NotAcceptableException>(result);
    }

    [Fact, Trait("Admin", "update")]
    public void UpdateAdmin_CheckForWorkingWell_ReturnSuccessTaskStatus()
    {
        var oldAdmin = new AdminBuilder().WithName("javid").WithEmail("javidleo@gmail.com").WithUserName("newuser").Build();
        var admin = new AdminBuilder().Build();
        _adminRepository.Setup(i => i.Find(admin.Id)).Returns(oldAdmin);

        var result = _service.Update(admin.Id, admin.Name, admin.Family, admin.DateofBirth, admin.UserName,
                                        admin.Email, admin.Password);
        result.Status.ToString().Should().Be("RanToCompletion");
    }
}
