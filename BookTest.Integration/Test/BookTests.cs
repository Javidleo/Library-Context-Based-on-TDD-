using BookDataAccess;
using BookDataAccess.Repository;
using DomainModel;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using UseCases.RepositoryContract;
using Xunit;

namespace BookTest.Integration.Test;

public class BookTests : PersistTest<BookContext>
{
    private readonly IBookRepository _repository;
    private readonly DbContextOptionsBuilder<BookContext> _optionsBuilder;
    private readonly Book _book;
    private readonly BookContext _context;

    public BookTests()
    {
        _optionsBuilder = new ContextOptionBuilderGenerator().Build();
        _context = new BookContext(_optionsBuilder.Options);
        _repository = new BookRepository(_context);
        _book = Book.Create("GoodBook", "ali", "11/12/1344");
    }

    [Fact, Trait("Book", "Repository")]
    public void CreateBook_CheckForCreatingSuccessfully()
    {
        _repository.Add(_book);
        var actual = _repository.Find(_book.Name);
        actual.Should().Be(_book);
    }

    [Fact, Trait("Book", "Repository")]
    public void CreateBook_CheckForNullData_ThrowException()
    {
        void result() => _repository.Add(null);
        Assert.Throws<NullReferenceException>(result);
    }

    [Fact, Trait("Book", "Repository")]
    public void GetAllBooks_CheckForWorkingWell()
    {
        _repository.Add(_book);
        var bookList = _repository.FindAll();
        bookList.Should().HaveCount(1);
        bookList[0].Should().BeEquivalentTo(_book);
    }

    [Fact, Trait("Book", "Repository")]
    public void GetAllBooks_CheckWhenListIsEmpty()
    {
        var bookList = _repository.FindAll();
        bookList.Count.Should().Be(0);
    }

    [Fact, Trait("Book", "Repository")]
    public void FindBookWithId_CheckForWorkingWell()
    {
        _repository.Add(_book);
        var book = _repository.Find(_book.Name);

        var excpected = _repository.Find(book.Id);
        excpected.Should().BeEquivalentTo(_book);
    }

    [Fact, Trait("Book", "Repository")]
    public void FindBookWithId_CheckForInvalidData_ReturnNull()
    {
        var excpected = _repository.Find(1);
        excpected.Should().BeNull();
    }

    [Fact, Trait("Book", "Repository")]
    public void FindBookWithId_CheckForNullData_ReturnNull()
    {
        var excpected = _repository.Find(null);
        excpected.Should().BeNull();
    }

    [Fact, Trait("Book", "Repository")]
    public void FindBookWithName_CheckForWorkingWell()
    {
        _repository.Add(_book);

        var excpected = _repository.Find(_book.Name);
        excpected.Should().BeEquivalentTo(_book);
    }

    [Fact, Trait("Book", "Repository")]
    public void FindBookWithName_CheckForInvalidData_ReturnNull()
    {
        var excpected = _repository.Find("Some name###");
        excpected.Should().BeNull();
    }

    [Fact, Trait("Book", "Repository")]
    public void FindBookWithName_CheckForNullData_ReturnNull()
    {
        var excpected = _repository.Find(null);
        excpected.Should().BeNull();
    }

    [Fact, Trait("Book", "Repository")]
    public void FindByAddingDate_CheckForWorkingWell()
    {
        _repository.Add(_book);
        var excpected = _repository.FindByAddingDate(_book.DateofAdding);

        excpected.Should().Contain(_book);
    }

    [Fact, Trait("Book", "Repository")]
    public void FindByAddingDate_CheckForInvalidData_NotContainExcpectedBook()
    {
        var excpected = _repository.FindByAddingDate("addingdate####");
        excpected.Should().NotContain(_book);
    }

    [Fact, Trait("Book", "Repository")]
    public void FindByAddingDate_CheckForNullData_NotContainExcpectedBook()
    {
        var excpected = _repository.FindByAddingDate(null);
        excpected.Should().NotContain(_book);
    }

    [Theory, Trait("Book", "Repository")]
    [InlineData("GoodBook", true)]
    [InlineData("invalidBook", false)]
    [InlineData(null, false)]
    public void DoesNameExist_CheckForValidAndInvalidBookNames(string bookName, bool excpectation)
    {
        _repository.Add(_book);
        var excpected = _repository.DoesNameExist(bookName);
        excpected.Should().Be(excpectation);
    }

    [Fact, Trait("Book", "Reposiotry")]
    public void UpdateBook_CheckForWorkingWell()
    {
        var book = Book.Create("book1", "author2", "11/12/1344");
        _repository.Add(_book);
        _book.Modify(book.Name, book.AuthorName, book.DateofAdding);

        _repository.Update(_book);

        var excpected = _repository.Find(_book.Name);
        excpected.Name.Should().Be(book.Name);
        excpected.AuthorName.Should().Be(book.AuthorName);
        excpected.DateofAdding.Should().Be(book.DateofAdding);
    }

    [Fact, Trait("Book", "Repository")]
    public void UpdateBook_CheckForNullData_ThrowExcption()
    {
        void result() => _repository.Update(null);
        Assert.Throws<NullReferenceException>(result);
    }
}
