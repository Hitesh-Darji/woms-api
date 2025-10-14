using Microsoft.EntityFrameworkCore;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
using WOMS.Infrastructure.Data;

namespace WOMS.Infrastructure.Repositories
{
    public class RateTableRepository : Repository<RateTable>, IRateTableRepository
    {
        public RateTableRepository(WomsDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Set<RateTable>()
                .AnyAsync(rt => rt.Name == name && !rt.IsDeleted, cancellationToken);
        }

        public async Task<bool> ExistsByNameExcludingIdAsync(string name, Guid excludeId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<RateTable>()
                .AnyAsync(rt => rt.Name == name && rt.Id != excludeId && !rt.IsDeleted, cancellationToken);
        }

        public async Task<IEnumerable<RateTable>> GetActiveRateTablesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<RateTable>()
                .Where(rt => rt.IsActive && !rt.IsDeleted)
                .OrderBy(rt => rt.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<RateTable>> GetRateTablesByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            return await _context.Set<RateTable>()
                .Where(rt => rt.EffectiveStartDate <= endDate && rt.EffectiveEndDate >= startDate && !rt.IsDeleted)
                .OrderBy(rt => rt.Name)
                .ToListAsync(cancellationToken);
        }
    }
}
