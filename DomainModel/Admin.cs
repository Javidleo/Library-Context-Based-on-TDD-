using System.Collections.Generic;

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
        public virtual List<Interaction> Interactions { get; private set; }

        private Admin() { }

        private Admin(string name, string family, string dateofBirth, string nationalCode, string userName,
                        string email, string password)
        {
            Name = name;
            Family = family;
            DateofBirth = dateofBirth;
            NationalCode = nationalCode;
            UserName = userName;
            Email = email;
            Password = password;
        }

        public static Admin Create(string name, string family, string dateofbirth, string nationalcode, string username,
                                    string email, string password)

        => new(name, family, dateofbirth, nationalcode, username, email, password);

        public void Modify(string name, string family, string dateofBirth, string username, string email, string password)
        {
            Name = name;
            Family = family;
            DateofBirth = dateofBirth;
            UserName = username;
            Email = email;
            Password = password;
        }
    }
}