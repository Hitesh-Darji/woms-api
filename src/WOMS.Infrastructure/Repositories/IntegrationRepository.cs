using Microsoft.EntityFrameworkCore;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
using WOMS.Infrastructure.Data;

namespace WOMS.Infrastructure.Repositories
{
    public class IntegrationRepository : Repository<Integration>, IIntegrationRepository
    {
        public IntegrationRepository(WomsDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Integration>()
                .AnyAsync(i => i.Name == name && !i.IsDeleted, cancellationToken);
        }

        public async Task<Integration?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Integration>()
                .FirstOrDefaultAsync(i => i.Name == name && !i.IsDeleted, cancellationToken);
        }

        public async Task<IEnumerable<Integration>> GetByCategoryAsync(WOMS.Domain.Enums.IntegrationCategory category, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Integration>()
                .Where(i => i.Category == category && !i.IsDeleted)
                .OrderBy(i => i.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Integration>> GetActiveIntegrationsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<Integration>()
                .Where(i => i.IsActive && !i.IsDeleted)
                .OrderBy(i => i.Category)
                .ThenBy(i => i.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Integration>> GetConnectedIntegrationsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<Integration>()
                .Where(i => i.Status == WOMS.Domain.Enums.IntegrationStatus.Connected && !i.IsDeleted)
                .OrderBy(i => i.Category)
                .ThenBy(i => i.Name)
                .ToListAsync(cancellationToken);
        }
    }
}

