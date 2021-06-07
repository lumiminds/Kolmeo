using Kalmeo.Models;
using Kalmeo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kalmeo.Repositories.Implementation
{
    public abstract class BaseRepository<TEntity> : ReadOnlyRepository<TEntity>, IBaseRepository<TEntity> where TEntity : EntityBase
    {
        private readonly KalmeoContext _kalmeoContext;

        public BaseRepository(KalmeoContext kalmeoContext) : base(kalmeoContext)
        {
            _kalmeoContext = kalmeoContext;
        }

        public virtual void Create(TEntity entity, string createdBy = null)
        {
            _kalmeoContext.Set<TEntity>().Add(entity);
        }

        public virtual async Task CreateAsync(TEntity entity, string createdBy = null)
        {
            await _kalmeoContext.Set<TEntity>().AddAsync(entity);
        }

        public virtual void Update(TEntity entity, string modifiedBy = null)
        {
            _kalmeoContext.Set<TEntity>().Attach(entity);
            _kalmeoContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(Guid id)
        {
            TEntity entity = _kalmeoContext.Set<TEntity>().Find(id);
            Delete(entity);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> filter)
        {
            var dbSet = _kalmeoContext.Set<TEntity>();
            IQueryable<TEntity> entities = dbSet.Where(filter);
  
            dbSet.RemoveRange(entities);
        }

        public virtual void Delete(TEntity entity)
        {
            var dbSet = _kalmeoContext.Set<TEntity>();
            if (_kalmeoContext.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public virtual void Delete(IEnumerable<TEntity> entity)
        {
            var dbSet = _kalmeoContext.Set<TEntity>();
            dbSet.RemoveRange(entity);
        }

        public virtual void Save()
        {
            try
            {
                _kalmeoContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                ThrowEnhancedDbUpdateException(e);
            }
        }

        public virtual Task SaveAsync()
        {
            try
            {
                return _kalmeoContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                ThrowEnhancedDbUpdateException(e);
            }

            return Task.FromResult(0);
        }

        protected virtual void ThrowEnhancedDbUpdateException(DbUpdateException e)
        {
            throw e;
        }
    }
}
