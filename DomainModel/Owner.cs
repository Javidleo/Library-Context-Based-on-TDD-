namespace DomainModel
{
    public class Owner
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Family { get; private set; }
        public string NationalCode { get; private set; }
        public string PhoneNumber { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }

        Owner(string name, string family, string nationalcode, string phonenumber, string username, string password)
        {
            Name = name;
            Family = family;
            NationalCode = nationalcode;
            PhoneNumber = phonenumber;
            UserName = username;
            Password = password;
        }
        public Owner() { }

        public static Owner Create(string name, string family, string nationalcode, string phonenumber, string username, string password)
            => new(name, family, nationalcode, phonenumber, username, password);
    }
}