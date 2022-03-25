using DomainModel;

namespace BookTest.Unit.Data.BookTestData;

public class BookBuilder
{
    private string _name = "raz";
    private string _authorName = "ali";
    private string _addingDate = "11/12/1399";
    private bool _inUse = false;
    public BookBuilder WithName(string name)
    {
        _name = name;
        return this;
    }
    public BookBuilder WithAuthorName(string authorName)
    {
        _authorName = authorName;
        return this;
    }
    public BookBuilder WithAddingDate(string addingDate)
    {
        _addingDate = addingDate;
        return this;
    }
    public BookBuilder IsUnavailable()
    {
        _inUse = true;
        return this;
    }
    public Book Build()
    {
        Book book = Book.Create(_name, _authorName, _addingDate);
        if (_inUse is true)
            book.UnAvailable();
        else
            book.Available();
        return book;
    }
}
