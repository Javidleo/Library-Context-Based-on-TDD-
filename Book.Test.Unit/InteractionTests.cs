using DomainModel;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using Xunit;

namespace BookTest.Unit;

public class InteractionTests
{
    private readonly InteractionService service;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly Mock<IAdminRepository> _adminRepositoryMock;
    private readonly Mock<IInteractionRepository> _interactionRepositoryMock;
    private readonly MockRepository mockRepository;
    public InteractionTests()
    {
        mockRepository = new MockRepository(MockBehavior.Loose);
        _userRepositoryMock = mockRepository.Create<IUserRepository>();
        _bookRepositoryMock = mockRepository.Create<IBookRepository>();
        _adminRepositoryMock = mockRepository.Create<IAdminRepository>();
        _interactionRepositoryMock = mockRepository.Create<IInteractionRepository>();
        service = new InteractionService(_userRepositoryMock.Object, _adminRepositoryMock.Object, _bookRepositoryMock.Object,_interactionRepositoryMock.Object);
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

    [Fact, Trait("Interaction", "logicalDelete")]
    public void DeleteInteraction_SendInvalidInteractionId_ThrowNotFoundException()
    {
        void result() => service.Delete(1);
        Assert.Throws<NotFoundException>(result);
    }

    [Fact, Trait("Interaction","logicalDelete")]
    public void DeleteInteraction_CheckForWorkingWell_ReturnSuccessTaskStatus()
    {
        var interaction = Interaction.Create(userId: 1, bookId:2,adminId:3);
        _interactionRepositoryMock.Setup(i => i.Find(1)).Returns(interaction);
        _bookRepositoryMock.Setup(i => i.Find(2)).Returns(Book.Create("name", "authorName", "11/12/1399"));

        var result = service.Delete(Id: 1);
        result.Status.ToString().Should().Be("RanToCompletion");
        interaction.IsDeleted.Should().BeTrue();
    }
    
    [Fact, Trait("Interaction","GetAll")]
    public void GetAllInteractions_CheckForWorkingWell_ReturnSpecificResult()
    {
        List<Interaction> interactions = new();
        interactions.Add(Interaction.Create(1, 1, 1));
        interactions.Add(Interaction.Create(2, 2, 2));

        _interactionRepositoryMock.Setup(i => i.GetAll()).Returns(interactions);
        var result = service.GetAll();
        result.Should().BeEquivalentTo(interactions);
    }
}
