using FluentValidation;

namespace DomainModel.Validation
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Name Should not be Empty").Matches(@"^[a-zA-Z\s]+$|^$").WithMessage("Name Should not have Numbers or Special Characters");
            RuleFor(i => i.Family).NotEmpty().WithMessage("Family Should not be Empty").Matches(@"^[a-zA-Z\s]+$|^$").WithMessage("Family Should not have Numbers or Special Characters");
            RuleFor(i => i.Age).InclusiveBetween(12, 70).WithMessage("Age Should be between 12,70");
            RuleFor(i => i.NationalCode).NotEmpty().WithMessage("NationalCode Should not be Empty").Must(ValidationBase.CheckNationalCode).WithMessage("Invalid NationalCode");
            RuleFor(i => i.Email).EmailAddress();
        }

    }
}
