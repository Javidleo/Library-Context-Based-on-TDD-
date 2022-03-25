using FluentValidation;

namespace DomainModel.Validation
{
    public class OwnerValidation : AbstractValidator<Owner>
    {
        public OwnerValidation()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Name Should not be Empty").Matches(@"^[a-zA-Z\s]+$|^$").WithMessage("Name Should not have Special Characters");
            RuleFor(i => i.Family).NotEmpty().WithMessage("Family Should not be Empty").Matches(@"^[a-zA-Z\s]+$|^$").WithMessage("Family Should not have Special Characters");
            RuleFor(i => i.NationalCode).NotEmpty().WithMessage("NationalCode Should not be Empty").Must(ValidationBase.CheckNationalCode).WithMessage("Invalid NationalCode");
            RuleFor(i => i.UserName).NotEmpty().WithMessage("UserName Should not be Empty").Must(ValidationBase.CheckUserName).WithMessage("Invalid UserName");
            RuleFor(i => i.Password).NotEmpty().WithMessage("Password Should not be Empty").Must(ValidationBase.CheckPassword).WithMessage("week Password");
            RuleFor(i => i.PhoneNumber).NotEmpty().WithMessage("PhoneNumber Should not be Empty").Must(ValidationBase.CheckPhoneNumber).WithMessage("Invalid PhoneNumber");
        }
    }
}