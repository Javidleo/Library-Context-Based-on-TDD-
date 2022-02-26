using DomainModel;
using DomainModel.Validation;
using System.Threading.Tasks;
using UseCases.Exceptions;

namespace UseCases.Services
{
    public class OwnerService
    {
        private readonly OwnerValidation validation;
        public OwnerService()
        {
            validation = new OwnerValidation();
        }

        public Task Create(string Name, string Family, string NationalCode, string PhoneNumber, string UserName, string Password)
        {
            Owner buyer = Owner.Create(Name, Family, NationalCode, PhoneNumber, UserName, Password);

            if (!validation.Validate(buyer).IsValid)
                throw new NotAcceptableException("Invalid Buyer");

            return Task.CompletedTask;
        }
    }
}