using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;

namespace WOMS.Domain.Repositories
{
    public interface IOptimizationSettingsRepository : IRepository<OptimizationSettings>
    {
        Task<OptimizationSettings?> GetCurrentSettingsAsync(CancellationToken cancellationToken = default);
        Task<OptimizationSettings> CreateOrUpdateSettingsAsync(OptimizationSettings settings, CancellationToken cancellationToken = default);
    }
}
