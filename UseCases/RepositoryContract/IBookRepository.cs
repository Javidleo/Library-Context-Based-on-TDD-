using DomainModel;

namespace UseCases.RepositoryContract
{
    public interface IBookRepository
    {
        void Add(Book book);
    }
}