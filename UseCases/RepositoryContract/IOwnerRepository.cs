using DomainModel;
using UseCases.RepositoryContract.Abstraction;

namespace UseCases.RepositoryContract;

public interface IOwnerRepository : IBaseRepository<Owner>
{
    public Owner Find(string name);
    public bool DoesNationalCodeExist(string nationalCode);
}
