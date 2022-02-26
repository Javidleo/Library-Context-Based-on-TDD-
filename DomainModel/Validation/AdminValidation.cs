using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainModel.Validation
{
    public class AdminValidation : AbstractValidator<Admin>
    {
        public AdminValidation()
        {
            RuleFor(i => i.Name).NotNull().NotEmpty().Matches("^[a-zA-Z]+$");
            RuleFor(i => i.Family).NotNull().NotEmpty().Matches("^[a-zA-Z]+$");
            RuleFor(i => i.NationalCode).NotNull().NotEmpty().Must(ValidationBase.CheckNationalCode);
            RuleFor(i => i.DateofBirth).NotNull().NotEmpty().Must(ValidationBase.DateValidation);
            RuleFor(i => i.UserName).NotNull().NotEmpty().Must(ValidationBase.CheckUserName);
            RuleFor(i => i.Password).NotNull().NotEmpty().Must(ValidationBase.CheckPassword);
            RuleFor(i => i.Email).EmailAddress();
        }
    }
}