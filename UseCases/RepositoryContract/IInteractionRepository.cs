using DomainModel;
using System.Collections.Generic;

namespace UseCases.RepositoryContract;

public interface IInteractionRepository
{
    List<Interaction> FindByUser(int userId);
    List<Interaction> FindByBookId(int bookId);
    void Add(Interaction interaction);
    Interaction Find(int id);
    void Update(Interaction interaction);
    List<Interaction> GetAll();
}
