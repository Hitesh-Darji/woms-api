using WOMS.Domain.Entities;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Repositories
{
    public interface IIntegrationRepository : IRepository<Integration>
    {
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<Integration?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<IEnumerable<Integration>> GetByCategoryAsync(IntegrationCategory category, CancellationToken cancellationToken = default);
        Task<IEnumerable<Integration>> GetActiveIntegrationsAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Integration>> GetConnectedIntegrationsAsync(CancellationToken cancellationToken = default);
    }
}

