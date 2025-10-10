using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WOMS.Domain.Entities;
using WOMS.Infrastructure.Data;

namespace WOMS.Infrastructure.Services
{
    public interface IWorkOrderQueryOptimizer
    {
        Task<IEnumerable<T>> GetOptimizedQueryAsync<T>(
            Expression<Func<WorkOrder, bool>> predicate,
            Expression<Func<WorkOrder, T>> selector,
            int? take = null,
            CancellationToken cancellationToken = default);
            
        Task<(IEnumerable<T> Items, int TotalCount)> GetOptimizedPaginatedQueryAsync<T>(
            Expression<Func<WorkOrder, bool>> predicate,
            Expression<Func<WorkOrder, T>> selector,
            int pageNumber,
            int pageSize,
            Expression<Func<WorkOrder, object>>? orderBy = null,
            bool descending = true,
            CancellationToken cancellationToken = default);
    }

    public class WorkOrderQueryOptimizer : IWorkOrderQueryOptimizer
    {
        private readonly WomsDbContext _context;

        public WorkOrderQueryOptimizer(WomsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetOptimizedQueryAsync<T>(
            Expression<Func<WorkOrder, bool>> predicate,
            Expression<Func<WorkOrder, T>> selector,
            int? take = null,
            CancellationToken cancellationToken = default)
        {
            var query = _context.WorkOrders
                .AsNoTracking()
                .Where(predicate);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.Select(selector).ToListAsync(cancellationToken);
        }

        public async Task<(IEnumerable<T> Items, int TotalCount)> GetOptimizedPaginatedQueryAsync<T>(
            Expression<Func<WorkOrder, bool>> predicate,
            Expression<Func<WorkOrder, T>> selector,
            int pageNumber,
            int pageSize,
            Expression<Func<WorkOrder, object>>? orderBy = null,
            bool descending = true,
            CancellationToken cancellationToken = default)
        {
            var baseQuery = _context.WorkOrders
                .AsNoTracking()
                .Where(predicate);

            // Get total count efficiently
            var totalCount = await baseQuery.CountAsync(cancellationToken);

            // Apply ordering
            if (orderBy != null)
            {
                baseQuery = descending 
                    ? baseQuery.OrderByDescending(orderBy)
                    : baseQuery.OrderBy(orderBy);
            }

            // Apply pagination and projection
            var items = await baseQuery
                .Select(selector)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }
    }
}
