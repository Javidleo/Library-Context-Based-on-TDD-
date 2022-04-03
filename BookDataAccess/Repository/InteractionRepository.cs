using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using UseCases.RepositoryContract;

namespace BookDataAccess.Repository;

public class InteractionRepository : IInteractionRepository
{
    private readonly BookContext _context;
    public InteractionRepository(BookContext context)
    => _context = context;

    public void Add(Interaction interaction)
    {
        _context.Interactions.Add(interaction);
        _context.SaveChanges();
    }

    public Interaction Find(int id)
    => _context.Interactions.FirstOrDefault(i => i.Id == id);

    public List<Interaction> FindByUserId(int userId)
    => _context.Interactions.Where(i => i.UserId == userId).ToList();

    public void Update(Interaction interaction)
    {
        _context.Interactions.Update(interaction);
        _context.SaveChanges();
    }

    public List<Interaction> GetAll()
    => _context.Interactions.ToList();

    public Interaction FindByBookId(int bookId)
    => _context.Interactions.FirstOrDefault(i => i.BookId == bookId&& i.IsDeleted == false);

    public List<Interaction> Find(DateTime date)
    => _context.Interactions.Where(i => i.Date == date).ToList();
}
