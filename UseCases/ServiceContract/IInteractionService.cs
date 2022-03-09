using System.Threading.Tasks;

namespace UseCases.ServiceContract
{
    public interface IInteractionService
    {
        Task Borrow(int userId, int bookId, int adminId);
    }
}
