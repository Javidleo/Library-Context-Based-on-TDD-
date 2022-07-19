using BookTest.Unit.Data.BookTestData;
using DomainModel;
using System.Collections.Generic;
using UseCases.RepositoryContract;

namespace BookTest.Unit.TestDoubles
{
    public class BookFakeRepository : IBookRepository
    {
        private string _bookValidName;
        private int _bookValidId;
        private bool _IsUnavailable = false;

        public void SetExistingName(string name) => _bookValidName = name;
        public void SetExistingId(int id) => _bookValidId = id;
        public void MakeItUnAvailable() => _IsUnavailable = true;

        public void Add(Book book)
        {

        }

        public bool DoesNameExist(string name)
        {
            if (name == _bookValidName)
                return true;
            return false;
        }

        public Book Find(string name)
        {
            if (name == _bookValidName) return new BookBuilder().Build();
            else return null;
        }

        public Book Find(int id)
        {
            if (_IsUnavailable is true && id == _bookValidId)
                return new BookBuilder().IsUnavailable().Build();
            if (id == _bookValidId)
                return new BookBuilder().Build();

            else return null;
        }

        public List<Book> FindByAddingDate(string dateofAdding)
        {
            var list = new List<Book>()
            {
                new BookBuilder().Build(),
                new BookBuilder().Build(),
                new BookBuilder().Build()
            };
            return list;
        }

        public List<Book> FindAll()
        {
            var list = new List<Book>()
            {
                new BookBuilder().Build(),
                new BookBuilder().Build(),
                new BookBuilder().Build()
            };
            return list;
        }

        public void Update(Book book)
        {

        }
    }
}
