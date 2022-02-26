using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainModel.Validation
{
    public class BookValidation : AbstractValidator<Book>
    {
        public BookValidation()
        {
            RuleFor(i => i.Name).NotNull().NotEmpty();
            RuleFor(i => i.authorName).NotNull().NotEmpty().Matches("^[a-zA-Z]+$");
            RuleFor(i => i.DateofAdding).NotNull().NotEmpty().Must(ValidationBase.DateValidation);
        }

    }
}