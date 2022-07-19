using BookTest.Unit.Data.OwnerTestData;
using DomainModel;
using DomainModel.Validation;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;
using NSubstitute;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.Services;
using Xunit;

namespace BookTest.Unit.Tests.OwnerTests;

public class OwnerServiceTests
{
    private OwnerService _service;
    private readonly Mock<IOwnerRepository> _ownerRepository;
    public OwnerServiceTests()
    {
        _ownerRepository = new Mock<IOwnerRepository>();
        _service = new OwnerService(_ownerRepository.Object); ;
    }

    [Theory, Trait("Owner", "create")]
    [OwnerInvalidTestData]
    public void CreateOwner_CheckForValidation_ThrowNotAcceptableException(string name, string family, string nationalcode,
                                                                            string phonenumber, string username, string password)
    {
        void result() => _service.Create(name, family, nationalcode, phonenumber, username, password);
        Assert.Throws<NotAcceptableException>(result);
    }

    [Fact, Trait("Owner", "create")]
    public void CreateOwner_CheckForDuplicateNationalCode_ThrowDuplciateException()
    {
        var owner = new OwnerBuilder().Build();
        _ownerRepository.Setup(i => i.DoesExist(i => i.NationalCode == owner.NationalCode)).Returns(true);

        void result() => _service.Create(owner.Name, owner.Family, owner.NationalCode, owner.PhoneNumber, 
                                            owner.UserName, owner.Password);

        var exception = Assert.Throws<DuplicateException>(result);
        exception.Message.Should().Be("Duplicate NationalCode");
    }
    [Fact, Trait("Owner", "create")]
    public void CreateOwner_CheckForDuplicatePhoneNumber_THrowsDuplicateException()
    {
        var owner = new OwnerBuilder().Build();
        _ownerRepository.Setup(i=> i.DoesExist(i=> i.PhoneNumber == owner.PhoneNumber)).Returns(true);

        void result() => _service.Create(owner.Name, owner.Family, owner.NationalCode, owner.PhoneNumber,
                                            owner.UserName, owner.Password);
        
        var exception = Assert.Throws<DuplicateException>(result);
        exception.Message.Should().Be("Duplicate Phone Number");
    }

    [Fact]
    public void CreateOwner_CheckForDuplicateUserName_ThrowsDuplicateException()
    {
        var owner = new OwnerBuilder().Build();
        _ownerRepository.Setup(i=> i.DoesExist(i=> i.UserName == owner.UserName)).Returns(true);

        void result() => _service.Create(owner.Name, owner.Family, owner.NationalCode, owner.PhoneNumber,
                                            owner.UserName, owner.Password);

        var exception = Assert.Throws<DuplicateException>(result);
        exception.Message.Should().Be("Duplicate User Name");
    }

    [Fact, Trait("Owner", "create")]
    public void CreateOwner_CheckForCreatingSuccessFully_ReturnSuccessStatusTask()
    {
        var owner = new OwnerBuilder().Build();

        var result = _service.Create(owner.Name, owner.Family, owner.NationalCode, owner.PhoneNumber, owner.UserName, owner.Password);

        //_ownerRepository.Verify(i=> )
        result.Status.ToString().Should().Be("RanToCompletion");
    }

  

}

