using Kalmeo.Models;
using Kalmeo.Repositories.Common;
using Kalmeo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kalmeo.Repositories.Implementation
{
    public class ReadOnlyRepository<TEntity> : Disposable, IReadOnlyRepository<TEntity> where TEntity : EntityBase
    {
        private readonly KalmeoContext _kalmeoContext;

        public ReadOnlyRepository(KalmeoContext kalmeoContext)
        {
            _kalmeoContext = kalmeoContext;
        }

        protected virtual (IQueryable<TEntity> Queryable, int TotalRecords) GetQueryable(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null, int? skip = null, int? take = null)
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<TEntity> query = _kalmeoContext.Set<TEntity>().AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            int totalRecords = query.Count();

            if (totalRecords > 0)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty).AsNoTracking();

                if (orderBy != null)
                    query = orderBy(query);

                if (skip.HasValue)
                    query = query.Skip(skip.Value);

                if (take.HasValue)
                    query = query.Take(take.Value);
            }

            return (query, totalRecords);
        }

        public virtual (IEnumerable<TEntity> Enumerable, int TotalRecords) GetAll(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null)
        {
            var result = GetQueryable(filter, orderBy, includeProperties, skip, take);
            return (result.Queryable.ToList(), result.TotalRecords);
        }

        public virtual async Task<(IEnumerable<TEntity> Enumerable, int TotalRecords)> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null, int? skip = null, int? take = null)
        {
            //return await GetQueryable(null, orderBy, includeProperties, skip, take).ToListAsync();
            var result = GetQueryable(filter, orderBy, includeProperties, skip, take);
            return (await result.Queryable.ToListAsync(), result.TotalRecords);
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null, int? skip = null, int? take = null)
        {
            var result = GetQueryable(filter, orderBy, includeProperties, skip, take);
            return result.Queryable.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null)
        {
            var result = GetQueryable(filter, orderBy, includeProperties, skip, take);
            return await result.Queryable.ToListAsync();
        }

        public virtual TEntity GetOne(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null)
        {
            var result = GetQueryable(filter, null, includeProperties);
            return result.Queryable.SingleOrDefault();
        }

        public virtual async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null)
        {
            var result = GetQueryable(filter, null, includeProperties);
            return await result.Queryable.SingleOrDefaultAsync();
        }

        public virtual TEntity GetFirst(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = null)
        {
            var result = GetQueryable(filter, orderBy, includeProperties);
            return result.Queryable.FirstOrDefault();
        }

        public virtual async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null)
        {
            var result = GetQueryable(filter, orderBy, includeProperties);
            return await result.Queryable.FirstOrDefaultAsync();
        }

        public virtual TEntity GetById(object id)
        {
            return _kalmeoContext.Set<TEntity>().Find(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await _kalmeoContext.Set<TEntity>().FindAsync(id);
        }

        public virtual int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            var result = GetQueryable(filter);
            return result.Queryable.Count();
        }

        public virtual Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Queryable.CountAsync();
        }

        public virtual bool GetExists(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Queryable.Any();
        }

        public virtual Task<bool> GetExistsAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Queryable.AnyAsync();
        }
    }
}
