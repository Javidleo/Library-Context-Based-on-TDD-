using BookDataAccess;
using BookDataAccess.Repository;
using DomainModel;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using UseCases.RepositoryContract;
using Xunit;

namespace BookTest.Integration.Test;

public class AdminTests : PersistTest<BookContext>
{
    private readonly BookContext _context;
    private readonly IAdminRepository _repository;
    private readonly DbContextOptionsBuilder<BookContext> _optionBuilder;
    private Admin _admin;
    public AdminTests()
    {
        _optionBuilder = new ContextOptionBuilderGenerator().Build();
        _context = new BookContext(_optionBuilder.Options);
        _repository = new AdminRepository(_context);
        _admin = Admin.Create("ali", "rezaie", "11/12/1388", "12412544", "user", "javidleo.ef@gmial.com", "adf@34");
    }

    [Fact, Trait("Admin", "Repository")]
    public void CreateAdmin_CheckForCreatingSuccessfully()
    {
        _repository.Add(_admin);
        var excpected = _repository.Find(_admin.Name);
        _admin.Should().BeEquivalentTo(excpected);
    }

    //[Fact, Trait("Admin", "Repository")]
    //public void CreateAdmin_TryToCreateNull_ReturnSqlServerError()
    //{
    //    void result() => _repository.Add(null);
    //    Assert.Throws<NullReferenceException>(result);
    //}

    [Fact, Trait("Admin", "Repository")]
    public void GetAll_CheckForWorkingWell()
    {
        _repository.Add(_admin);
        var adminList = _repository.FindAll();
        adminList.Should().Contain(_admin);
    }

    [Theory, Trait("Admin", "Repository")]
    [InlineData("12412544", true)]
    [InlineData("natioanlCode", false)]
    public void DoesNationalCodeExist_CheckForWorkingWell_ReturnTrueAnswre(string nationalCode, bool excpected)
    {
        _repository.Add(_admin);
        bool doesExist = _repository.DoesNationalCodeExist(nationalCode);
        doesExist.Should().Be(excpected);
    }

    [Fact, Trait("Admin", "Repository")]
    public void GetByNationalCode_CheckForWorkingWell()
    {
        _repository.Add(_admin);
        var excpected = _repository.GetByNationalCode(_admin.NationalCode);
        _admin.Should().BeEquivalentTo(excpected);
    }
    [Fact, Trait("Admin", "Repository")]
    public void GetByNationalCode_CheckForWorkingWithInvalidData_ReturnNull()
    {
        _repository.Add(_admin);
        var excpected = _repository.GetByNationalCode("natinalCode");
        excpected.Should().BeNull();
    }

    [Theory, Trait("Admin", "Repository")]
    [InlineData("user", true)]
    [InlineData("user3242", false)]
    [InlineData(null, false)]
    public void DoesUserNameExist_CheckForWorkingWell_ReturnTrueAnswre(string userName, bool excpectation)
    {
        _repository.Add(_admin);
        var excpected = _repository.DoesUsernameExist(userName);
        excpected.Should().Be(excpectation);
    }

    [Theory, Trait("Admin", "Repository")]
    [InlineData("javidleo.ef@gmial.com", true)]
    public void DoesEmailExist_CheckForWorkingWell_ReturnTrue(string email, bool excpectation)
    {
        _repository.Add(_admin);
        var excpected = _repository.DoesEmailExist(email);
        excpected.Should().Be(excpectation);
    }

    [Fact, Trait("Admin", "Repository")]
    public void DeleteAdmin_CheckForWorkingWell()
    {
        _repository.Add(_admin);
        _repository.Delete(_admin);
        var excpected = _repository.GetByNationalCode(_admin.NationalCode);
        excpected.Should().BeNull();
    }

    //[Fact, Trait("Admin", "Repository")]
    //public void DeleteAdmin_CheckForNullData_ThrowNullReferenceException()
    //{
    //    _repository.Add(_admin);

    //    void result() => _repository.Delete(null);
    //    Assert.Throws<ArgumentNullException>(result);
    //}

    [Fact, Trait("Admin", "Repository")]
    public void FindAdminWithId_CheckForWorkingWell()
    {
        _repository.Add(_admin);
        var admin = _repository.Find(_admin.Name);

        var excpected = _repository.Find(admin.Id);

        excpected.Should().BeEquivalentTo(_admin);
    }

    [Fact, Trait("Admin", "Repository")]
    public void FindAdminWithId_CheckForInvalidData_ReturnNull()
    {
        var excpected = _repository.Find(0);
        excpected.Should().BeNull();
    }

    [Fact, Trait("Admin", "Repository")]
    public void FindAdminWithId_CheckForNullData_ThrowExcpection()
    {
        var excpected = _repository.Find(null);
        excpected.Should().BeNull();
    }

    [Fact, Trait("Admin", "Repository")]
    public void FindAdminWithName_CheckForWorkingWell()
    {
        _repository.Add(_admin);
        var excpected = _repository.Find(_admin.Name);
        excpected.Should().BeEquivalentTo(_admin);
    }

    [Fact, Trait("Admin", "Repository")]
    public void FindAdminWithName_CheckForInvalidData_ReturnNull()
    {
        var excpected = _repository.Find("name12321654");
        excpected.Should().BeNull();
    }

    [Fact, Trait("Admin", "Repository")]
    public void FindAdminWithName_CheckForNullData_ThrowException()
    {
        var excpected = _repository.Find(null);
        excpected.Should().BeNull();
    }

    [Fact, Trait("Admin", "Repository")]
    public void UpdateAdmin_CheckForWorkingWell()
    {
        _repository.Add(_admin);
        var newAdmin = Admin.Create("ali", "rezaie", "11/12/1399", "12312454", "user", "email", "pass");
        _admin.Modify(newAdmin.Name, newAdmin.Family, newAdmin.DateofBirth, newAdmin.UserName, newAdmin.Email, newAdmin.Password);

        _repository.Update(_admin);

        var excpected = _repository.Find(_admin.Name);
        excpected.Family.Should().Be(newAdmin.Family);
        excpected.DateofBirth.Should().Be(newAdmin.DateofBirth);
    }

    //[Fact, Trait("Admin", "Repository")]
    //public void UpdateAdmin_CheckForNullData_ThrowException()
    //{
    //    void result() => _repository.Update(null);
    //    Assert.Throws<ArgumentNullException>(result);
    //}

}
