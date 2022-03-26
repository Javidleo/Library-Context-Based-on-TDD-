using BookDataAccess;
using BookDataAccess.Repository;
using DomainModel;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        _optionBuilder = new ContextOptionBuilderGenerator().GenerateOptionBuilder();
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
        void result()=> _ownerRepository.Add(null);
        Assert.Throws<NullReferenceException>(result);
    }

    [Fact]
    public void GetAllOwners_CheckForWorkingWell()
    {

    }


}
