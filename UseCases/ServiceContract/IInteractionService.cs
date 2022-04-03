using DomainModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UseCases.ServiceContract
{
    public interface IInteractionService
    {
        Task Borrow(int userId, int bookId, int adminId);
        Task Delete(int Id);
        Task<List<Interaction>> FindByUserId(int userId);
        Task<Interaction> FindByBookId(int bookId);
        Task<Interaction> FindByInteractionId(int id);
        Task<List<Interaction>> FindByInteractionDate(DateTime date);
        Task<List<Interaction>> GetAll();
    }
}
