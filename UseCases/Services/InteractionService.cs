using DomainModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.ServiceContract;
using UseCases.ViewModel;

namespace UseCases.Services
{
    public class InteractionService : IInteractionService
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

        // ////////////////////////////////////   Borrow
        public Task Borrow(int userId, int bookId, int adminId)
        {
            var book = _bookRepository.Find(bookId);
            if (book is null)
                throw new NotFoundException("Book Not Founded");

            if (book.InUse is true)
                throw new NotAcceptableException("Cannot Borrow UnAvailable Book");

            if (!_userRepository.DoesExist(i=> i.Id == userId))
                throw new NotFoundException("User Not Founded");

            if (!_adminRepository.DoesExist(i=> i.Id == adminId))
                throw new NotFoundException("Admin Not Founded");

            Interaction interaction = Interaction.Create(userId, bookId, adminId);

            _interactionRepository.Add(interaction);

            MakeBookUnAvailable(book);
            return Task.CompletedTask;
        }

        // ////////////////////////////////////      Make Book UnAvailable
        private void MakeBookUnAvailable(Book book)
        {
            book.UnAvailable();
            _bookRepository.Update(book);
        }

        // /////////////////////////////////     Make Book Available 
        public void MakeBookAvailable(int bookId)
        {
            var book = _bookRepository.Find(bookId);
            book.Available();
            _bookRepository.Update(book);
        }

        // ///////////////////////////////////       Delete (logical Delete)
        public Task Delete(int Id)
        {
            var interaction = _interactionRepository.Find(Id);
            if (interaction is null)
                throw new NotFoundException("No Borrowing was Found");

            interaction.LogicalDelete();

            _interactionRepository.Update(interaction);

            MakeBookAvailable(interaction.BookId); // after giving book back to the available mode and users can borrow it again
            return Task.CompletedTask;
        }

        // ////////////////////////////////////   GetAll
        public Task<List<InteractionListViewModel>> GetAll()
        => Task.FromResult(_interactionRepository.GetAll());

        // //////////////////////////////////      Find By User Id 
        public Task<List<Interaction>> FindByUserId(int userId)
        => Task.FromResult(_interactionRepository.FindByUserId(userId));

        // /////////////////////////////////////        Find By Book Id
        public Task<Interaction> FindByBookId(int bookId)
        {
            var intearction = _interactionRepository.FindByBookId(bookId);
            if (intearction is null)
                throw new NotFoundException("no interaction founded with this bookId");
            return Task.FromResult(intearction);
        }

        // ///////////////////////////////////           Find By Interaction Id
        public Task<Interaction> FindByInteractionId(int id)
        {
            var interaction = _interactionRepository.Find(id);
            if (interaction is null)
                throw new NotFoundException("Not Founded");

            return Task.FromResult(interaction);
        }

        // //////////////////////////////////////     Find By Interaction Date
        public Task<List<Interaction>> FindByInteractionDate(DateTime date)
        => Task.FromResult(_interactionRepository.Find(date));
    }
}