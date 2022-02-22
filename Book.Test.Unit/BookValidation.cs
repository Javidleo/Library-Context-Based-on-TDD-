using DomainModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookTest.Unit
{
    public class BookValidation : AbstractValidator<Book>
    {
        public BookValidation()
        {
            RuleFor(i => i.Name).NotNull().NotEmpty();
            RuleFor(i => i.authorName).NotNull().NotEmpty().Matches("^[a-zA-Z]+$");
            RuleFor(i => i.DateofAdding).NotNull().NotEmpty().Must(DateValidation);
        }

        private bool DateValidation(string dateofBirth)
        {
            if (dateofBirth.Length != 10)
                return false;

            var arr = dateofBirth.Split('/');
            if (arr.Length != 3)
                return false;

            List<char> numbers = new() { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            if (arr[0].ToCharArray().Any(i => !numbers.Contains(i)))
                return false;

            if (arr[1].ToCharArray().Any(i => !numbers.Contains(i)))
                return false;

            if (arr[2].ToCharArray().Any(i => !numbers.Contains(i)))
                return false;

            if (arr[0] == "00" || arr[1] == "00" || arr[2] == "00")
                return false;

            if (arr[0].Length != 2 || arr[1].Length != 2 || arr[2].Length != 4)
                return false;

            int year = Convert.ToInt32(arr[2]);
            int month = Convert.ToInt32(arr[1]);
            int day = Convert.ToInt32(arr[0]);
            if (year < 1300)
                return false;

            if (month < 0 && month > 12)
                return false;

            if (day < 0 && month > 31)
                return false;

            return true;
        }
    }
}