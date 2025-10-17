using Microsoft.EntityFrameworkCore;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
using WOMS.Infrastructure.Data;

namespace WOMS.Infrastructure.Repositories
{
    public class OptimizationSettingsRepository : Repository<OptimizationSettings>, IOptimizationSettingsRepository
    {
        public OptimizationSettingsRepository(WomsDbContext context) : base(context)
        {
        }

        public async Task<OptimizationSettings?> GetCurrentSettingsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.OptimizationSettings
                .Where(s => !s.IsDeleted)
                .OrderByDescending(s => s.CreatedOn)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<OptimizationSettings> CreateOrUpdateSettingsAsync(OptimizationSettings settings, CancellationToken cancellationToken = default)
        {
            var existingSettings = await GetCurrentSettingsAsync(cancellationToken);
            
            if (existingSettings != null)
            {
                // Update existing settings
                existingSettings.PrioritizeDistanceReduction = settings.PrioritizeDistanceReduction;
                existingSettings.RespectTimeWindows = settings.RespectTimeWindows;
                existingSettings.BalanceWorkload = settings.BalanceWorkload;
                existingSettings.ConsiderTrafficPatterns = settings.ConsiderTrafficPatterns;
                existingSettings.MaxRouteTimeHours = settings.MaxRouteTimeHours;
                existingSettings.UpdatedOn = DateTime.UtcNow;
                existingSettings.UpdatedBy = settings.UpdatedBy;
                
                await _context.SaveChangesAsync(cancellationToken);
                return existingSettings;
            }
            else
            {
                // Create new settings
                settings.Id = Guid.NewGuid();
                settings.CreatedOn = DateTime.UtcNow;
                settings.IsDeleted = false;
                
                await _context.OptimizationSettings.AddAsync(settings, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return settings;
            }
        }
    }
}
