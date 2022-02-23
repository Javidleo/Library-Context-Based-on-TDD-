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
            RuleFor(i => i.NationalCode).NotNull().NotEmpty().Must(CheckNationalCode);
            RuleFor(i => i.DateofBirth).NotNull().NotEmpty().Must(DateValidation);
            RuleFor(i => i.UserName).NotNull().NotEmpty().Must(CheckUserName);
            RuleFor(i => i.Password).NotNull().NotEmpty().Must(CheckPassword);
            RuleFor(i => i.Email).EmailAddress();
        }

        private bool CheckPassword(string password)
        {
            if (password.Length < 8)
                return false;

            List<char> specialCharacter = new() { '@', '#', '$', '!', '%', '^', '&', '*', '(', ')', '<', ',', '>', '/', '|' };
            if (!specialCharacter.Any(i => password.Contains(i)))
                return false;

            if (!password.ToCharArray().Any(i => char.IsDigit(i)))
                return false;

            if (password.Any(i => i == ' '))
                return false;
            return true;
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

        private bool CheckNationalCode(string nationalCode)
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

        private bool CheckUserName(string userName)
        {
            List<char> specialCharacter = new() { '@', '#', '$', '!', '%', '^', '&', '*', '(', ')', '<', ',', '>', '/', '|' };
            if (userName.Any(i => specialCharacter.Contains(i)))
                return false;

            if (userName.ToCharArray().Any(i => char.IsUpper(i)))
                return false;

            return true;
        }
    }
}