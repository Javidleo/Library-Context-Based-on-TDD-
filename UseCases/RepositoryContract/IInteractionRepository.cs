using DomainModel;
using System;
using System.Collections.Generic;

namespace UseCases.RepositoryContract;

public interface IInteractionRepository
{
    void Add(Interaction interaction);
    List<Interaction> FindByUserId(int userId);
    Interaction FindByBookId(int bookId);
    Interaction Find(int id);
    void Update(Interaction interaction);
    List<Interaction> GetAll();
    List<Interaction> Find(DateTime date);
}
