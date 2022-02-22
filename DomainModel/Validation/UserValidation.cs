using FluentValidation;
using System;

namespace DomainModel.Validation
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(i => i.Name).NotNull().NotEmpty().MaximumLength(50).Matches("^[a-zA-Z]+$").WithMessage("Invalid Name");
            RuleFor(i => i.Family).NotNull().NotEmpty().MaximumLength(50).Matches("^[a-zA-Z]+$").WithMessage("Invalid Family");
            RuleFor(i => i.Age).NotNull().InclusiveBetween(15, 70).WithMessage("Invalid Age");
            RuleFor(i => i.NationalCode).MaximumLength(10).Must(CheckNationalCode).WithMessage("Invalid NationalCode");
            RuleFor(i => i.Email).EmailAddress().WithMessage("Invalid Email");
        }

        public bool CheckNationalCode(string nationalCode)
        {
            if (nationalCode.Length != 10)
                return false;
            try
            {
                char[] chArray = nationalCode.ToCharArray();
                int[] numArray = new int[chArray.Length];
                for (int i = 0; i < chArray.Length; i++)
                {
                    numArray[i] = (int)char.GetNumericValue(chArray[i]);
                }
                int num2 = numArray[9];
                switch (nationalCode)
                {
                    case "0000000000":
                    case "1111111111":
                    case "22222222222":
                    case "33333333333":
                    case "4444444444":
                    case "5555555555":
                    case "6666666666":
                    case "7777777777":
                    case "8888888888":
                    case "9999999999":
                        return false;
                }
                int num3 = numArray[0] * 10 + numArray[1] * 9 + numArray[2] * 8 + numArray[3] * 7 + numArray[4] * 6 + numArray[5] * 5 + numArray[6] * 4 + numArray[7] * 3 + numArray[8] * 2;
                int num4 = num3 - num3 / 11 * 11;

                if (num4 == 0 && num2 == num4 || num4 == 1 && num2 == 1 || num4 > 1 && num2 == Math.Abs(num4 - 11))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
