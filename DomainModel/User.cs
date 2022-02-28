
using System;

namespace DomainModel
{
    public class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Family { get; private set; }
        public int Age { get; private set; }
        public string NationalCode { get; private set; }
        public string Email { get; private set; }

        User(string name, string family, int age, string nationalCode, string email)
        {
            Name = name;
            Family = family;
            Age = age;
            NationalCode = nationalCode;
            Email = email;
        }

        public User() { }

        public static User Create(string name, string family, int age, string nationalCode, string email)
        => new(name, family, age, nationalCode, email);

        public void Modify(string name, string family, int age, string email)
        {
            Name = name;
            Family =family;
            Age = age;
            Email = email;
        }
    }
}