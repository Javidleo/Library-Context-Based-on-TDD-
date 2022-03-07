using BookTest.Unit.Data.Owner;
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
        [InlineData("name123@", "Name Should not have Special Characters")]
        [InlineData("", "Name Should not be Empty")]
        public void OwnerValidation_ValidatingName_ThrowExcpectedMessage(string name, string errorMessage)
        {
            var owner = new OwnerBuilder().WithName(name).Build();
            var result = _validation.TestValidate(owner);
            result.ShouldHaveValidationErrorFor(owner => owner.Name).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Owner", "validation")]
        [InlineData("family#123", "Family Should not have Special Characters")]
        [InlineData("", "Family Should not be Empty")]
        public void OwnerValidation_ValidatingFamily_ThrowExceptedMessage(string family, string errorMessage)
        {
            var owner = new OwnerBuilder().WithFamily(family).Build();
            var result = _validation.TestValidate(owner);
            result.ShouldHaveValidationErrorFor(owner => owner.Family).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Owner", "validation")]
        [InlineData("", "NationalCode Should not be Empty")]
        [InlineData("3214123", "Invalid NationalCode")]
        public void OwnerValidation_ValidatingNationlCode_ThrowExceptedMessage(string nationalCode, string errorMessage)
        {
            var owner = new OwnerBuilder().WithNationalCode(nationalCode).Build();
            var result = _validation.TestValidate(owner);
            result.ShouldHaveValidationErrorFor(owner => owner.NationalCode).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Owner", "validation")]
        [InlineData("javid123$$", "UserName Should not be Empty")]
        [InlineData("user@4Javid!!", "Invalid UserName")]

        public void OwnerValidation_ValidatingUserName_ThrowExcepetedMessage(string username, string errorMessage)
        {
            var owner = new OwnerBuilder().WithUserName(username).Build();
            var result = _validation.TestValidate(owner);
            result.ShouldHaveValidationErrorFor(owner => owner.UserName).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Owner", "validation")]
        [InlineData("", "Password Should not be Empty")]
        [InlineData("12313", "week Password")]

        public void OwnerValidationn_ValidatingPassword_ThrowExceptedMessage(string password, string errorMessage)
        {
            var owner = new OwnerBuilder().WithPassword(password).Build();
            var result = _validation.TestValidate(owner);
            result.ShouldHaveValidationErrorFor(owner => owner.Password).WithErrorMessage(errorMessage);
        }

        [Theory, Trait("Owner", "validation")]
        [InlineData("3123123", "Invalid PhoneNumber")]
        [InlineData("", "PhoneNumber Should not be Empty")]
        [InlineData("03434567540", "Invalid PhoneNumber")]
        [InlineData("19434567540", "Invalid PhoneNumber")]
        public void OwnerValidation_ValidatingPhoneNumber_ThrowExceptedMessage(string phoneNumber, string errorMessage)
        {
            var owner = new OwnerBuilder().WithPhoneNumber(phoneNumber).Build();
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
