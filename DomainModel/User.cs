
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

        User(int id, string name, string family, int age, string nationalCode, string email)
        {
            Id = id;
            Name = name;
            Family = family;
            Age = age;
            NationalCode = nationalCode;
            Email = email;
        }
        public static User Create(int id, string name, string family, int age, string nationalCode, string email)
        => new(id, name, family, age, nationalCode, email);

    }
}