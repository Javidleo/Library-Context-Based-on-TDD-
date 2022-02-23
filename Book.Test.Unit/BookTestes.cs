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

        [Fact, Trait("Book", "validation")]
        public void BookValidation_ValidatingNullBookName_ShouldHaveError()
        {
            var book = Book.Create("", "authorName", "11/12/1399");
            var result = validation.TestValidate(book);
            result.ShouldHaveValidationErrorFor(book => book.Name);
        }

        [Fact, Trait("Book", "validation")]
        public void BookValidation_VaidatingValidName_ShouldNotHaveError()
        {
            var book = Book.Create("book", "authorName", "11/12/1399");
            var result = validation.TestValidate(book);
            result.ShouldNotHaveValidationErrorFor(book => book.Name);
        }

        [Fact, Trait("Book", "validation")]
        public void BookValidation_ValidatingNullAuthorName_ShouldHaveError()
        {
            var book = Book.Create("bookName", "", "11/12/1399");
            var result = validation.TestValidate(book);
            result.ShouldHaveValidationErrorFor(book => book.authorName);
        }

        [Fact, Trait("Book", "validation")]
        public void BookValidation_ValidatingInvalidAuthorName_ShouldHaveError()
        {
            var book = Book.Create("bookName", "name123", "11/12/1399");
            var result = validation.TestValidate(book);
            result.ShouldHaveValidationErrorFor(book => book.authorName);
        }

        [Fact, Trait("Book", "validation")]
        public void BookValidtion_ValidtingValidAuthorName_ShouldNotHaveError()
        {
            var book = Book.Create("bookName", "authorName", "11/12/1399");
            var result = validation.TestValidate(book);
            result.ShouldNotHaveValidationErrorFor(book => book.authorName);
        }

        [Fact, Trait("Book", "validation")]
        public void BookValidation_ValidatingNullAddingDate_ShouldHaveError()
        {
            var book = Book.Create("bookName", "authorName", "");
            var result = validation.TestValidate(book);
            result.ShouldHaveValidationErrorFor(book => book.DateofAdding);
        }

        [Fact, Trait("Book", "validation")]
        public void BookValidation_ValidatingInvalidAddingDate_ShouldHaveError()
        {
            var book = Book.Create("bookName", "authorName", "41/42/1300");
            var result = validation.TestValidate(book);
            result.ShouldHaveValidationErrorFor(book => book.DateofAdding);
        }

        [Fact, Trait("Book", "validation")]
        public void BookValidation_ValidatingValidAddingDate_ShouldNotHaveError()
        {
            var book = Book.Create("bookName", "authorName", "11/12/1399");
            var result = validation.TestValidate(book);
            result.ShouldNotHaveValidationErrorFor(book => book.DateofAdding);
        }

        [Theory, Trait("Book", "create")]
        [InlineData("book1", "ali", "11/11/1311")]
        [InlineData("book1", "reza", "11/10/1378")]
        [InlineData("bookforanyone", "ali", "11/11/1398")]
        [InlineData("book1", "reza", "11/11/1345")]
        public void CreateBook_CheckForCreatingSuccessfully_ReturnRanToCompletionTaskStatus(string name, string authorName, string dateofAdding)
        {
            var result = service.Create(name, authorName, dateofAdding);
            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Theory, Trait("Book", "create")]
        [InlineData("book1", "ali", "111/1123/13115")]
        [InlineData("", "ali", "13/004/1")]
        [InlineData("book1", "", "44/3/1315")]
        public void CreateBook_CheckForCreateingWithInvalidValues_ThrowNotAcceptableException(string name, string authorName, string dateofAdding)
        {
            void result() => service.Create(name, authorName, dateofAdding);
            Assert.Throws<NotAcceptableException>(result);
        }

    }
}
