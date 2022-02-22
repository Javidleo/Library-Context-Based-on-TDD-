using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DomainModel
{
    public class Admin
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Family { get; private set; }
        public string DateofBirth { get; private set; }
        public string NationalCode { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        Admin(int id, string name, string family, string dateofBirth, string nationalCode, string userName, string email, string password)
        {
            Id = id;
            Name = name;
            Family = family;
            DateofBirth = dateofBirth;
            NationalCode = nationalCode;
            UserName = userName;
            Email = email;
            Password = password;
        }

        public static Admin Create(int id, string name, string family, string dateofbirth, string nationalcode, string username, string email, string password)
            => new(id, name, family, dateofbirth, nationalcode, username, email, password);

    }
}