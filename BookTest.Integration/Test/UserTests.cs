using BookDataAccess;
using BookDataAccess.Repository;
using DomainModel;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UseCases.RepositoryContract;
using Xunit;

namespace BookTest.Integration.Test;

public class UserTests : PersistTest<BookContext>
{
    private readonly IUserRepository _repository;
    private readonly BookContext _context;
    private readonly DbContextOptionsBuilder<BookContext> optionBuilder;
    public UserTests()
    {
        optionBuilder = GenerateOpiton();
        _context = new BookContext(optionBuilder.Options);
        _repository = new UserRepository(_context);

    }
    public DbContextOptionsBuilder<BookContext> GenerateOpiton()
    {
        var optionBuilder = new DbContextOptionsBuilder<BookContext>();
        optionBuilder.UseSqlServer("Server=DESKTOP-MONHQ70;Database=bookdb;Trusted_Connection=True;");
        return optionBuilder;
    }

    [Fact, Trait("User", "Repository")]
    public void CreateUser_CheckForCreatingSuccessfully()
    {
        User user = User.Create("name", "family", 15, "123123123", "javidleo.ef@gmail.com", 1);
        _repository.Add(user);
        var excpected = _repository.Find(user.Name);

        user.Should().BeEquivalentTo(excpected);
    }

    [Fact, Trait("User", "Repository")]
    public void GetAllUsers_CheckForDoingWell()
    {
        User user = User.Create("name", "family", 15, "1231231", "email", 1);
        _repository.Add(user);
        var userList = _repository.GetAll();
        userList[0].Should().BeEquivalentTo(user);
    }

    [Fact, Trait("User", "Repository")]
    public void FindByName_CheckForWorkingWell()
    {
        User user = User.Create("name", "family", 15, "1231231", "email", 1);
        _repository.Add(user);

        var actual = _repository.Find(user.Name);

        actual.Should().BeEquivalentTo(user);
    }


}
