using DomainModel;
using System.Collections.Generic;
using System.Linq;
using UseCases.RepositoryContract;

namespace BookDataAccess.Repository;

internal class InteractionRepository : IInteractionRepository
{
    private readonly BookContext _context;
    public InteractionRepository(BookContext context)
    => _context = context;

    public List<Interaction> FindByUser(int userId)
    => _context.Interactions.Where(i => i.UserId == userId).ToList();
}
