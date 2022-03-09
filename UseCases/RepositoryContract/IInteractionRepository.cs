using DomainModel;
using System.Collections.Generic;

namespace UseCases.RepositoryContract;

public interface IInteractionRepository
{
    List<Interaction> FindByUser(int userId);
}
