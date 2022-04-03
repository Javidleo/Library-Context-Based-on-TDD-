using DomainModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.ServiceContract;

namespace UseCases.Exceptions
{
    public class InteractionService: IInteractionService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IBookRepository _bookRepository;
        private IInteractionRepository _interactionRepository;

        public InteractionService(IUserRepository userRepository, IAdminRepository adminRepository, IBookRepository bookRepository,
                                    IInteractionRepository interactionRepository)
        {
            _userRepository = userRepository;
            _adminRepository = adminRepository;
            _bookRepository = bookRepository;
            _interactionRepository = interactionRepository;
        }

        public Task Borrow(int userId, int bookId, int adminId)
        {
            var book = _bookRepository.Find(bookId);
            if (book is null)
                throw new NotFoundException("Book Not Founded");

            if (book.InUse is true)
                throw new NotAcceptableException("Cannot Borrow UnAvailable Book");


            if (_userRepository.Find(userId) is null)
                throw new NotFoundException("User Not Founded");

            if (_adminRepository.Find(adminId) is null)
                throw new NotFoundException("Admin Not Founded");

            Interaction interaction = Interaction.Create(userId, bookId, adminId);

            _interactionRepository.Add(interaction);

            MakeBookUnAvailable(bookId);
            return Task.CompletedTask;
        }

        private void MakeBookUnAvailable(int bookId)
        {
            var book = _bookRepository.Find(bookId);
            book.UnAvailable();
            _bookRepository.Update(book);
        }

        public Task Delete(int Id)
        {
            var interaction = _interactionRepository.Find(Id);
            if (interaction is null)
                throw new NotFoundException("No Borrowing was Found");

            interaction.LogicalDelete();

            _interactionRepository.Update(interaction);

            MakeBookAvailable(interaction.BookId);
            return Task.CompletedTask;
        }

        public void MakeBookAvailable(int bookId)
        {
            var book = _bookRepository.Find(bookId);
            book.Available();
            _bookRepository.Update(book);
        }

        public Task<List<Interaction>> GetAll()
        => Task.FromResult(_interactionRepository.GetAll());

        public Task<List<Interaction>> FindByUserId(int userId)
        => Task.FromResult(_interactionRepository.FindByUserId(userId));

        public Task<Interaction> FindByBookId(int bookId)
        => Task.FromResult(_interactionRepository.FindByBookId(bookId));

        public Task<Interaction> FindByInteractionId(int id)
        {
            var interaction = _interactionRepository.Find(id);
            if (interaction is null)
                throw new NotFoundException("Not Founded");

            return Task.FromResult(interaction);
        }

        public Task<List<Interaction>> FindByInteractionDate(DateTime date)
        => Task.FromResult(_interactionRepository.Find(date));
    }
}