using DomainModel;
using System.Threading.Tasks;
using UseCases.Exceptions;

namespace UseCases.Services
{
    public class UserService
    {
        public UserService()
        {
        }

        public Task Create(int id, string name, string family, int age, string nationalCode, string email)
        {
            User user = User.Create(id, name, family, age, nationalCode, email);

            //Null Cheking
            if (string.IsNullOrEmpty(name))
                throw new NotAcceptableException("Name is Empty");

            if (string.IsNullOrEmpty(family))
                throw new NotAcceptableException("Family is Empty");

            if (string.IsNullOrEmpty(nationalCode))
                throw new NotAcceptableException("NationalCode is Empty");

            if (string.IsNullOrEmpty(email))
                throw new NotAcceptableException("Email is Empty");

            return Task.CompletedTask;
        }
    }
}