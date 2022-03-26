using BookTest.Unit.Data.AdminTestData;
using BookTest.Unit.Data.BookTestData;
using BookTest.Unit.Data.InteractionTestData;
using BookTest.Unit.Data.UserTestData;
using BookTest.Unit.TestDoubles;
using DomainModel;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using Xunit;

namespace BookTest.Unit;

public class InteractionTests
{
    private readonly InteractionService service;
    private readonly InteractionFakeRepository _interactionRepository;
    private readonly BookFakeRepository _bookRepository;
    private readonly AdminFakeRepository _adminRepository;
    private readonly USerFakerepository _userRepository;
    private readonly MockRepository mockRepository;
    public InteractionTests()
    {
        _interactionRepository = new InteractionFakeRepository();
        _bookRepository = new BookFakeRepository();
        _adminRepository = new AdminFakeRepository();
        _userRepository = new USerFakerepository();
        service = new InteractionService(_userRepository, _adminRepository, _bookRepository, _interactionRepository);
    }

    [Fact, Trait("Interaction", "Borrow")]
    public void BorrowBook_SendInvalidBookId_ThrowNotFoundException()
    {
        void result() => service.Borrow(userId: 1, bookId: 2, adminId: 3);

        var exception = Assert.Throws<NotFoundException>(result);
        Assert.Equal("Book Not Founded", exception.Message);
    }

    [Fact, Trait("Interaction", "Borrow")]
    public void BorrowBook_CheckForBorrowingBook_WithTrueInUse_ThrowNotAcceptableException()
    {
        _bookRepository.SetExistingId(2);
        _bookRepository.MakeItUnAvailable();
        void result() => service.Borrow(1, 2, 3);
        var exception = Assert.Throws<NotAcceptableException>(result);
        Assert.Equal("Cannot Borrow UnAvailable Book", exception.Message);
    }


    [Fact, Trait("Interaction", "Borrow")]
    public void BorrowBook_SendInvalidUserId_ThrowNotFoundException()
    {
        _bookRepository.SetExistingId(2);
        void result() => service.Borrow(userId: 1, bookId: 2, adminId: 3);
        var exception = Assert.Throws<NotFoundException>(result);

        Assert.Equal("User Not Founded", exception.Message);
    }

    

    [Fact, Trait("Integration", "Borrow")]
    public void BorrowBook_SendInvalidAdminId_ThrowNotFoundException()
    {
        _bookRepository.SetExistingId(2);
        _userRepository.SetExistingId(1);

        void result() => service.Borrow(userId: 1, bookId: 2, adminId: 3);
        var exception = Assert.Throws<NotFoundException>(result);
        Assert.Equal("Admin Not Founded", exception.Message);
    }

    [Fact, Trait("Interaction", "Borrow")]
    public void BorrowBook_CheckForWorkingWell_ReturnSuccessTaskStatus()
    {
        _bookRepository.SetExistingId(2);
        _userRepository.SetExistingId(1);
        _adminRepository.SetExistingId(3);

        var result = service.Borrow(userId: 1, bookId: 2, adminId: 3);

        result.Status.ToString().Should().Be("RanToCompletion");
    }

    
    [Fact, Trait("Interaction", "logicalDelete")]
    public void DeleteInteraction_SendInvalidInteractionId_ThrowNotFoundException()
    {
        void result() => service.Delete(1);
        Assert.Throws<NotFoundException>(result);
    }

    [Fact, Trait("Interaction","logicalDelete")]
    public void DeleteInteraction_CheckForWorkingWell_ReturnSuccessTaskStatus()
    {
        _interactionRepository.SetExistingInteractionId(1);
        _bookRepository.SetExistingId(1);
        var interaction = new InteractionBuilder().WithId(1).WithBookId(1).WithUserId(1).WithDate(DateTime.Now.Date).Build();
       
        var result = service.Delete(Id: 1);
        interaction.IsDeleted.Should().BeTrue();
        result.Status.ToString().Should().Be("RanToCompletion");
        
    }

    [Fact, Trait("Interaction", "GetAll")]
    public void GetAllInteractions_CheckForWorkingWell_ReturnSpecificResult()
    {
        var interactions = new List<Interaction>()
        {
            new InteractionBuilder().Build()
        };
        var result = service.GetAll();
        result.Should().BeEquivalentTo(interactions);
    }
}
