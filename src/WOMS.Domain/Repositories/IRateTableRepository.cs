using WOMS.Domain.Entities;

namespace WOMS.Domain.Repositories
{
    public interface IRateTableRepository : IRepository<RateTable>
    {
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameExcludingIdAsync(string name, Guid excludeId, CancellationToken cancellationToken = default);
        Task<IEnumerable<RateTable>> GetActiveRateTablesAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<RateTable>> GetRateTablesByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    }
}
