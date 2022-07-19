using BookDataAccess;
using BookDataAccess.Repository;
using DomainModel;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using UseCases.RepositoryContract;
using Xunit;

namespace BookTest.Integration.Test;

public class UserTests : PersistTest<BookContext>
{
    private readonly IUserRepository _userRepository;
    private IBookRepository _bookRepository;
    private IInteractionRepository _interactionRepository;
    private IAdminRepository _adminRepository;
    private readonly BookContext _context;
    private readonly DbContextOptionsBuilder<BookContext> _optionBuilder;
    private User _user;
    public UserTests()
    {
        _optionBuilder = new ContextOptionBuilderGenerator().Build();
        _context = new BookContext(_optionBuilder.Options);
        _userRepository = new UserRepository(_context);
        _user = User.Create("name", "family", 15, "123123123", "javidleo.ef@gmail.com", 1);
    }

    [Fact, Trait("User", "Repository")]
    public void CreateUser_CheckForWorkingWell()
    {
        _userRepository.Add(_user);
        var excpected = _userRepository.Find(_user.Name);
        _user.Should().BeEquivalentTo(excpected);
    }

    [Fact, Trait("User", "Repository")]
    public void CreateUser_CheckForNullData_ThrowExcption()
    {
        void result() => _userRepository.Add(null);
        Assert.Throws<NullReferenceException>(result);
    }

    [Fact, Trait("User", "Repository")]
    public void GetAllUsers_CheckForDoingWell()
    {
        _userRepository.Add(_user);
        var userList = _userRepository.FindAll();
        userList.Should().Contain(_user);
    }

    [Fact, Trait("User", "Repository")]
    public void FindByName_CheckForWorkingWell()
    {
        _userRepository.Add(_user);

        var actual = _userRepository.Find(_user.Name);
        actual.Should().BeEquivalentTo(_user);
    }

    [Fact, Trait("User", "Repository")]
    public void FindByName_CheckForInvalidData_ReturnNull()
    {
        var excpected = _userRepository.Find("name");
        excpected.Should().BeNull();
    }

    [Fact, Trait("User", "Repository")]
    public void FindByName_CheckForNullData_ReturnNull()
    {
        var excpected = _userRepository.Find(null);
        excpected.Should().BeNull();
    }

    [Fact, Trait("User", "Repository")]
    public void FindById_CheckForWorkingWell()
    {
        _userRepository.Add(_user);
        var user = _userRepository.Find(_user.Name);

        var excpected = _userRepository.Find(user.Id);
        excpected.Should().BeEquivalentTo(_user);
    }
    [Fact]
    public void FindById_CheckForInvalidData_ReturnNull()
    {
        var excpected = _userRepository.Find(0);
        excpected.Should().BeNull();
    }

    [Fact]
    public void FindById_CheckFOrNullData_ReturnNull()
    {
        var excpected = _userRepository.Find(null);
        excpected.Should().BeNull();
    }

    [Fact]
    public void FindWithBooks_CheckForWorkingWell()
    {
        _interactionRepository = new InteractionRepository(_context);
        _bookRepository = new BookRepository(_context);
        _adminRepository = new AdminRepository(_context);
        _userRepository.Add(_user);
        var user = _userRepository.Find(_user.Name);

        _bookRepository.Add(Book.Create("java", "ali", "11/12/1399"));
        var book = _bookRepository.Find("java");

        _adminRepository.Add(Admin.Create("ali", "hasani", "11/12/1399", "12412534", "javid", "javid", "jav124234"));
        var admin = _adminRepository.Find("ali");

        Interaction interaction = Interaction.Create(user.Id, book.Id, admin.Id);
        _interactionRepository.Add(interaction);

        var excpected = _userRepository.FindWithBooks(user.Id);
        excpected.Interactions.Should().Contain(interaction);
    }

    [Fact]
    public void FindWithBooks_CheckForUserWithDeletedInteractions()
    {
        _interactionRepository = new InteractionRepository(_context);
        _bookRepository = new BookRepository(_context);
        _adminRepository = new AdminRepository(_context);
        _userRepository.Add(_user);
        var user = _userRepository.Find(_user.Name);

        _bookRepository.Add(Book.Create("java", "ali", "11/12/1399"));
        var book = _bookRepository.Find("java");

        _adminRepository.Add(Admin.Create("ali", "hasani", "11/12/1399", "12412534", "javid", "javid", "jav124234"));
        var admin = _adminRepository.Find("ali");

        Interaction interaction = Interaction.Create(user.Id, book.Id, admin.Id);
        _interactionRepository.Add(interaction);
        var interactions = _interactionRepository.FindByUserId(user.Id);

        interactions[0].LogicalDelete();
        _interactionRepository.Update(interactions[0]);

        var excpected = _userRepository.FindWithBooks(user.Id);
        excpected.Interactions.Should().NotContain(interactions[0]);
    }

    [Fact]
    public void UpdateUser_CheckForWorkingWell()
    {
        _userRepository.Add(_user);
        var user = _userRepository.Find(_user.Name);

        user.Modify("javid", "hasani", 16, "javidleo.ef@gmail.com", 1);
        _userRepository.Update(user);

        var excpected = _userRepository.Find(user.Id);

        excpected.Name.Should().Be(user.Name);
        excpected.Family.Should().Be(user.Family);
        excpected.Email.Should().Be(user.Email);
    }

    [Fact]
    public void UpdateUser_CheckForNullData_ThrowExcpetion()
    {
        void result() => _userRepository.Update(null);

        Assert.Throws<ArgumentNullException>(result);
    }

    [Fact]
    public void DeleteUser_CheckForWorkingWell()
    {
        _userRepository.Add(_user);
        var user = _userRepository.Find(_user.Name);

        _userRepository.Delete(user);
        var excpected = _userRepository.Find(user.Id);
        excpected.Should().BeNull();
    }

    [Fact]
    public void DeleteUser_CheckForNullData_ThrowExcpection()
    {
        void result() => _userRepository.Delete(null);
        Assert.Throws<ArgumentNullException>(result);
    }

    [Theory]
    [InlineData("javidleo.ef@gmail.com", true)]
    [InlineData("SomeEmail", false)]
    [InlineData(null, false)]
    public void DoesEmailExist_CheckForWorkingWell(string email, bool excpectation)
    {
        _userRepository.Add(_user);

        var excpected = _userRepository.DoesEmailExist(email);
        excpected.Should().Be(excpectation);
    }

    [Theory]
    [InlineData("123123123", true)]
    [InlineData("natioanlCode", false)]
    [InlineData(null, false)]
    public void DoesNaitonalCodeExist_CheckForWrokingWell(string nationalCode, bool excpectation)
    {
        _userRepository.Add(_user);

        var excpected = _userRepository.DoesNationalCodeExist(nationalCode);
        excpected.Should().Be(excpectation);
    }


}
