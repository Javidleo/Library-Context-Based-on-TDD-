using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UseCases.RepositoryContract.Abstraction;

namespace BookDataAccess.Repository.Abstraction
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly BookContext _bookContext;
        private readonly DbSet<TEntity> _dbSet;
        public BaseRepository(BookContext bookContext)
        {
            _bookContext = bookContext;
            _dbSet = _bookContext.Set<TEntity>();
        }
        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
            _bookContext.SaveChanges();
        }

        public virtual void Add(List<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            _bookContext.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            _bookContext.SaveChanges();
        }

        public virtual void Delete(List<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            _bookContext.SaveChanges();
        }

        public virtual bool DoesExist(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        => _dbSet.Any(filter);

        public virtual TEntity Find(int Id)
        => _dbSet.Find(Id);

        public virtual List<TEntity> FindAll()
        => _dbSet.ToList();

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
            _bookContext.SaveChanges();
        }

        public virtual void Update(List<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
            _bookContext.SaveChanges();
        }
    }
}
