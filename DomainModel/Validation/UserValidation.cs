using FluentValidation;
using System;

namespace DomainModel.Validation
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(i => i.Name).NotNull().NotEmpty().MaximumLength(50).Matches("^[a-zA-Z]+$");
            RuleFor(i => i.Family).NotNull().NotEmpty().MaximumLength(50).Matches("^[a-zA-Z]+$");
            RuleFor(i => i.Age).NotNull().InclusiveBetween(15, 70).WithMessage("Invalid Age");
            RuleFor(i => i.NationalCode).MaximumLength(10).Must(ValidationBase.CheckNationalCode);
            RuleFor(i => i.Email).EmailAddress().WithMessage("Invalid Email");
        }

    }
}
