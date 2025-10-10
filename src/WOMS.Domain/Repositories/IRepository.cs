using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WOMS.Domain.Entities;

namespace WOMS.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetQueryable();
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
       /* Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);*/
    }
}
