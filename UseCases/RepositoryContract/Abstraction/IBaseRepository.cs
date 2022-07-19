using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UseCases.RepositoryContract.Abstraction
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Add(List<TEntity> entities);
        TEntity Find(int Id);
        List<TEntity> FindAll();
        bool DoesExist(Expression<Func<TEntity, bool>> filter);
        void Update(TEntity entity);
        void Update(List<TEntity> entities);
        void Delete(TEntity entity);
        void Delete(List<TEntity> entities);
    }
}
