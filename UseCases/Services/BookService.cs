using DomainModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UseCases.Exceptions;

namespace UseCases.Services
{
    public class BookService
    {
        public BookService()
        {
        }

        public Task Create(int Id, string BookName, string authorName, string AddingDate)
        {
            if (string.IsNullOrEmpty(BookName))
                throw new NotAcceptableException("book Name is Empty");

            if (string.IsNullOrEmpty(authorName))
                throw new NotAcceptableException("authorName is Empty");

            if (!Regex.IsMatch(authorName, "^[a-zA-Z]+$"))
                throw new NotAcceptableException("Invalid Author Name");


            if (string.IsNullOrWhiteSpace(AddingDate))
                throw new NotAcceptableException("Adding Date is Empty");

            Book book = Book.Create(Id, BookName, authorName, AddingDate);
            return Task.CompletedTask;
        }
    }
}