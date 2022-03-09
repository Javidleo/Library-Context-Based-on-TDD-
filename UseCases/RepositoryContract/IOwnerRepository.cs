using DomainModel;
using System.Collections.Generic;

namespace UseCases.RepositoryContract;

public interface IOwnerRepository
{
    public void Add(Owner owner);
    public List<Owner> GetAll();
    public Owner FindById(int id);
}
