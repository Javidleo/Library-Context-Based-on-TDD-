using BookDataAccess.Repository.Abstraction;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UseCases.RepositoryContract;
using UseCases.ViewModel;

namespace BookDataAccess.Repository;

public class InteractionRepository : BaseRepository<Interaction>, IInteractionRepository
{
    private readonly BookContext _context;
    public InteractionRepository(BookContext bookContext) : base(bookContext)
    => _context = bookContext;

    public List<Interaction> FindByUserId(int userId)
    => _context.Interactions.Where(i => i.UserId == userId).ToList();

    public Interaction FindByBookId(int bookId)
    => _context.Interactions.FirstOrDefault(i => i.BookId == bookId && i.IsDeleted == false);

    public List<Interaction> Find(DateTime date)
    => _context.Interactions.Where(i => i.Date == date).ToList();

    public List<InteractionListViewModel> GetAll()
    => _context.Interactions.Include(i => i.Book).Include(i => i.User).Include(i => i.Admin).Select(i => new InteractionListViewModel
    {
        BookName = i.Book.Name,
        AdminName = i.Admin.Name,
        UserName = i.User.Name // add date later
    }).AsNoTracking().ToList();
}
