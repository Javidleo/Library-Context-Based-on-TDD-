using DomainModel;
using FluentAssertions;
using Moq;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.Services;
using Xunit;

namespace BookTest.Unit;

public class InteractionTests
{
    private readonly InteractionService service;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly Mock<IAdminRepository> _adminRepositoryMock;
    private readonly MockRepository mockRepository;
    public InteractionTests()
    {
        mockRepository = new MockRepository(MockBehavior.Loose);
        _userRepositoryMock = mockRepository.Create<IUserRepository>();
        _bookRepositoryMock = mockRepository.Create<IBookRepository>();
        _adminRepositoryMock = mockRepository.Create<IAdminRepository>();
        service = new InteractionService(_userRepositoryMock.Object, _adminRepositoryMock.Object, _bookRepositoryMock.Object);
    }

    [Fact, Trait("Interaction", "Borrow")]
    public void BorrowBook_SendInvalidUserId_ThrowNotFoundException()
    {
        void result() => service.Borrow(userId: 1, bookId: 2, adminId: 3);
        var exception = Assert.Throws<NotFoundException>(result);

        Assert.Equal("User Not Founded", exception.Message);
    }

    [Fact, Trait("Interaction", "Borrow")]
    public void BorrowBook_SendInvalidBookId_ThrowNotFoundException()
    {
        _userRepositoryMock.Setup(i => i.Find(1)).Returns(User.Create("ali", "rezaie", 16, "091434543", "javidleo.ef@gmail.com", 1));

        void result() => service.Borrow(userId: 1, bookId: 2, adminId: 3);

        var exception = Assert.Throws<NotFoundException>(result);
        Assert.Equal("Book Not Founded", exception.Message);
    }

    [Fact, Trait("Integration", "Borrow")]
    public void BorrowBook_SendInvalidAdminId_ThrowNotFoundException()
    {
        _userRepositoryMock.Setup(i => i.Find(1)).Returns(User.Create("ali", "rezaie", 16, "091434543", "javidleo.ef@gmail.com", 1));
        _bookRepositoryMock.Setup(i => i.Find(2)).Returns(Book.Create("name", "authorName", "11/12/1388"));

        void result() => service.Borrow(userId: 1, bookId: 2, adminId: 3);
        var exception = Assert.Throws<NotFoundException>(result);
        Assert.Equal("Admin Not Founded", exception.Message);
    }

    [Fact, Trait("Interaction", "Borrow")]
    public void BorrowBook_CheckForWorkingWell_ReturnSuccessTaskStatus()
    {
        _userRepositoryMock.Setup(i => i.Find(1)).Returns(User.Create("ali", "rezaie", 16, "091434543", "javidleo.ef@gmail.com", 1));
        _bookRepositoryMock.Setup(i => i.Find(2)).Returns(Book.Create("name", "authorName", "11/12/1388"));
        _adminRepositoryMock.Setup(i => i.Find(3)).Returns(Admin.Create("ali", "mohamdai", "11/12/1355", "123512312", "javid", "email", "password"));

        var result = service.Borrow(userId: 1, bookId: 2, adminId: 3);

        result.Status.ToString().Should().Be("RanToCompletion");
    }
}
