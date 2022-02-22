using DomainModel;
using FluentAssertions;
using FluentValidation.TestHelper;
using UseCases.Exceptions;
using UseCases.Services;
using Xunit;

namespace BookTest.Unit
{
    public class BookTestes
    {
        private readonly BookService service;
        private readonly BookValidation validation;
        public BookTestes()
        {
            service = new BookService();
            validation = new BookValidation();
        }

        [Fact, Trait("Book", "validation")]
        public void BookValidation_ValidatingNullBookName_ShouldHaveError()
        {
            var book = Book.Create(1, "", "authorName", "11/12/1399");
            var result = validation.TestValidate(book);
            result.ShouldHaveValidationErrorFor(book => book.Name);
        }

        [Fact, Trait("Book", "validation")]
        public void BookValidation_VaidatingValidName_ShouldNotHaveError()
        {
            var book = Book.Create(1, "book", "authorName", "11/12/1399");
            var result = validation.TestValidate(book);
            result.ShouldNotHaveValidationErrorFor(book => book.Name);
        }

        [Fact, Trait("Book", "validation")]
        public void BookValidation_ValidatingNullAuthorName_ShouldHaveError()
        {
            var book = Book.Create(1, "bookName", "", "11/12/1399");
            var result = validation.TestValidate(book);
            result.ShouldHaveValidationErrorFor(book => book.authorName);
        }

        [Fact, Trait("Book", "validation")]
        public void BookValidation_ValidatingInvalidAuthorName_ShouldHaveError()
        {
            var book = Book.Create(1, "bookName", "name123", "11/12/1399");
            var result = validation.TestValidate(book);
            result.ShouldHaveValidationErrorFor(book => book.authorName);
        }

        [Fact, Trait("Book", "validation")]
        public void BookValidtion_ValidtingValidAuthorName_ShouldNotHaveError()
        {
            var book = Book.Create(1, "bookName", "authorName", "11/12/1399");
            var result = validation.TestValidate(book);
            result.ShouldNotHaveValidationErrorFor(book => book.authorName);
        }

        [Fact, Trait("Book", "validation")]
        public void BookValidation_ValidatingNullAddingDate_ShouldHaveError()
        {
            var book = Book.Create(1, "bookName", "authorName", "");
            var result = validation.TestValidate(book);
            result.ShouldHaveValidationErrorFor(book => book.DateofAdding);
        }

        [Fact , Trait("Book", "validation")]
        public void BookValidation_ValidatingInvalidAddingDate_ShouldHaveError()
        {
            var book = Book.Create(1, "bookName", "authorName", "41/42/1300");
            var result = validation.TestValidate(book);
            result.ShouldHaveValidationErrorFor(book => book.DateofAdding);
        }

        [Fact, Trait("Book", "validation")]
        public void BookValidation_ValidatingValidAddingDate_ShouldNotHaveError()
        {
            var book = Book.Create(1, "bookName", "authorName", "11/12/1399");
            var result = validation.TestValidate(book);
            result.ShouldNotHaveValidationErrorFor(book => book.DateofAdding);
        }

        [Theory , Trait("Book", "create")]
        [InlineData(1,"book1", "ali","11/11/1311")]
        [InlineData(1,"book1", "reza","11/10/1378")]
        [InlineData(1,"bookforanyone", "ali","11/11/1398")]
        [InlineData(1,"book1", "reza","11/11/1345")]
        public void CreateBook_CheckForCreatingSuccessfully_ReturnRanToCompletionTaskStatus(int id , string name , string authorName , string dateofAdding)
        {
            var result =service.Create(id, name, authorName, dateofAdding);
            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Theory , Trait("Book", "create")]
        [InlineData(1, "book1", "ali", "111/1123/13115")]
        [InlineData(1, "", "ali", "13/004/1")]
        [InlineData(1, "book1", "", "44/3/1315")]
        public void CreateBook_CheckForCreateingWithInvalidValues_ThrowNotAcceptableException(int id , string name ,string authorName ,string dateofAdding)
        {
            void result() => service.Create(id, name, authorName, dateofAdding);
            Assert.Throws<NotAcceptableException>(result);
        }

    }
}
