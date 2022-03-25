using BookTest.Unit.Data.BookTestData;
using BookTest.Unit.TestDoubles;
using DomainModel;
using DomainModel.Validation;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;
using System.Collections.Generic;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using Xunit;
using NSubstitute;

namespace BookTest.Unit;

public class BookTests
{
    private readonly BookService _service;
    private readonly BookFakeRepository _bookRepository;
    private readonly BookValidation _validation;
    private Mock<IBookRepository> _bookRepositoryMock;
    private MockRepository mockRepository;
    public BookTests()
    {
        _bookRepository = new BookFakeRepository();
        _service = new BookService(_bookRepository);
        _validation = new BookValidation();
    }

    private void Setup()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
    }

    [Theory, Trait("Book", "validation")]
    [InlineData("", "Name Should not be Empty")]
    [InlineData("book132@12", "Name Should not have Special Characters")]
    public void BookValidation_ValidatingName_ThrowExcepectedMessage(string name, string errorMessage)
    {
        var book = new BookBuilder().WithName(name).Build();
        var result = _validation.TestValidate(book);
        result.ShouldHaveValidationErrorFor(book => book.Name).WithErrorMessage(errorMessage);
    }

    [Theory, Trait("Book", "validation")]
    [InlineData("", "AuthorName Should not be Empty")]
    [InlineData("authorName13", "AuthorName Should not have Special Characters")]
    public void BookValidation_ValidatingAuthorName_ThrowExcpectedMessage(string authorName, string errorMessage)
    {
        var book = new BookBuilder().WithAuthorName(authorName).Build();
        var result = _validation.TestValidate(book);
        result.ShouldHaveValidationErrorFor(book => book.authorName).WithErrorMessage(errorMessage);
    }

    [Theory, Trait("Book", "validation")]
    [InlineData("", "DateofAdding Should not be Empty")]
    [InlineData("1355/43/13", "Invalid DateofAdding")]
    [InlineData("1355/00/13", "Invalid DateofAdding")]
    [InlineData("99999/99/99", "Invalid DateofAdding")]
    public void BookValidation_ValidatingDateofAdding_ThrowExcpectedMessage(string dateofAdding, string errorMessage)
    {
        var book = new BookBuilder().WithAddingDate(dateofAdding).Build();
        var result = _validation.TestValidate(book);
        result.ShouldHaveValidationErrorFor(book => book.DateofAdding).WithErrorMessage(errorMessage);
    }

    [Fact, Trait("Book","create")]
    public void CreateBook_CheckForSendingDuplicateName_ThrowDuplciateException()
    {
        var book = new BookBuilder().Build();
        _bookRepository.SetExistingName(book.Name);

        void result() => _service.Create(book.Name, book.authorName, book.DateofAdding);
        Assert.Throws<DuplicateException>(result);
    }

    [Theory, Trait("Book", "create")]
    [InlineData("ali", "reza", "13/13/1333")]
    [InlineData("ali#", "reza", "13/12/1355")]
    [InlineData("", "reza", "13/12/1355")]
    [InlineData("ali", "reza123", "13/10/1333")]
    [InlineData("ali", "reza$", "13/10/13335")]
    [InlineData("ali", "", "13/11/1334")]
    public void CreateBook_CheckForInvalidData_ThrowNotAcceptableExcpeiton(string name, string authorName, string dateofAdding)
    {
        void result() => _service.Create(name, authorName, dateofAdding);
        Assert.Throws<NotAcceptableException>(result);
    }

    [Fact, Trait("Book", "create")]
    public void CreateBook_ChechForCreatingSuccessfully_ReturnSuccessTaskStatus()
    { 
        Setup(); // we call setup to create an instance of MockRepository because we dont need it always
        var book = new BookBuilder().Build();

        //var repository = Substitute.For<IBookRepository>();


        var service = new BookService(_bookRepositoryMock.Object);

        var result = service.Create(book.Name, book.authorName, book.DateofAdding);
        Assert.Equal(2, _bookRepositoryMock.Invocations.Count);
    }


    [Fact, Trait("Book", "update")]
    public void UpdateBook_UpdateWithInvalidBookId_ThrowNotFoundException()
    {
        void result() => _service.Update(Id: 2, name: "ali", authorName: "alireza", dateofAdding: "11/12/1399");
        Assert.Throws<NotFoundException>(result);
    }

    [Fact, Trait("Book", "update")]
    public void UpdateBook_UpdateWithDuplicateName_ThrowNotAcceptableException()
    {
        var book = new BookBuilder().Build();
        _bookRepository.SetExistingName(book.Name);

        void result() => _service.Update(book.Id, book.Name, book.authorName, book.DateofAdding);
        Assert.Throws<DuplicateException>(result);
    }

    [Fact, Trait("Book", "update")]
    public void UpdateBook_CheckForWorkingWell_ReturnSuccessTaskStatus()
    {
        var book = new BookBuilder().Build();
        var MockRepo = new Mock<IBookRepository>();
        var service = new BookService(MockRepo.Object);
        MockRepo.Setup(i => i.Find(1)).Returns(book);

        var result = service.Update(1, book.Name, book.authorName, book.DateofAdding);

        Assert.Equal(3, MockRepo.Invocations.Count);
        MockRepo.Verify(i => i.Update(book), Times.Once());
        result.Status.ToString().Should().Be("RanToCompletion");
    }

    [Fact, Trait("Book", "delete")]
    public void DeleteBook_CheckForInvalidId_ThrowNotFoundException()
    {
        void result() => _service.Delete(2);
        var exception = Assert.Throws<NotFoundException>(result);
    }

    [Fact, Trait("Book", "delete")]
    public void DeleteBook_CheckForDelete_WithTrueInUse_ThrowNotAcceptableException()
    {
        //_bookRepositoryMock.Setup(i => i.Find(1)).Returns(new BookBuilder().IsUnavailable().Build());
        _bookRepository.MakeItUnAvailable();
        _bookRepository.SetExistingId(1);
        void result() => _service.Delete(1);
        var exception = Assert.Throws<NotAcceptableException>(result);
    }

    [Fact, Trait("Book", "delete")]
    public void DeleteBook_CheckForWorkingWell_ReturnSuccessTaskStatus()
    {
        _bookRepository.SetExistingId(1);

        var result = _service.Delete(1);
        result.Status.ToString().Should().Be("RanToCompletion");
    }

    [Fact, Trait("Book", "getall")]
    public void GetAllBooks_CheckForWorkingWell_ReturnsExcpectedList()
    {
        List<Book> books = new List<Book>()
        {
            new BookBuilder().Build(),
            new BookBuilder().Build(),
            new BookBuilder().Build()
        };

        var result = _service.GetAll();
        result.Result.Should().BeEquivalentTo(books);
    }

    [Fact, Trait("Book", "getbyname")]
    public void GetByName_CheckWithInvalidName_ThrowNotFoundException()
    {
        Book book = new BookBuilder().WithName("newBook").Build();
        void result() => _service.GetByName(book.Name);
        Assert.Throws<NotFoundException>(result);
    }

    [Fact, Trait("Book", "getbyname")]
    public void GetByName_CheckForWorkingWell_ReturnExcpectedBook()
    {
        Book book = new BookBuilder().WithName("raz").Build();
        _bookRepository.SetExistingName(book.Name);

        var result = _service.GetByName(book.Name);
        result.Result.Should().BeEquivalentTo(book);
    }

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
        _bookRepository.SetExistingId(1);

        var result = _service.GetById(1);
        result.Result.Should().BeEquivalentTo(book);
    }
}
