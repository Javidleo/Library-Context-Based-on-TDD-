using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace BookTest.Unit.Data.BookTestData
{
    public class BookBuilder
    {
        private string _name;
        private string _authorName;
        private string _addingDate;

        public  BookBuilder WithName(string name)
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
        public  DomainModel.Book Build()
        {
            DomainModel.Book book = DomainModel.Book.Create(_name, _authorName, _addingDate);
            return book;
        }
    }
}
