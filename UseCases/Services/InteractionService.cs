using DomainModel;
using System;
using System.Threading.Tasks;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.ServiceContract;

namespace UseCases.Services
{
    public class InteractionService: IInteractionService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IBookRepository _bookRepository;

        public InteractionService(IUserRepository userRepository, IAdminRepository adminRepository, IBookRepository bookRepository)
        {
            _userRepository = userRepository;
            _adminRepository = adminRepository;
            _bookRepository = bookRepository;
        }

        public Task Borrow(int userId, int bookId, int adminId)
        {
            if (_userRepository.Find(userId) is null)
                throw new NotFoundException("User Not Founded");

            if (_bookRepository.Find(bookId) is null)
                throw new NotFoundException("Book Not Founded");

            if (_adminRepository.Find(adminId) is null)
                throw new NotFoundException("Admin Not Founded");

            Interaction interaction = Interaction.Create(userId, bookId, adminId);
            return Task.CompletedTask;
        }
    }
}