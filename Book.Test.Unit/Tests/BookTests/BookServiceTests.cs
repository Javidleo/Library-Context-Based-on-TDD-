using BookTest.Unit.Data.BookTestData;
using DomainModel;
using DomainModel.Validation;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;
using System.Collections.Generic;
using System.Linq;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.Services;
using UseCases.ViewModel;
using Xunit;

namespace BookTest.Unit.Tests.BookTests;

public class BookServiceTests
{
    private readonly BookService _service;
    private Mock<IBookRepository> _bookRepository;
    public BookServiceTests()
    {
        _bookRepository = new Mock<IBookRepository>();
        _service = new BookService(_bookRepository.Object);
    }

    // ////////////////////////////////////////       Create        /////////////////////////////////////////////////////////////
    [Fact, Trait("Book", "create")]
    public void CreateBook_CheckForValidation_ThrowNotAcceptableException()
    {
        var book = new BookBuilder().WithName("").Build();

        void result() => _service.Create(book.Name, book.AuthorName, book.DateofAdding);
        Assert.Throws<NotAcceptableException>(result);
    }

    [Fact, Trait("Book", "create")]
    public void CreateBook_CheckForSendingDuplicateName_ThrowDuplciateException()
    {
        var book = new BookBuilder().Build();
        _bookRepository.Setup(i => i.DoesExist(i => i.Name == book.Name)).Returns(true);

        void result() => _service.Create(book.Name, book.AuthorName, book.DateofAdding);
        Assert.Throws<DuplicateException>(result);
    }

    [Fact, Trait("Book", "create")]
    public void CreateBook_ChechForCreatingSuccessfully_ReturnSuccessTaskStatus()
    {
        var book = new BookBuilder().Build();

        var result = _service.Create(book.Name, book.AuthorName, book.DateofAdding);
        _bookRepository.Verify(i => i.Add(It.IsAny<Book>()), Times.Once());
    }

    // //////////////////////////////////////////       Update       /////////////////////////////////////////////////////////

    [Fact, Trait("Book", "update")]
    public void UpdateBook_UpdateWithDuplicateName_ThrowNotAcceptableException()
    {
        var book = new BookBuilder().Build();
        _bookRepository.Setup(i=> i.DoesExist(i=> i.Name == book.Name)).Returns(true);  

        void result() => _service.Update(book.Id, book.Name, book.AuthorName, book.DateofAdding);
        Assert.Throws<DuplicateException>(result);
    }

    [Fact, Trait("Book", "update")]
    public void UpdateBook_UpdateWithInvalidBookId_ThrowNotFoundException()
    {
        void result() => _service.Update(Id: 2, name: "ali", authorName: "alireza", dateofAdding: "11/12/1399");
        Assert.Throws<NotFoundException>(result);
    }

    [Fact, Trait("Book", "update")]
    public void UpdateBook_CheckForWorkingWell_ReturnSuccessTaskStatus()
    {
        var book = new BookBuilder().Build();
        _bookRepository.Setup(i => i.Find(book.Id)).Returns(book);

        var result = _service.Update(1, book.Name, book.AuthorName, book.DateofAdding);

        _bookRepository.Verify(i => i.Update(It.IsAny<Book>()), Times.Once());
        _bookRepository.Invocations.Count.Should().Be(3);
    }

    // ///////////////////////////////////////       Delete       //////////////////////////////////////////////////////////////
    [Fact, Trait("Book", "delete")]
    public void DeleteBook_CheckForInvalidId_ThrowNotFoundException()
    {
        void result() => _service.Delete(2);
        var exception = Assert.Throws<NotFoundException>(result);
    }

    [Fact, Trait("Book", "delete")]
    public void DeleteBook_CheckForDelete_WithTrueInUse_ThrowNotAcceptableException()
    {
        var book = new BookBuilder().Build();
        _bookRepository.Setup(i => i.Find(book.Id)).Returns(book);
        book.UnAvailable();

        void result() => _service.Delete(book.Id);
        var exception = Assert.Throws<NotAcceptableException>(result);
    }

    [Fact, Trait("Book", "delete")]
    public void DeleteBook_CheckForWorkingWell_ReturnSuccessTaskStatus()
    {
        var book = new BookBuilder().Build();
        _bookRepository.Setup(i => i.Find(book.Id)).Returns(book);

        var result = _service.Delete(1);
        _bookRepository.Verify(i => i.Delete(book), Times.Once());
    }

    // /////////////////////////////////////////      GetAll        //////////////////////////////////////////////////////////

    [Fact, Trait("Book", "getall")]
    public void GetAllBooks_CheckForWorkingWell_ReturnsExcpectedList()
    {
        List<BookListViewModel> books = new List<BookListViewModel>()
        {
            new BookListViewModel(){ BookName = "new book", AuthorName = "ali", DateOfAdding = "11/12/1399", InUse = true},
            new BookListViewModel(){ BookName = "new book", AuthorName = "ali", DateOfAdding = "11/12/1399", InUse = true},
            new BookListViewModel(){ BookName = "new book", AuthorName = "ali", DateOfAdding = "11/12/1399", InUse = true},
        };

        _bookRepository.Setup(i => i.GetAll()).Returns(books);

        var result = _service.GetAll();
        result.Result.Should().BeEquivalentTo(books);
    }

    // ////////////////////////////////////////////      GetByName        ////////////////////////////////////////////////////
    [Fact, Trait("Book", "getbyname")]
    public void GetByName_CheckWithInvalidName_ThrowNotFoundException()
    {
        Book book = new BookBuilder().Build();
        void result() => _service.GetByName(book.Name);
        Assert.Throws<NotFoundException>(result);
    }

    [Fact, Trait("Book", "getbyname")]
    public void GetByName_CheckForWorkingWell_ReturnExcpectedBook()
    {
        Book book = new BookBuilder().Build();
        _bookRepository.Setup(i => i.Find(book.Name)).Returns(book);

        var result = _service.GetByName(book.Name);
        result.Result.Should().BeEquivalentTo(book);
    }

    // ///////////////////////////////////////////////     GetById       ///////////////////////////////////////////////////////
    [Fact, Trait("Book", "getbyid")]
    public void GetById_CheckForInvalidId_ThrowNotFoundException()
    {
        void result() => _service.GetById(2);
        Assert.Throws<NotFoundException>(result);
    }

    [Fact, Trait("Book", "getbyid")]
    public void GetById_CheckForWorkingWell_ReturnExcpectedBook()
    {
        Book book = new BookBuilder().Build();
        _bookRepository.Setup(i => i.Find(book.Id)).Returns(book);

        var result = _service.GetById(book.Id);
        result.Result.Should().BeEquivalentTo(book);
    }
}
