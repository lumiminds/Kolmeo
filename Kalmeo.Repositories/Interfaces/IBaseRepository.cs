using Kalmeo.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kalmeo.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : EntityBase
    {
        void Create(TEntity entity, string createdBy = null);

        Task CreateAsync(TEntity entity, string createdBy = null);

        void Update(TEntity entity, string modifiedBy = null);

        void Delete(Guid id);

        void Delete(Expression<Func<TEntity, bool>> filter);

        void Delete(TEntity entity);

        void Delete(IEnumerable<TEntity> entity);

        void Save();

        Task SaveAsync();
    }
}
