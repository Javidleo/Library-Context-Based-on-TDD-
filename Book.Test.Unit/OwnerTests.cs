using BookTest.Unit.Data.Owner;
using DomainModel;
using DomainModel.Validation;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.Services;
using Xunit;

namespace BookTest.Unit
{
    public class OwnerTests
    {
        private readonly OwnerService _service;
        private readonly OwnerValidation _validation;
        private readonly MockRepository mockRepository;
        private readonly Mock<IOwnerRepository> ownerRepoMock;
        public OwnerTests()
        {
            mockRepository = new MockRepository(MockBehavior.Loose);
            ownerRepoMock = mockRepository.Create<IOwnerRepository>();
            _service = new OwnerService(ownerRepoMock.Object);
            _validation = new OwnerValidation();
        }

        [Theory, Trait("Owner", "validation")]
        [InlineData("name123@", "family", "0613575024", "09177034678", "user123", "Javid13#231@%@!", "Name Should not have Special Characters")]
        [InlineData("", "family", "0613575024", "09177034678", "user123", "1Javidl#4321@@#", "Name Should not be Empty")]
        public void OwnerValidation_ValidatingName_ThrowExcpectedMessage(string name, string family, string nationalcode, string phonenumber, string username, string password, string errorMessage)
        {
            var owner = Owner.Create(name, family, nationalcode, phonenumber, username, password);
            var result = _validation.TestValidate(owner);
            result.ShouldHaveValidationErrorFor(owner => owner.Name).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Owner", "validation")]
        [InlineData("name", "family#123", "0613575024", "09177044432", "user123", "javid123$$", "Family Should not have Special Characters")]
        [InlineData("name", "", "0613575024", "09177044432", "user123", "javid123$$", "Family Should not be Empty")]
        public void OwnerValidation_ValidatingFamily_ThrowExceptedMessage(string name, string family, string nationalcode, string phonenumber, string username, string password, string errorMessage)
        {
            var owner = Owner.Create(name, family, nationalcode, phonenumber, username, password);
            var result = _validation.TestValidate(owner);
            result.ShouldHaveValidationErrorFor(owner => owner.Family).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Owner", "validation")]
        [InlineData("name", "family", "", "09177044432", "user123", "javid123$$", "NationalCode Should not be Empty")]
        [InlineData("name", "family", "3214123", "09177044432", "user123", "javid123$$", "Invalid NationalCode")]
        public void OwnerValidation_ValidatingNationlCode_ThrowExceptedMessage(string name, string family, string nationalCode, string phoneNumber, string username, string password, string errorMessage)
        {
            var owner = Owner.Create(name, family, nationalCode, phoneNumber, username, password);
            var result = _validation.TestValidate(owner);
            result.ShouldHaveValidationErrorFor(owner => owner.NationalCode).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Owner", "validation")]
        [InlineData("name", "family", "0613575024", "09177044432", "", "javid123$$", "UserName Should not be Empty")]
        [InlineData("name", "family", "0613575024", "09177044432", "user@4Javid!!", "javid123$$", "Invalid UserName")]

        public void OwnerValidation_ValidatingUserName_ThrowExcepetedMessage(string name, string family, string nationalCode, string phoneNumber, string username, string password, string errorMessage)
        {
            var owner = Owner.Create(name, family, nationalCode, phoneNumber, username, password);
            var result = _validation.TestValidate(owner);
            result.ShouldHaveValidationErrorFor(owner => owner.UserName).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Owner", "validation")]
        [InlineData("name", "family", "0613575024", "09177044432", "user123", "", "Password Should not be Empty")]
        [InlineData("name", "family", "0613575024", "09177044432", "user123", "12313", "week Password")]

        public void OwnerValidationn_ValidatingPassword_ThrowExceptedMessage(string name, string family, string nationalCode, string phonenumber, string username, string password, string errorMessage)
        {
            var owner = Owner.Create(name, family, nationalCode, phonenumber, username, password);
            var result = _validation.TestValidate(owner);
            result.ShouldHaveValidationErrorFor(owner => owner.Password).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Owner", "validation")]
        [InlineData("name", "family", "0613575024", "3123123", "user@4Javid!!", "javid123$$", "Invalid PhoneNumber")]
        [InlineData("name", "family", "0613575024", "", "user@4Javid!!", "javid123$$", "PhoneNumber Should not be Empty")]
        [InlineData("name", "family", "0613575024", "03434567540", "user@4Javid!!", "javid123$$", "Invalid PhoneNumber")]
        [InlineData("name", "family", "0613575024", "19434567540", "user@4Javid!!", "javid123$$", "Invalid PhoneNumber")]
        public void OwnerValidation_ValidatingPhoneNumber_ThrowExceptedMessage(string name, string family, string nationalCode, string phoneNumber, string username, string password, string errorMessage)
        {
            var owner = Owner.Create(name, family, nationalCode, phoneNumber, username, password);
            var result = _validation.TestValidate(owner);
            result.ShouldHaveValidationErrorFor(owner => owner.PhoneNumber).WithErrorMessage(errorMessage);
        }

        [Fact, Trait("Owner", "create")]
        public void CreateOwner_CheckForCreatingSuccessFully_ReturnSuccessStatusTask()
        {
            var result = _service.Create("name", "family", "0613575024", "09177044432", "user123", "javid123$$");
            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Theory, Trait("Owner", "create")]
        [OwnerInvalidTestData]
        public void CreateOwner_CheckWithInvalidValues_ThrowNotAcceptableException(string name, string family, string nationalcode, string phonenumber, string username, string password)
        {
            void result() => _service.Create(name, family, nationalcode, phonenumber, username, password);
            Assert.Throws<NotAcceptableException>(result);
        }
    }
}
