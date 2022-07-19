using BookDataAccess;
using BookDataAccess.Repository;
using DomainModel;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using UseCases.RepositoryContract;
using Xunit;

namespace BookTest.Integration.Test;

public class OwnerTests : PersistTest<BookContext>
{
    private readonly IOwnerRepository _ownerRepository;
    private readonly BookContext _context;
    private readonly DbContextOptionsBuilder<BookContext> _optionBuilder;
    private Owner _owner;
    public OwnerTests()
    {
        _optionBuilder = new ContextOptionBuilderGenerator().Build();
        _context = new BookContext(_optionBuilder.Options);
        _ownerRepository = new OwnerRepository(_context);
        _owner = Owner.Create("javid", "hasani", "1234567890", "09177034453", "user", "password");
    }
    [Fact]
    public void CreateOwner_CheckForWorkingWell()
    {
        _ownerRepository.Add(_owner);

        var excpected = _ownerRepository.Find(_owner.Name);
        excpected.Should().BeEquivalentTo(_owner);
    }

    [Fact]
    public void CreateOwner_CheckForNullData_ThrowException()
    {
        void result() => _ownerRepository.Add(null);
        Assert.Throws<NullReferenceException>(result);
    }

    [Fact]
    public void GetAllOwners_CheckForWorkingWell()
    {
        var owner = Owner.Create("javid", "hasani", "1122133", "09177034678", "user", "pass");
        var owners = new List<Owner>()
        {
            owner,
            Owner.Create("javid","mohamadi","1122133","09185764832","ahmad","pass123"),
            Owner.Create("javid","rezaie","1122133","21325838744","javid","passw435"),
        };
        _context.Owner.AddRange(owners);
        _context.SaveChanges();

        var excpected = _ownerRepository.FindAll();

        excpected.Should().NotBeNull();
        excpected.Count.Should().Be(3);
        excpected[0].Should().BeEquivalentTo(owner);
    }

    [Fact]
    public void GetAllOwners_CheckForEmptyList()
    {
        var excpected = _ownerRepository.FindAll();
        excpected.Count.Should().Be(0);
    }

    [Fact]
    public void FindById_CheckForWorkingWell()
    {
        _ownerRepository.Add(_owner);
        var owner = _ownerRepository.Find(_owner.Name);
        var excpected = _ownerRepository.Find(owner.Id);

        excpected.Should().BeEquivalentTo(_owner);
    }

    [Fact]
    public void FindById_CheckForLessThanZero_ReturnNull()
    {
        var excpected = _ownerRepository.Find(-2);
        excpected.Should().BeNull();
    }

    [Fact]
    public void FindByName_CheckForWorkingWell()
    {
        _ownerRepository.Add(_owner);
        var excpected = _ownerRepository.Find(_owner.Name);

        excpected.Should().BeEquivalentTo(_owner);
    }

    [Fact]
    public void FindByName_CheckForNullData_ReturnNull()
    {
        _ownerRepository.Add(_owner);
        var excpected = _ownerRepository.Find(null);
        excpected.Should().Be(null);
    }

    [Theory]
    [InlineData("1234567890", true)]
    [InlineData("", false)]
    [InlineData(null, false)]
    [InlineData("dflhf20i3h", false)]
    public void DoesNationalCodeExist_CheckForWorkingWell(string nationalCode, bool excpectation)
    {
        _ownerRepository.Add(_owner);
        var excpected = _ownerRepository.DoesNationalCodeExist(nationalCode);
        excpected.Should().Be(excpectation);
    }

    [Fact]
    public void UpdateUser_CheckForWorkingWell()
    {
        var newOwner = Owner.Create("ali", "rezaie", "10432535", "0917538484", "user", "pass");

        _ownerRepository.Add(_owner);
        var owner = _ownerRepository.Find(_owner.Name);
        owner.Modify(newOwner.Name, newOwner.Family, newOwner.PhoneNumber, newOwner.UserName, newOwner.Password);

        _ownerRepository.Update(owner);
        var excpected = _ownerRepository.Find(owner.Name);
        excpected.Should().Be(owner);
    }

    [Fact]
    public void UpdateUser_CheckForNullData_ThrowException()
    {
        void result() => _ownerRepository.Update(null);
        Assert.Throws<ArgumentNullException>(result);
    }

    [Fact]
    public void DeleteUser_CheckForWorkingWell()
    {
        _ownerRepository.Add(_owner);
        var owner = _ownerRepository.Find(_owner.Name);

        _ownerRepository.Delete(owner);
        var excpected = _ownerRepository.Find(owner.Id);
        excpected.Should().Be(null);
    }

    [Fact]
    public void DeleteUser_CheckForNullData_ThrowException()
    {
        void result() => _ownerRepository.Delete(null);
        Assert.Throws<ArgumentNullException>(result);
    }
}
