using BookDataAccess.Repository;
using DomainModel;
using DomainModel.Validation;
using FluentAssertions;
using FluentValidation.TestHelper;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.Services;
using Xunit;

namespace BookTest.Unit
{
    public class BookTestes
    {
        private readonly BookService service;
        private readonly BookValidation validation;
        private readonly IBookRepository _repository;
        public BookTestes()
        {
            _repository = new BookRepository();
            service = new BookService(_repository);
            validation = new BookValidation();
        }

        [Theory, Trait("Book","validation")]
        [InlineData("","family","11/12/1399","Name Should not be Empty")]
        [InlineData("book132@12","family","11/12/1399","Name Should not have Special Characters")]
        public void BookValidation_ValidatingName_ThrowExcepectedMessage(string name , string authorName ,string dateofAdding,string errorMessage)
        {
            var book = Book.Create(name, authorName, dateofAdding);
            var result = validation.TestValidate(book);
            result.ShouldHaveValidationErrorFor(book => book.Name).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Book", "validation")]
        [InlineData("name","","11/12/1355","AuthorName Should not be Empty")]
        [InlineData("name","authorName13","11/12/1355","AuthorName Should not have Special Characters")]
        public void BookValidation_ValidatingAuthorName_ThrowExcpectedMessage(string name ,string authorName , string dateofAdding, string errorMessage)
        {
            var book = Book.Create(name,authorName, dateofAdding);
            var result =validation.TestValidate(book);
            result.ShouldHaveValidationErrorFor(book => book.authorName).WithErrorMessage(errorMessage);
        }

        [Theory , Trait("Book", "validation")]
        [InlineData("name", "authorName", "", "DateofAdding Should not be Empty")]
        [InlineData("name", "authorName", "1355/43/13", "Invalid DateofAdding")]
        [InlineData("name", "authorName", "1355/00/13", "Invalid DateofAdding")]
        [InlineData("name", "authorName", "99999/99/99", "Invalid DateofAdding")]
        public void BookValidation_ValidatingDateofAdding_ThrowExcpectedMessage(string name ,string authorName ,string dateofAdding, string errorMessage)
        {
            var book = Book.Create(name, authorName, dateofAdding);
            var result = validation.TestValidate(book);
            result.ShouldHaveValidationErrorFor(book => book.DateofAdding).WithErrorMessage(errorMessage);
        }

        [Fact , Trait("Book", "create")]
        public void CreateBook_ChechForCreatingSuccessfully_ReturnSuccessTaskStatus()
        {
            var result = service.Create("azadi123", "javidleo", "11/11/1311");
            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Theory , Trait("Book","create")]
        [InlineData("ali","reza","13/13/1333")]
        [InlineData("ali@","reza","13/12/1355")]
        [InlineData("","reza","13/12/1355")]
        [InlineData("ali","reza123","13/10/1333")]
        [InlineData("ali","reza$","13/10/13335")]
        [InlineData("ali","","13/11/1334")]
        public void CreateBook_CheckForInvalidData_ThrowNotAcceptableExcpeiton(string name ,string authorName,string dateofAdding)
        {
            void result ()=>  service.Create(name, authorName,dateofAdding);
            Assert.Throws<NotAcceptableException>(result);
        }
    }
}
