using WOMS.Domain.Entities;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Repositories
{
    public interface IStockRequestRepository : IRepository<StockRequest>
    {
        Task<IEnumerable<StockRequest>> GetByStatusAsync(StockRequestStatus status, CancellationToken cancellationToken = default);
        Task<IEnumerable<StockRequest>> GetByRequesterAsync(string requesterId, CancellationToken cancellationToken = default);
        Task<IEnumerable<StockRequest>> GetByFromLocationAsync(Guid fromLocationId, CancellationToken cancellationToken = default);
        Task<IEnumerable<StockRequest>> GetByToLocationAsync(Guid toLocationId, CancellationToken cancellationToken = default);
        Task<(IEnumerable<StockRequest> StockRequests, int TotalCount)> GetPaginatedAsync(
            int pageNumber,
            int pageSize,
            string? searchTerm = null,
            StockRequestStatus? status = null,
            Guid? fromLocationId = null,
            Guid? toLocationId = null,
            string? requesterId = null,
            string? sortBy = "RequestDate",
            bool sortDescending = true,
            CancellationToken cancellationToken = default);
        Task<StockRequest?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
        Task SoftDeleteAsync(Guid id, string deletedBy, CancellationToken cancellationToken = default);
    }
}

