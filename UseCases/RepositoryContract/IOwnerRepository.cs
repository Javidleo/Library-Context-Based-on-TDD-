using DomainModel;
using System.Collections.Generic;

namespace UseCases.RepositoryContract;

public interface IOwnerRepository
{
    public void Add(Owner owner);
    public List<Owner> GetAll();
    public Owner Find(int id);
    public Owner Find(string name);
    public bool DoesNationalCodeExist(string nationalCode);
    public void Update(Owner owner);
    public void Delete(Owner owner);
}
