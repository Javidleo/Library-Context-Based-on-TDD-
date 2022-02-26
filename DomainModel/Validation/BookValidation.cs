using FluentValidation;

namespace DomainModel.Validation
{
    public class BookValidation : AbstractValidator<Book>
    {
        public BookValidation()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Name Should not be Empty").Matches(@"^[a-zA-Z\s\d]+$|^$").WithMessage("Name Should not have Special Characters");
            RuleFor(i => i.authorName).NotEmpty().WithMessage("AuthorName Should not be Empty").Matches(@"^[a-zA-Z\s]+$|^$").WithMessage("AuthorName Should not have Special Characters");
            RuleFor(i => i.DateofAdding).NotEmpty().WithMessage("DateofAdding Should not be Empty").Must(ValidationBase.DateValidation).WithMessage("Invalid DateofAdding");
        }

    }
}