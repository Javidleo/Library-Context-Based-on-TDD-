using BookTest.Unit.Data.InteractionTestData;
using BookTest.Unit.TestDoubles;
using DomainModel;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using UseCases.Exceptions;
using UseCases.Services;
using Xunit;

namespace BookTest.Unit;

public class InteractionTests
{
    private readonly InteractionService _service;
    private readonly InteractionFakeRepository _interactionRepository;
    private readonly BookFakeRepository _bookRepository;
    private readonly AdminFakeRepository _adminRepository;
    private readonly USerFakerepository _userRepository;
    private readonly MockRepository mockRepository;
    private Interaction _interaction;
    public InteractionTests()
    {
        _interactionRepository = new InteractionFakeRepository();
        _bookRepository = new BookFakeRepository();
        _adminRepository = new AdminFakeRepository();
        _userRepository = new USerFakerepository();
        _service = new InteractionService(_userRepository, _adminRepository, _bookRepository, _interactionRepository);
        _interaction = Interaction.Create(1, 1, 1);
    }

    [Fact, Trait("Interaction", "Borrow")]
    public void BorrowBook_SendInvalidBookId_ThrowNotFoundException()
    {
        void result() => _service.Borrow(userId: 1, bookId: 2, adminId: 3);

        var exception = Assert.Throws<NotFoundException>(result);
        Assert.Equal("Book Not Founded", exception.Message);
    }

    [Fact, Trait("Interaction", "Borrow")]
    public void BorrowBook_CheckForBorrowingBook_WithTrueInUse_ThrowNotAcceptableException()
    {
        _bookRepository.SetExistingId(2);
        _bookRepository.MakeItUnAvailable();
        void result() => _service.Borrow(1, 2, 3);
        var exception = Assert.Throws<NotAcceptableException>(result);
        Assert.Equal("Cannot Borrow UnAvailable Book", exception.Message);
    }


    [Fact, Trait("Interaction", "Borrow")]
    public void BorrowBook_SendInvalidUserId_ThrowNotFoundException()
    {
        _bookRepository.SetExistingId(2);
        void result() => _service.Borrow(userId: 1, bookId: 2, adminId: 3);
        var exception = Assert.Throws<NotFoundException>(result);

        Assert.Equal("User Not Founded", exception.Message);
    }



    [Fact, Trait("Integration", "Borrow")]
    public void BorrowBook_SendInvalidAdminId_ThrowNotFoundException()
    {
        _bookRepository.SetExistingId(2);
        _userRepository.SetExistingId(1);

        void result() => _service.Borrow(userId: 1, bookId: 2, adminId: 3);
        var exception = Assert.Throws<NotFoundException>(result);
        Assert.Equal("Admin Not Founded", exception.Message);
    }

    [Fact, Trait("Interaction", "Borrow")]
    public void BorrowBook_CheckForWorkingWell_ReturnSuccessTaskStatus()
    {
        _bookRepository.SetExistingId(2);
        _userRepository.SetExistingId(1);
        _adminRepository.SetExistingId(3);

        var result = _service.Borrow(userId: 1, bookId: 2, adminId: 3);

        result.Status.ToString().Should().Be("RanToCompletion");
    }


    [Fact, Trait("Interaction", "logicalDelete")]
    public void DeleteInteraction_SendInvalidInteractionId_ThrowNotFoundException()
    {
        void result() => _service.Delete(1);
        Assert.Throws<NotFoundException>(result);
    }

    [Fact, Trait("Interaction", "logicalDelete")]
    public void DeleteInteraction_CheckForWorkingWell_ReturnSuccessTaskStatus()
    {
        _interactionRepository.SetExistingInteractionId(1);
        _bookRepository.SetExistingId(1);
        var interaction = new InteractionBuilder().WithBookId(1).WithUserId(1).Build();

        var result = _service.Delete(Id: 1);
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
        var result = _service.GetAll();
        result.Result.Should().BeEquivalentTo(interactions);
    }

    [Fact]
    public void FindInteractionByUserId_CheckForWorkingWell_ReturnSpecificResult()
    {
        _interactionRepository.SetExistingUserId(1);
        var interactions = _service.FindByUserId(1);
        interactions.Result[0].Should().BeEquivalentTo(_interaction);
    }

    [Fact]
    public void FindInteractionByUserId_CheckForInvalidId_ReturnNull()
    {
        var interactions = _service.FindByUserId(1);
        interactions.Result.Should().BeNull();
    }

    [Fact]
    public void FindInteractionByBookId_CheckForWorkingWell_ReturnSpecificResult()
    {
        _interactionRepository.SetExistingBookId(1);
        var interaction = _service.FindByBookId(bookId: 1);
        interaction.Result.Should().BeEquivalentTo(_interaction);
    }

    [Fact]
    public void FindInteractionByBookId_CheckForInvalidId_ReturnNull()
    {
        var interaction = _service.FindByBookId(1);
        interaction.Result.Should().BeNull();
    }

    [Fact]
    public void FindInteractionById_CheckForWorkingWell_ReturnSpecificData()
    {
        _interactionRepository.SetExistingInteractionId(1);
        var interaction = _service.FindByInteractionId(id: 1);
        interaction.Result.Should().BeEquivalentTo(_interaction);
    }

    [Fact]
    public void FindInteractionById_CheckForInvalidId_ThrowNotFoundedException()
    {
        void result() => _service.FindByInteractionId(1);
        Assert.Throws<NotFoundException>(result);
    }

    [Fact]
    public void FindInteractionByDate_CheckForWorkingWell_ReturnSpecificData()
    {
        _interactionRepository.SetExistingDate(DateTime.Now.Date);
        var interaction = _service.FindByInteractionDate(DateTime.Now.Date);
        interaction.Result[0].Should().BeEquivalentTo(_interaction);
    }

    [Fact]
    public void FindInteractionByDate_CheckForInvalidDate_ReturnNull()
    {
        var interaction = _service.FindByInteractionDate(DateTime.Now);
        interaction.Result.Should().BeNull();
    }
}
