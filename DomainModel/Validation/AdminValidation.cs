using FluentValidation;

namespace DomainModel.Validation
{
    public class AdminValidation : AbstractValidator<Admin>
    {
        public AdminValidation()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Name Should not be Empty")
                    .Matches(@"^[a-zA-Z\s]+$|^$").WithMessage("Name Should not have Numbers or Special Characters");

            RuleFor(i => i.Family).NotEmpty().WithMessage("Family Should not be Empty")
                    .Matches(@"^[a-zA-Z\s]+$|^$").WithMessage("Family Should not have Numbers or Special Characters");

            RuleFor(i => i.NationalCode).NotEmpty().WithMessage("NationalCode Should not be Empty")
                    .Must(ValidationBase.CheckNationalCode).WithMessage("Invalid NationalCode");

            RuleFor(i => i.DateofBirth).NotEmpty().WithMessage("DateofBirth Should not be Empty")
                    .Must(ValidationBase.DateValidation).WithMessage("Invalid DateofBirth");

            RuleFor(i => i.UserName).NotEmpty().WithMessage("UserName Should not be Empty")
                    .Must(ValidationBase.CheckUserName).WithMessage("Invalid UserName");

            RuleFor(i => i.Password).NotEmpty().WithMessage("Password Should not be Empty")
                    .Must(ValidationBase.CheckPassword).WithMessage("week Password");

            RuleFor(i => i.Email).EmailAddress();
        }
    }
}