using BookDataAccess;
using BookDataAccess.Repository;
using DomainModel;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.RepositoryContract;
using Xunit;

namespace BookTest.Integration.Test
{
    public class BookTests:PersistTest<BookContext>
    {
        private readonly IBookRepository _repository;
        private readonly DbContextOptionsBuilder<BookContext> _optionsBuilder;
        private readonly Book _book;
        private readonly BookContext _context;
        
        public BookTests()
        {
            _optionsBuilder = GenerateOptionBuilder();
            _context = new BookContext(_optionsBuilder.Options);
            _repository = new BookRepository(_context);
            _book = Book.Create("GoodBook", "ali", "11/12/1344");
        }

        private  DbContextOptionsBuilder<BookContext> GenerateOptionBuilder()
        {
            var optionBuilder = new DbContextOptionsBuilder<BookContext>();
            optionBuilder.UseSqlServer("Server=DESKTOP-MONHQ70;Database=bookdb;Trusted_Connection=True;");
            return optionBuilder;
        }

        [Fact, Trait("Book","Repository")]
        public void CreateBook_CheckForCreatingSuccessfully()
        {
            _repository.Add(_book);
            var excpected = _repository.Find(_book.Name);
            _book.Should().BeEquivalentTo(excpected);
        }

        [Fact,Trait("Book","Repository")]
        public void GetAll_CheckForWorkingWell()
        {
            _repository.Add(_book);
            var bookList = _repository.GetAll();
            bookList.Should().HaveCount(1);
            bookList[0].Should().BeEquivalentTo(_book);
        }

        [Theory, Trait("Book","Repository")]
        [InlineData("java","ahmad","11/12/1344")]
        [InlineData("neda","reza","11/12/1344")]
        public void FindByDateofAdding_CheckForWorkingWell(string name,string authorName,string dateofAdding)
        {
            Book book = Book.Create(name, authorName, dateofAdding);
            _repository.Add(_book);
            _repository.Add(book);

            var bookList = _repository.FindByAddingDate("11/12/1344");
            bookList.Should().Contain(_book);
            bookList.Should().Contain(book);
        }
    }
}
