using BookDataAccess;
using BookDataAccess.Repository;
using DomainModel;
using UseCases.RepositoryContract;
using Xunit;

namespace BookTest.Integration
{
    public class UserIntegrationTests : PersistTest<BookContext>
    {
        private IUserRepository _repository;
        public UserIntegrationTests()
        {
            _repository = new UserRepository();
        }
        [Fact]
        public void AddUser_CheckForCreatingSuccess_InTransaction()
        {
            var user = User.Create("ali", "rea", 17, "0994432437", "javidl@gmail.com");
            _repository.Add(user);
            var excpected = _repository.FindByName(user.Name);
            Assert.Equal(user, excpected);

        }
    }
}