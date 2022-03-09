using BookTest.Unit.Data.BookTestData;
using DomainModel;
using DomainModel.Validation;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using Xunit;

namespace BookTest.Unit;

public class BookTests
{
    private readonly BookService service;
    private readonly BookValidation validation;
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly MockRepository mockRepository;
    public BookTests()
    {
        mockRepository = new MockRepository(MockBehavior.Loose);
        _bookRepositoryMock = mockRepository.Create<IBookRepository>();
        service = new BookService(_bookRepositoryMock.Object);
        validation = new BookValidation();
    }

    [Theory, Trait("Book", "validation")]
    [InlineData("", "Name Should not be Empty")]
    [InlineData("book132@12", "Name Should not have Special Characters")]
    public void BookValidation_ValidatingName_ThrowExcepectedMessage(string name, string errorMessage)
    {
        var book = new BookBuilder().WithName(name).Build();
        var result = validation.TestValidate(book);
        result.ShouldHaveValidationErrorFor(book => book.Name).WithErrorMessage(errorMessage);
    }

    [Theory, Trait("Book", "validation")]
    [InlineData("", "AuthorName Should not be Empty")]
    [InlineData("authorName13", "AuthorName Should not have Special Characters")]
    public void BookValidation_ValidatingAuthorName_ThrowExcpectedMessage(string authorName, string errorMessage)
    {
        var book = new BookBuilder().WithAuthorName(authorName).Build();
        var result = validation.TestValidate(book);
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
        var result = validation.TestValidate(book);
        result.ShouldHaveValidationErrorFor(book => book.DateofAdding).WithErrorMessage(errorMessage);
    }

    [Fact, Trait("Book", "create")]
    public void CreateBook_ChechForCreatingSuccessfully_ReturnSuccessTaskStatus()
    {
        var result = service.Create("azadi123", "javidleo", "11/11/1311");
        result.Status.ToString().Should().Be("RanToCompletion");
    }

    [Theory, Trait("Book", "create")]
    [InlineData("ali", "reza", "13/13/1333")]
    [InlineData("ali@", "reza", "13/12/1355")]
    [InlineData("", "reza", "13/12/1355")]
    [InlineData("ali", "reza123", "13/10/1333")]
    [InlineData("ali", "reza$", "13/10/13335")]
    [InlineData("ali", "", "13/11/1334")]
    public void CreateBook_CheckForInvalidData_ThrowNotAcceptableExcpeiton(string name, string authorName, string dateofAdding)
    {
        void result() => service.Create(name, authorName, dateofAdding);
        Assert.Throws<NotAcceptableException>(result);
    }

    [Fact, Trait("Book","update")]
    public void UpdateBook_UpdateWithInvalidBookId_ThrowNotFoundException()
    {
        void result() => service.Update(Id: 1, name: "ali", authorName: "alireza", dateofAdding: "11/12/1399");
        Assert.Throws<NotFoundException>(result);
    }
 
    [Fact, Trait("Book","update")]
    public void UpdateBook_UpdateWithDuplicateName_ThrowNotAcceptableException()
    {
        var book = Book.Create("azadi123", "javidleo", "11/11/1311");

        _bookRepositoryMock.Setup(i => i.DoesNameExist("ali")).Returns(true);
        _bookRepositoryMock.Setup(i => i.Find(1)).Returns(book);

        void result() => service.Update(Id: 1, name: "ali", authorName: "alireza", dateofAdding: "11/12/1399");
        Assert.Throws<DuplicateException>(result);
    }

    [Fact, Trait("Book","update")]
    public void UpdateBook_CheckForWorkingWell_ReturnSuccessTaskStatus()
    {
        var book = Book.Create("ali", "alireza", "11/12/1399");
        _bookRepositoryMock.Setup(i => i.Find(1)).Returns(book);

        var result = service.Update(Id: 1, name: "ali", authorName: "alireza", dateofAdding: "11/12/1399");
        result.Status.ToString().Should().Be("RanToCompletion");
    }
}
