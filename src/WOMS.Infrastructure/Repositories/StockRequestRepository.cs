using Microsoft.EntityFrameworkCore;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
using WOMS.Domain.Enums;
using WOMS.Infrastructure.Data;

namespace WOMS.Infrastructure.Repositories
{
    public class StockRequestRepository : Repository<StockRequest>, IStockRequestRepository
    {
        public StockRequestRepository(WomsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<StockRequest>> GetByStatusAsync(StockRequestStatus status, CancellationToken cancellationToken = default)
        {
            return await FindAsync(sr => sr.Status == status && !sr.IsDeleted, cancellationToken);
        }

        public async Task<IEnumerable<StockRequest>> GetByRequesterAsync(string requesterId, CancellationToken cancellationToken = default)
        {
            return await FindAsync(sr => sr.RequesterId == requesterId && !sr.IsDeleted, cancellationToken);
        }

        public async Task<IEnumerable<StockRequest>> GetByFromLocationAsync(Guid fromLocationId, CancellationToken cancellationToken = default)
        {
            return await FindAsync(sr => sr.FromLocationId == fromLocationId && !sr.IsDeleted, cancellationToken);
        }

        public async Task<IEnumerable<StockRequest>> GetByToLocationAsync(Guid toLocationId, CancellationToken cancellationToken = default)
        {
            return await FindAsync(sr => sr.ToLocationId == toLocationId && !sr.IsDeleted, cancellationToken);
        }

        public async Task<(IEnumerable<StockRequest> StockRequests, int TotalCount)> GetPaginatedAsync(
            int pageNumber,
            int pageSize,
            string? searchTerm = null,
            StockRequestStatus? status = null,
            Guid? fromLocationId = null,
            Guid? toLocationId = null,
            string? requesterId = null,
            string? sortBy = "RequestDate",
            bool sortDescending = true,
            CancellationToken cancellationToken = default)
        {
            var query = GetQueryable()
                .AsNoTracking()
                .Include(sr => sr.FromLocation)
                .Include(sr => sr.ToLocation)
                .Include(sr => sr.RequestItems)
                    .ThenInclude(ri => ri.Item)
                .Where(sr => !sr.IsDeleted);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(sr =>
                    EF.Functions.Like(sr.Notes, $"%{searchTerm}%") ||
                    EF.Functions.Like(sr.FromLocation.Name, $"%{searchTerm}%") ||
                    EF.Functions.Like(sr.ToLocation.Name, $"%{searchTerm}%") ||
                    sr.RequestItems.Any(ri => EF.Functions.Like(ri.Item.Description, $"%{searchTerm}%")));
            }

            if (status.HasValue)
            {
                query = query.Where(sr => sr.Status == status.Value);
            }

            if (fromLocationId.HasValue)
            {
                query = query.Where(sr => sr.FromLocationId == fromLocationId.Value);
            }

            if (toLocationId.HasValue)
            {
                query = query.Where(sr => sr.ToLocationId == toLocationId.Value);
            }

            if (!string.IsNullOrEmpty(requesterId))
            {
                query = query.Where(sr => sr.RequesterId == requesterId);
            }

            // Apply sorting
            query = (sortBy?.ToLower() ?? "requestdate") switch
            {
                "status" => sortDescending
                    ? query.OrderByDescending(sr => sr.Status)
                    : query.OrderBy(sr => sr.Status),
                "requestdate" => sortDescending
                    ? query.OrderByDescending(sr => sr.RequestDate)
                    : query.OrderBy(sr => sr.RequestDate),
                "fromlocation" => sortDescending
                    ? query.OrderByDescending(sr => sr.FromLocation.Name)
                    : query.OrderBy(sr => sr.FromLocation.Name),
                "tolocation" => sortDescending
                    ? query.OrderByDescending(sr => sr.ToLocation.Name)
                    : query.OrderBy(sr => sr.ToLocation.Name),
                "requester" => sortDescending
                    ? query.OrderByDescending(sr => sr.RequesterId)
                    : query.OrderBy(sr => sr.RequesterId),
                _ => sortDescending
                    ? query.OrderByDescending(sr => sr.RequestDate)
                    : query.OrderBy(sr => sr.RequestDate)
            };

            // Get total count
            var totalCount = await query.CountAsync(cancellationToken);

            // Apply pagination
            var stockRequests = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (stockRequests, totalCount);
        }

        public async Task<StockRequest?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await GetQueryable()
                .Include(sr => sr.FromLocation)
                .Include(sr => sr.ToLocation)
                .Include(sr => sr.RequestItems)
                    .ThenInclude(ri => ri.Item)
                .Include(sr => sr.WorkOrder)
                .FirstOrDefaultAsync(sr => sr.Id == id && !sr.IsDeleted, cancellationToken);
        }

        public async Task SoftDeleteAsync(Guid id, string deletedBy, CancellationToken cancellationToken = default)
        {
            var stockRequest = await GetByIdAsync(id, cancellationToken);
            if (stockRequest != null)
            {
                stockRequest.IsDeleted = true;
                stockRequest.DeletedBy = Guid.Parse(deletedBy);
                stockRequest.DeletedOn = DateTime.UtcNow;
                await UpdateAsync(stockRequest, cancellationToken);
            }
        }
    }
}

