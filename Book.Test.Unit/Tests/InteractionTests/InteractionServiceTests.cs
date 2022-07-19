using BookTest.Unit.Data.BookTestData;
using BookTest.Unit.Data.InteractionTestData;
using DomainModel;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.Services;
using Xunit;

namespace BookTest.Unit.Tests.InteractionTests;

public class InteractionServiceTests
{
    private readonly InteractionService _service;
    private readonly Mock<IInteractionRepository> _interactionRepository;
    private readonly Mock<IBookRepository> _bookRepository;
    private readonly Mock<IAdminRepository> _adminRepository;
    private readonly Mock<IUserRepository> _userRepository;
    public InteractionServiceTests()
    {
        _interactionRepository = new Mock<IInteractionRepository>();
        _bookRepository = new Mock<IBookRepository>();
        _adminRepository = new Mock<IAdminRepository>();
        _userRepository = new Mock<IUserRepository>();
        _service = new InteractionService(_userRepository.Object, _adminRepository.Object, _bookRepository.Object,
                                            _interactionRepository.Object);
    }

    // //////////////////////////////////      Borrow      ///////////////////////////////////////////////////////
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
        var book = new BookBuilder().Build();
        _bookRepository.Setup(i => i.Find(book.Id)).Returns(book);
        book.UnAvailable();

        void result() => _service.Borrow(book.Id, 2, 3);
        var exception = Assert.Throws<NotAcceptableException>(result);
    }

    [Fact, Trait("Interaction", "Borrow")]
    public void BorrowBook_SendInvalidUserId_ThrowNotFoundException()
    {
        var book = new BookBuilder().Build();
        _bookRepository.Setup(i => i.Find(book.Id)).Returns(book);

        void result() => _service.Borrow(userId: 1, bookId: 2, adminId: 3);

        var exception = Assert.Throws<NotFoundException>(result);
        Assert.Equal("User Not Founded", exception.Message);
    }



    [Fact, Trait("Interaction", "Borrow")]
    public void BorrowBook_SendInvalidAdminId_ThrowNotFoundException()
    {
        var book = new BookBuilder().Build();
        _bookRepository.Setup(i => i.Find(book.Id)).Returns(book);
        _userRepository.Setup(i => i.DoesExist(i => i.Id == 1)).Returns(true);

        void result() => _service.Borrow(userId: 1, bookId: 2, adminId: 3);
        var exception = Assert.Throws<NotFoundException>(result);
        Assert.Equal("Admin Not Founded", exception.Message);
    }

    [Fact, Trait("Interaction", "Borrow")]
    public void BorrowBook_CheckForWorkingWell_ReturnSuccessTaskStatus()
    {
        var book = new BookBuilder().Build();
        _bookRepository.Setup(i => i.Find(book.Id)).Returns(book);
        _userRepository.Setup(i => i.DoesExist(i => i.Id == 1)).Returns(true);
        _adminRepository.Setup(i => i.DoesExist(i => i.Id == 3)).Returns(true);
        var result = _service.Borrow(userId: 1, bookId: 2, adminId: 3);

        _interactionRepository.Verify(i => i.Add(It.IsAny<Interaction>()), Times.Once());
        _bookRepository.Verify(i => i.Update(It.IsAny<Book>()), Times.Once());
    }
    // /////////////////////////////////////////////           Delete (Logical)            /////////////////////////////////////
    [Fact, Trait("Interaction", "logicalDelete")]
    public void DeleteInteraction_CheckForInvalidInteractionId_ThrowNotFoundException()
    {
        void result() => _service.Delete(1);
        Assert.Throws<NotFoundException>(result);
    }

    [Fact, Trait("Interaction", "logicalDelete")]
    public void DeleteInteraction_CheckForWorkingWell_ReturnSuccessTaskStatus()
    {
        var interaction = new InteractionBuilder().Build();
        _interactionRepository.Setup(i => i.Find(interaction.Id)).Returns(interaction);
        var book = new BookBuilder().Build();
        _bookRepository.Setup(i => i.Find(book.Id)).Returns(book);

        var result = _service.Delete(interaction.Id);

        _interactionRepository.Verify(i => i.Update(It.IsAny<Interaction>()), Times.Once());
        _bookRepository.Verify(i => i.Update(It.IsAny<Book>()), Times.Once());
    }

    // ////////////////////////////////////////          GetAll         ///////////////////////////////////////////////////////
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
    // ////////////////////////////////////     Find By User Id       //////////////////////////////////////////////////////
    //[Fact]
    //public void FindInteractionByUserId_CheckForInvalidId_ReturnNull()
    //{
    //    _interactionRepository.Setup(i => i.FindByUserId(1)).Returns(new List<Interaction>());// return empty list
    //    void result() => _service.FindByUserId(1);
    //    Assert.Throws<NotFoundException>(result);
    //}

    //[Fact]
    //public void FindInteractionByUserId_CheckForWorkingWell_ReturnSpecificResult()
    //{
    //    var interactionList = new List<Interaction>() {  new InteractionBuilder().Build() };
    //   _interactionRepository.Setup(i => i.FindByUserId(1)).Returns(interactionList);

    //    var result = _service.FindByUserId(1);
    //    result.Result.Should().BeEquivalentTo(interactionList);
    //}

    // ////////////////////////////////////////          Find By BookId        /////////////////////////////////////////////////////
    [Fact]
    public void FindInteractionByBookId_CheckForWorkingWell_ReturnSpecificResult()
    {
        void result() => _service.FindByBookId(bookId: 1);
        Assert.Throws<NotFoundException>(result);
    }

    [Fact]
    public void FindInteractionByBookId_CheckForInvalidId_ReturnNull()
    {
        var interaction = new InteractionBuilder().Build();
        _interactionRepository.Setup(i => i.FindByBookId(1)).Returns(interaction);
        var result = _service.FindByBookId(1);
        result.Result.Should().BeEquivalentTo(interaction);
    }
    // /////////////////////////////////// /////////        Find By InteractionId        ///////////////////////////////
    [Fact]
    public void FindInteractionById_CheckForInvalidId_ThrowNotFoundedException()
    {
        void result() => _service.FindByInteractionId(1);
        Assert.Throws<NotFoundException>(result);
    }

    [Fact]
    public void FindInteractionById_CheckForWorkingWell_ReturnSpecificData()
    {
        var interaction = new InteractionBuilder().Build();
        _interactionRepository.Setup(i => i.Find(interaction.Id)).Returns(interaction);

        var result = _service.FindByInteractionId(interaction.Id);
        result.Result.Should().BeEquivalentTo(interaction);
    }

    // ////////////////////////////////////        Find Interaction By Date       //////////////////////////////

}
