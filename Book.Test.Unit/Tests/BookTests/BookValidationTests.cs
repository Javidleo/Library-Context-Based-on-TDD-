using BookTest.Unit.Data.BookTestData;
using DomainModel.Validation;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookTest.Unit.Tests.BookTests
{
    public class BookValidationTests
    {
        private readonly BookValidation _validation;
        public BookValidationTests()
        => _validation = new BookValidation();

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
            result.ShouldHaveValidationErrorFor(book => book.AuthorName).WithErrorMessage(errorMessage);
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
    }
}
