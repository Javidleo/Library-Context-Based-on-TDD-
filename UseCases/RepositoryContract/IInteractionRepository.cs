using DomainModel;
using System;
using System.Collections.Generic;
using UseCases.RepositoryContract.Abstraction;

namespace UseCases.RepositoryContract;

public interface IInteractionRepository : IBaseRepository<Interaction>
{
    List<Interaction> FindByUserId(int userId);
    Interaction FindByBookId(int bookId);
    List<Interaction> Find(DateTime date);
}
