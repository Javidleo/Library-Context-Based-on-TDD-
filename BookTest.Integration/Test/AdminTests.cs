using BookDataAccess;
using BookDataAccess.Repository;
using DomainModel;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using UseCases.RepositoryContract;
using Xunit;

namespace BookTest.Integration.Test
{
    public class AdminTests : PersistTest<BookContext>
    {
        private readonly BookContext _context;
        private readonly IAdminRepository _repository;
        private readonly DbContextOptionsBuilder<BookContext> _optionBuilder;
        private readonly Admin _admin;
        public AdminTests()
        {
            _optionBuilder = GenerateOption();
            _context = new BookContext(_optionBuilder.Options);
            _repository = new AdminRepository(_context);
            _admin = Admin.Create("ali", "rezaie", "11/12/1388", "12412544", "user", "javidleo.ef@gmial.com", "adf@34");
        }

        private DbContextOptionsBuilder<BookContext> GenerateOption()
        {
            var optionBuilder = new DbContextOptionsBuilder<BookContext>();
            optionBuilder.UseSqlServer("Server=DESKTOP-MONHQ70;Database=bookdb;Trusted_Connection=True;");
            return optionBuilder;

        }

        [Fact, Trait("Admin", "Repository")]
        public void CreateAdmin_CheckForCreatingSuccessfully()
        {
            _repository.Add(_admin);
            var excpected = _repository.Find(_admin.Name);
            _admin.Should().BeEquivalentTo(excpected);
        }

        [Fact, Trait("Admin", "Repository")]
        public void GetAll_CheckForWorkingWell()
        {
            _repository.Add(_admin);
            var adminList = _repository.GetAll();
            adminList.Should().Contain(_admin);
        }

        [Fact, Trait("Admin", "Repository")]
        public void DoesExist_CheckForWorkingWell()
        {
            _repository.Add(_admin);
            bool doesExist = _repository.DoesExist(_admin.NationalCode);
            doesExist.Should().BeTrue();
        }

        [Fact, Trait("Admin", "Repository")]
        public void DoesExist_CheckForPassingWrongValue_ReturnFalse()
        {
            _repository.Add(_admin);
            bool doesExist = _repository.DoesExist("wrong nationalCode");
            doesExist.Should().BeFalse();
        }

        [Fact, Trait("Admin", "Repository")]
        public void GetByNationalCode_CheckForWorkingWell()
        {
            _repository.Add(_admin);
            var excpected = _repository.GetByNationalCode(_admin.NationalCode);
            _admin.Should().BeEquivalentTo(excpected);
        }
        [Fact, Trait("Admin", "Repository")]
        public void GetNatinoalCode_CheckForWorkingWithInvalidData_ReturnNull()
        {
            _repository.Add(_admin);
            var excpected = _repository.GetByNationalCode("natinalCode");
            excpected.Should().BeNull();
        }

        //[Fact, Trait("Admin", "Repository")]
        //public void FindById_CheckForWorkingWell_ReturnSuccess()
        //{
        //    var mockAdmin = new Mock<Admin>();
        //    mockAdmin.Setup(i => i.Id == 1).Returns(true);
        //    _repository.Add(mockAdmin.Object);
        //    var excpected = _repository.Find(mockAdmin.Object.Id);
        //    _admin.Should().BeEquivalentTo(excpected);
        //}

        //[Fact, Trait("Admin","Repository")]
        //public void FindById_CheckFOrWorkingWithInvalidData_ReturnSuccess()
        //{
        //    var mockAdmin = new Mock<Admin>();
        //    mockAdmin.Setup(i => i.Id == 1);
        //    _repository.Add(mockAdmin.Object);
        //    var excpected = _repository.Find(mockAdmin.Object.Id);

        //}
    }
}
